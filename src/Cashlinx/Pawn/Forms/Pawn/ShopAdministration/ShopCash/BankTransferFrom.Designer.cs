using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    partial class BankTransferFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankTransferFrom));
            this.labelHeading1 = new System.Windows.Forms.Label();
            this.labelTransferDate = new System.Windows.Forms.Label();
            this.labelHeading2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelChkdata = new System.Windows.Forms.Panel();
            this.customTextBoxCheckNo = new CustomTextBox();
            this.customLabelCheckNo = new CustomLabel();
            this.panelDestination = new System.Windows.Forms.Panel();
            this.labelDestination = new System.Windows.Forms.Label();
            this.customLabelAmt = new CustomLabel();
            this.labelCashDrawerName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.customLabelCashDrawer = new CustomLabel();
            this.customTextBoxTrAmount = new CustomTextBox();
            this.panelBankData = new System.Windows.Forms.Panel();
            this.customTextBoxRoutingNumber = new CustomTextBox();
            this.customLabelRoutingNo = new CustomLabel();
            this.customLabelNewBankName = new CustomLabel();
            this.customTextBoxAcctNumber = new CustomTextBox();
            this.customTextBoxBankName = new CustomTextBox();
            this.customLabelNewAcctNo = new CustomLabel();
            this.customButtonSubmit = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.comboBoxBankData = new System.Windows.Forms.ComboBox();
            this.customLabelBankName = new CustomLabel();
            this.labelSource = new System.Windows.Forms.Label();
            this.panelCurrency = new System.Windows.Forms.Panel();
            this.currencyEntry2 = new CurrencyEntry();
            this.panel1.SuspendLayout();
            this.panelChkdata.SuspendLayout();
            this.panelDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelBankData.SuspendLayout();
            this.panelCurrency.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading1
            // 
            this.labelHeading1.AutoSize = true;
            this.labelHeading1.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading1.ForeColor = System.Drawing.Color.White;
            this.labelHeading1.Location = new System.Drawing.Point(28, 35);
            this.labelHeading1.Name = "labelHeading1";
            this.labelHeading1.Size = new System.Drawing.Size(133, 16);
            this.labelHeading1.TabIndex = 1;
            this.labelHeading1.Text = "Transfer From Bank";
            // 
            // labelTransferDate
            // 
            this.labelTransferDate.AutoSize = true;
            this.labelTransferDate.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferDate.ForeColor = System.Drawing.Color.White;
            this.labelTransferDate.Location = new System.Drawing.Point(603, 35);
            this.labelTransferDate.Name = "labelTransferDate";
            this.labelTransferDate.Size = new System.Drawing.Size(94, 16);
            this.labelTransferDate.TabIndex = 4;
            this.labelTransferDate.Text = "mm/dd/yyyy";
            // 
            // labelHeading2
            // 
            this.labelHeading2.AutoSize = true;
            this.labelHeading2.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading2.ForeColor = System.Drawing.Color.White;
            this.labelHeading2.Location = new System.Drawing.Point(494, 35);
            this.labelHeading2.Name = "labelHeading2";
            this.labelHeading2.Size = new System.Drawing.Size(103, 16);
            this.labelHeading2.TabIndex = 3;
            this.labelHeading2.Text = "Transfer Date:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panelChkdata);
            this.panel1.Controls.Add(this.panelDestination);
            this.panel1.Controls.Add(this.panelBankData);
            this.panel1.Controls.Add(this.customButtonSubmit);
            this.panel1.Controls.Add(this.customButtonCancel);
            this.panel1.Controls.Add(this.comboBoxBankData);
            this.panel1.Controls.Add(this.customLabelBankName);
            this.panel1.Controls.Add(this.labelSource);
            this.panel1.Location = new System.Drawing.Point(10, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 262);
            this.panel1.TabIndex = 6;
            // 
            // panelChkdata
            // 
            this.panelChkdata.Controls.Add(this.customTextBoxCheckNo);
            this.panelChkdata.Controls.Add(this.customLabelCheckNo);
            this.panelChkdata.Location = new System.Drawing.Point(94, 98);
            this.panelChkdata.Name = "panelChkdata";
            this.panelChkdata.Size = new System.Drawing.Size(302, 23);
            this.panelChkdata.TabIndex = 147;
            // 
            // customTextBoxCheckNo
            // 
            this.customTextBoxCheckNo.AllowOnlyNumbers = true;
            this.customTextBoxCheckNo.CausesValidation = false;
            this.customTextBoxCheckNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCheckNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCheckNo.Location = new System.Drawing.Point(168, 2);
            this.customTextBoxCheckNo.MaxLength = 20;
            this.customTextBoxCheckNo.Name = "customTextBoxCheckNo";
            this.customTextBoxCheckNo.Required = true;
            this.customTextBoxCheckNo.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCheckNo.TabIndex = 142;
            this.customTextBoxCheckNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelCheckNo
            // 
            this.customLabelCheckNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCheckNo.Location = new System.Drawing.Point(2, 2);
            this.customLabelCheckNo.Name = "customLabelCheckNo";
            this.customLabelCheckNo.Required = true;
            this.customLabelCheckNo.Size = new System.Drawing.Size(100, 21);
            this.customLabelCheckNo.TabIndex = 141;
            this.customLabelCheckNo.Text = "Check Number:";
            // 
            // panelDestination
            // 
            this.panelDestination.Controls.Add(this.labelDestination);
            this.panelDestination.Controls.Add(this.customLabelAmt);
            this.panelDestination.Controls.Add(this.labelCashDrawerName);
            this.panelDestination.Controls.Add(this.pictureBox1);
            this.panelDestination.Controls.Add(this.customLabelCashDrawer);
            this.panelDestination.Controls.Add(this.customTextBoxTrAmount);
            this.panelDestination.Location = new System.Drawing.Point(21, 125);
            this.panelDestination.Name = "panelDestination";
            this.panelDestination.Size = new System.Drawing.Size(508, 82);
            this.panelDestination.TabIndex = 146;
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestination.Location = new System.Drawing.Point(3, 9);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(82, 16);
            this.labelDestination.TabIndex = 129;
            this.labelDestination.Text = "Destination";
            // 
            // customLabelAmt
            // 
            this.customLabelAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAmt.Location = new System.Drawing.Point(76, 56);
            this.customLabelAmt.Name = "customLabelAmt";
            this.customLabelAmt.Required = true;
            this.customLabelAmt.Size = new System.Drawing.Size(118, 23);
            this.customLabelAmt.TabIndex = 139;
            this.customLabelAmt.Text = "Amount of Transfer:";
            // 
            // labelCashDrawerName
            // 
            this.labelCashDrawerName.AutoSize = true;
            this.labelCashDrawerName.BackColor = System.Drawing.Color.Transparent;
            this.labelCashDrawerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCashDrawerName.ForeColor = System.Drawing.Color.Black;
            this.labelCashDrawerName.Location = new System.Drawing.Point(242, 28);
            this.labelCashDrawerName.Name = "labelCashDrawerName";
            this.labelCashDrawerName.Size = new System.Drawing.Size(13, 13);
            this.labelCashDrawerName.TabIndex = 126;
            this.labelCashDrawerName.Text = "1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::Common.Properties.Resources.plus_icon_small;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(356, 53);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 128;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // customLabelCashDrawer
            // 
            this.customLabelCashDrawer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCashDrawer.Location = new System.Drawing.Point(58, 28);
            this.customLabelCashDrawer.Name = "customLabelCashDrawer";
            this.customLabelCashDrawer.Size = new System.Drawing.Size(136, 23);
            this.customLabelCashDrawer.TabIndex = 140;
            this.customLabelCashDrawer.Text = "Transfer To Cash Drawer:";
            // 
            // customTextBoxTrAmount
            // 
            this.customTextBoxTrAmount.AllowDecimalNumbers = true;
            this.customTextBoxTrAmount.CausesValidation = false;
            this.customTextBoxTrAmount.ErrorMessage = "Invalid amount entered";
            this.customTextBoxTrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxTrAmount.Location = new System.Drawing.Point(245, 53);
            this.customTextBoxTrAmount.MaxLength = 8;
            this.customTextBoxTrAmount.Name = "customTextBoxTrAmount";
            this.customTextBoxTrAmount.RegularExpression = true;
            this.customTextBoxTrAmount.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxTrAmount.TabIndex = 134;
            this.customTextBoxTrAmount.ValidationExpression = "(?!^0*$)(?!^0*\\.0*$)^\\d{1,5}(\\.\\d{1,2})?$";
            this.customTextBoxTrAmount.Leave += new System.EventHandler(this.customTextBoxTrAmount_Leave);
            // 
            // panelBankData
            // 
            this.panelBankData.Controls.Add(this.customTextBoxRoutingNumber);
            this.panelBankData.Controls.Add(this.customLabelRoutingNo);
            this.panelBankData.Controls.Add(this.customLabelNewBankName);
            this.panelBankData.Controls.Add(this.customTextBoxAcctNumber);
            this.panelBankData.Controls.Add(this.customTextBoxBankName);
            this.panelBankData.Controls.Add(this.customLabelNewAcctNo);
            this.panelBankData.Location = new System.Drawing.Point(94, 42);
            this.panelBankData.Name = "panelBankData";
            this.panelBankData.Size = new System.Drawing.Size(613, 54);
            this.panelBankData.TabIndex = 145;
            this.panelBankData.Visible = false;
            // 
            // customTextBoxRoutingNumber
            // 
            this.customTextBoxRoutingNumber.AllowOnlyNumbers = true;
            this.customTextBoxRoutingNumber.CausesValidation = false;
            this.customTextBoxRoutingNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxRoutingNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxRoutingNumber.Location = new System.Drawing.Point(450, 26);
            this.customTextBoxRoutingNumber.MaxLength = 9;
            this.customTextBoxRoutingNumber.Name = "customTextBoxRoutingNumber";
            this.customTextBoxRoutingNumber.Required = true;
            this.customTextBoxRoutingNumber.Size = new System.Drawing.Size(160, 21);
            this.customTextBoxRoutingNumber.TabIndex = 146;
            this.customTextBoxRoutingNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelRoutingNo
            // 
            this.customLabelRoutingNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelRoutingNo.Location = new System.Drawing.Point(345, 30);
            this.customLabelRoutingNo.Name = "customLabelRoutingNo";
            this.customLabelRoutingNo.Required = true;
            this.customLabelRoutingNo.Size = new System.Drawing.Size(100, 23);
            this.customLabelRoutingNo.TabIndex = 145;
            this.customLabelRoutingNo.Text = "Routing Number:";
            // 
            // customLabelNewBankName
            // 
            this.customLabelNewBankName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelNewBankName.Location = new System.Drawing.Point(20, 4);
            this.customLabelNewBankName.Name = "customLabelNewBankName";
            this.customLabelNewBankName.Required = true;
            this.customLabelNewBankName.Size = new System.Drawing.Size(100, 23);
            this.customLabelNewBankName.TabIndex = 138;
            this.customLabelNewBankName.Text = "Bank Name:";
            // 
            // customTextBoxAcctNumber
            // 
            this.customTextBoxAcctNumber.AllowOnlyNumbers = true;
            this.customTextBoxAcctNumber.CausesValidation = false;
            this.customTextBoxAcctNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxAcctNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAcctNumber.Location = new System.Drawing.Point(169, 30);
            this.customTextBoxAcctNumber.MaxLength = 40;
            this.customTextBoxAcctNumber.Name = "customTextBoxAcctNumber";
            this.customTextBoxAcctNumber.Required = true;
            this.customTextBoxAcctNumber.Size = new System.Drawing.Size(160, 21);
            this.customTextBoxAcctNumber.TabIndex = 144;
            this.customTextBoxAcctNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxBankName
            // 
            this.customTextBoxBankName.CausesValidation = false;
            this.customTextBoxBankName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBankName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxBankName.Location = new System.Drawing.Point(168, 4);
            this.customTextBoxBankName.MaxLength = 40;
            this.customTextBoxBankName.Name = "customTextBoxBankName";
            this.customTextBoxBankName.Required = true;
            this.customTextBoxBankName.Size = new System.Drawing.Size(161, 21);
            this.customTextBoxBankName.TabIndex = 137;
            this.customTextBoxBankName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelNewAcctNo
            // 
            this.customLabelNewAcctNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelNewAcctNo.Location = new System.Drawing.Point(20, 26);
            this.customLabelNewAcctNo.Name = "customLabelNewAcctNo";
            this.customLabelNewAcctNo.Required = true;
            this.customLabelNewAcctNo.Size = new System.Drawing.Size(100, 23);
            this.customLabelNewAcctNo.TabIndex = 143;
            this.customLabelNewAcctNo.Text = "Account Number:";
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
            this.customButtonSubmit.Location = new System.Drawing.Point(607, 208);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 0;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(21, 210);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 1;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // comboBoxBankData
            // 
            this.comboBoxBankData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBankData.FormattingEnabled = true;
            this.comboBoxBankData.Location = new System.Drawing.Point(260, 19);
            this.comboBoxBankData.Name = "comboBoxBankData";
            this.comboBoxBankData.Size = new System.Drawing.Size(247, 21);
            this.comboBoxBankData.TabIndex = 133;
            this.comboBoxBankData.SelectedIndexChanged += new System.EventHandler(this.comboBoxBankData_SelectedIndexChanged);
            // 
            // customLabelBankName
            // 
            this.customLabelBankName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelBankName.Location = new System.Drawing.Point(73, 19);
            this.customLabelBankName.Name = "customLabelBankName";
            this.customLabelBankName.Required = true;
            this.customLabelBankName.Size = new System.Drawing.Size(141, 23);
            this.customLabelBankName.TabIndex = 135;
            this.customLabelBankName.Text = "Bank Name and Account:";
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSource.Location = new System.Drawing.Point(18, 1);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(53, 16);
            this.labelSource.TabIndex = 123;
            this.labelSource.Text = "Source";
            // 
            // panelCurrency
            // 
            this.panelCurrency.BackColor = System.Drawing.Color.Transparent;
            this.panelCurrency.Controls.Add(this.currencyEntry2);
            this.panelCurrency.Location = new System.Drawing.Point(10, 100);
            this.panelCurrency.Name = "panelCurrency";
            this.panelCurrency.Size = new System.Drawing.Size(760, 321);
            this.panelCurrency.TabIndex = 7;
            // 
            // currencyEntry2
            // 
            this.currencyEntry2.BackColor = System.Drawing.Color.Transparent;
            this.currencyEntry2.Location = new System.Drawing.Point(4, 4);
            this.currencyEntry2.Name = "currencyEntry2";
            this.currencyEntry2.Size = new System.Drawing.Size(738, 313);
            this.currencyEntry2.TabIndex = 0;
            // 
            // BankTransferFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 697);
            this.ControlBox = false;
            this.Controls.Add(this.panelCurrency);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelTransferDate);
            this.Controls.Add(this.labelHeading2);
            this.Controls.Add(this.labelHeading1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BankTransferFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "+";
            this.Load += new System.EventHandler(this.BankTransferFrom_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelChkdata.ResumeLayout(false);
            this.panelChkdata.PerformLayout();
            this.panelDestination.ResumeLayout(false);
            this.panelDestination.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelBankData.ResumeLayout(false);
            this.panelBankData.PerformLayout();
            this.panelCurrency.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading1;
        private System.Windows.Forms.Label labelTransferDate;
        private System.Windows.Forms.Label labelHeading2;
        private System.Windows.Forms.Panel panel1;
        private CustomButton customButtonSubmit;
        private CustomButton customButtonCancel;
        private System.Windows.Forms.ComboBox comboBoxBankData;
        private CustomTextBox customTextBoxTrAmount;
        private CustomLabel customLabelBankName;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CustomTextBox customTextBoxBankName;
        private System.Windows.Forms.Label labelCashDrawerName;
        private CustomLabel customLabelNewBankName;
        private CustomLabel customLabelAmt;
        private System.Windows.Forms.Label labelSource;
        private CustomLabel customLabelCashDrawer;
        private System.Windows.Forms.Panel panelCurrency;
        private CurrencyEntry currencyEntry2;
        private CustomTextBox customTextBoxCheckNo;
        private CustomLabel customLabelCheckNo;
        private CustomTextBox customTextBoxAcctNumber;
        private CustomLabel customLabelNewAcctNo;
        private System.Windows.Forms.Panel panelDestination;
        private System.Windows.Forms.Panel panelBankData;
        private System.Windows.Forms.Panel panelChkdata;
        private CustomTextBox customTextBoxRoutingNumber;
        private CustomLabel customLabelRoutingNo;
    }
}
