using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    partial class BankTransferTo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankTransferTo));
            this.labelHeading1 = new System.Windows.Forms.Label();
            this.labelHeading2 = new System.Windows.Forms.Label();
            this.labelTransferDate = new System.Windows.Forms.Label();
            this.panelCurrency = new System.Windows.Forms.Panel();
            this.currencyEntry1 = new CurrencyEntry();
            this.panelTransferData = new System.Windows.Forms.Panel();
            this.panelBankData = new System.Windows.Forms.Panel();
            this.customTextBoxRoutingNumber = new CustomTextBox();
            this.customLabelRoutingNo = new CustomLabel();
            this.customLabelNewBankName = new CustomLabel();
            this.customTextBoxAcctNumber = new CustomTextBox();
            this.customTextBoxBankName = new CustomTextBox();
            this.customLabelNewAcctNo = new CustomLabel();
            this.panelDepositNo = new System.Windows.Forms.Panel();
            this.customTextBoxBagNo = new CustomTextBox();
            this.customLabelDepositBag = new CustomLabel();
            this.customLabelCashDrawerName = new CustomLabel();
            this.customButtonSubmit = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.comboBoxBankData = new System.Windows.Forms.ComboBox();
            this.customTextBoxTrAmount = new CustomTextBox();
            this.customLabelBankName = new CustomLabel();
            this.customLabelAmtHeading = new CustomLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelCashDrawerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCurrency.SuspendLayout();
            this.panelTransferData.SuspendLayout();
            this.panelBankData.SuspendLayout();
            this.panelDepositNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading1
            // 
            this.labelHeading1.AutoSize = true;
            this.labelHeading1.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading1.ForeColor = System.Drawing.Color.White;
            this.labelHeading1.Location = new System.Drawing.Point(22, 23);
            this.labelHeading1.Name = "labelHeading1";
            this.labelHeading1.Size = new System.Drawing.Size(117, 16);
            this.labelHeading1.TabIndex = 0;
            this.labelHeading1.Text = "Transfer To Bank";
            // 
            // labelHeading2
            // 
            this.labelHeading2.AutoSize = true;
            this.labelHeading2.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading2.ForeColor = System.Drawing.Color.White;
            this.labelHeading2.Location = new System.Drawing.Point(499, 23);
            this.labelHeading2.Name = "labelHeading2";
            this.labelHeading2.Size = new System.Drawing.Size(103, 16);
            this.labelHeading2.TabIndex = 1;
            this.labelHeading2.Text = "Transfer Date:";
            // 
            // labelTransferDate
            // 
            this.labelTransferDate.AutoSize = true;
            this.labelTransferDate.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferDate.ForeColor = System.Drawing.Color.White;
            this.labelTransferDate.Location = new System.Drawing.Point(608, 23);
            this.labelTransferDate.Name = "labelTransferDate";
            this.labelTransferDate.Size = new System.Drawing.Size(94, 16);
            this.labelTransferDate.TabIndex = 2;
            this.labelTransferDate.Text = "mm/dd/yyyy";
            // 
            // panelCurrency
            // 
            this.panelCurrency.BackColor = System.Drawing.Color.Transparent;
            this.panelCurrency.Controls.Add(this.currencyEntry1);
            this.panelCurrency.Location = new System.Drawing.Point(12, 100);
            this.panelCurrency.Name = "panelCurrency";
            this.panelCurrency.Size = new System.Drawing.Size(735, 308);
            this.panelCurrency.TabIndex = 96;
            // 
            // currencyEntry1
            // 
            this.currencyEntry1.BackColor = System.Drawing.Color.Transparent;
            this.currencyEntry1.Location = new System.Drawing.Point(-3, -5);
            this.currencyEntry1.Name = "currencyEntry1";
            this.currencyEntry1.Size = new System.Drawing.Size(738, 313);
            this.currencyEntry1.TabIndex = 0;
            // 
            // panelTransferData
            // 
            this.panelTransferData.BackColor = System.Drawing.Color.Transparent;
            this.panelTransferData.Controls.Add(this.panelBankData);
            this.panelTransferData.Controls.Add(this.panelDepositNo);
            this.panelTransferData.Controls.Add(this.customLabelCashDrawerName);
            this.panelTransferData.Controls.Add(this.customButtonSubmit);
            this.panelTransferData.Controls.Add(this.customButtonCancel);
            this.panelTransferData.Controls.Add(this.comboBoxBankData);
            this.panelTransferData.Controls.Add(this.customTextBoxTrAmount);
            this.panelTransferData.Controls.Add(this.customLabelBankName);
            this.panelTransferData.Controls.Add(this.customLabelAmtHeading);
            this.panelTransferData.Controls.Add(this.label2);
            this.panelTransferData.Controls.Add(this.pictureBox1);
            this.panelTransferData.Controls.Add(this.labelCashDrawerName);
            this.panelTransferData.Controls.Add(this.label1);
            this.panelTransferData.Location = new System.Drawing.Point(12, 414);
            this.panelTransferData.Name = "panelTransferData";
            this.panelTransferData.Size = new System.Drawing.Size(735, 280);
            this.panelTransferData.TabIndex = 97;
            // 
            // panelBankData
            // 
            this.panelBankData.Controls.Add(this.customTextBoxRoutingNumber);
            this.panelBankData.Controls.Add(this.customLabelRoutingNo);
            this.panelBankData.Controls.Add(this.customLabelNewBankName);
            this.panelBankData.Controls.Add(this.customTextBoxAcctNumber);
            this.panelBankData.Controls.Add(this.customTextBoxBankName);
            this.panelBankData.Controls.Add(this.customLabelNewAcctNo);
            this.panelBankData.Location = new System.Drawing.Point(86, 141);
            this.panelBankData.Name = "panelBankData";
            this.panelBankData.Size = new System.Drawing.Size(632, 54);
            this.panelBankData.TabIndex = 146;
            this.panelBankData.Visible = false;
            // 
            // customTextBoxRoutingNumber
            // 
            this.customTextBoxRoutingNumber.AllowOnlyNumbers = true;
            this.customTextBoxRoutingNumber.CausesValidation = false;
            this.customTextBoxRoutingNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxRoutingNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxRoutingNumber.Location = new System.Drawing.Point(458, 28);
            this.customTextBoxRoutingNumber.MaxLength = 9;
            this.customTextBoxRoutingNumber.Name = "customTextBoxRoutingNumber";
            this.customTextBoxRoutingNumber.Required = true;
            this.customTextBoxRoutingNumber.Size = new System.Drawing.Size(160, 21);
            this.customTextBoxRoutingNumber.TabIndex = 148;
            this.customTextBoxRoutingNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelRoutingNo
            // 
            this.customLabelRoutingNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelRoutingNo.Location = new System.Drawing.Point(353, 31);
            this.customLabelRoutingNo.Name = "customLabelRoutingNo";
            this.customLabelRoutingNo.Required = true;
            this.customLabelRoutingNo.Size = new System.Drawing.Size(100, 17);
            this.customLabelRoutingNo.TabIndex = 147;
            this.customLabelRoutingNo.Text = "Routing Number:";
            // 
            // customLabelNewBankName
            // 
            this.customLabelNewBankName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelNewBankName.Location = new System.Drawing.Point(62, 4);
            this.customLabelNewBankName.Name = "customLabelNewBankName";
            this.customLabelNewBankName.Required = true;
            this.customLabelNewBankName.Size = new System.Drawing.Size(82, 23);
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
            this.customLabelNewAcctNo.Location = new System.Drawing.Point(37, 30);
            this.customLabelNewAcctNo.Name = "customLabelNewAcctNo";
            this.customLabelNewAcctNo.Required = true;
            this.customLabelNewAcctNo.Size = new System.Drawing.Size(100, 23);
            this.customLabelNewAcctNo.TabIndex = 143;
            this.customLabelNewAcctNo.Text = "Account Number:";
            // 
            // panelDepositNo
            // 
            this.panelDepositNo.Controls.Add(this.customTextBoxBagNo);
            this.panelDepositNo.Controls.Add(this.customLabelDepositBag);
            this.panelDepositNo.Location = new System.Drawing.Point(81, 202);
            this.panelDepositNo.Name = "panelDepositNo";
            this.panelDepositNo.Size = new System.Drawing.Size(413, 25);
            this.panelDepositNo.TabIndex = 125;
            // 
            // customTextBoxBagNo
            // 
            this.customTextBoxBagNo.CausesValidation = false;
            this.customTextBoxBagNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBagNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxBagNo.Location = new System.Drawing.Point(174, 3);
            this.customTextBoxBagNo.MaxLength = 20;
            this.customTextBoxBagNo.Name = "customTextBoxBagNo";
            this.customTextBoxBagNo.Required = true;
            this.customTextBoxBagNo.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxBagNo.TabIndex = 124;
            this.customTextBoxBagNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelDepositBag
            // 
            this.customLabelDepositBag.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelDepositBag.Location = new System.Drawing.Point(25, 0);
            this.customLabelDepositBag.Name = "customLabelDepositBag";
            this.customLabelDepositBag.Required = true;
            this.customLabelDepositBag.Size = new System.Drawing.Size(124, 23);
            this.customLabelDepositBag.TabIndex = 0;
            this.customLabelDepositBag.Text = "Deposit Bag Number:";
            // 
            // customLabelCashDrawerName
            // 
            this.customLabelCashDrawerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCashDrawerName.Location = new System.Drawing.Point(78, 29);
            this.customLabelCashDrawerName.Name = "customLabelCashDrawerName";
            this.customLabelCashDrawerName.Size = new System.Drawing.Size(145, 23);
            this.customLabelCashDrawerName.TabIndex = 1;
            this.customLabelCashDrawerName.Text = "Transfer From Cash Drawer:";
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
            this.customButtonSubmit.Location = new System.Drawing.Point(590, 230);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 2;
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
            this.customButtonCancel.Location = new System.Drawing.Point(13, 230);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 3;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // comboBoxBankData
            // 
            this.comboBoxBankData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBankData.FormattingEnabled = true;
            this.comboBoxBankData.Location = new System.Drawing.Point(249, 115);
            this.comboBoxBankData.Name = "comboBoxBankData";
            this.comboBoxBankData.Size = new System.Drawing.Size(245, 21);
            this.comboBoxBankData.TabIndex = 120;
            this.comboBoxBankData.SelectedIndexChanged += new System.EventHandler(this.comboBoxBankData_SelectedIndexChanged);
            // 
            // customTextBoxTrAmount
            // 
            this.customTextBoxTrAmount.AllowDecimalNumbers = true;
            this.customTextBoxTrAmount.CausesValidation = false;
            this.customTextBoxTrAmount.ErrorMessage = "Invalid amount entered";
            this.customTextBoxTrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxTrAmount.Location = new System.Drawing.Point(249, 54);
            this.customTextBoxTrAmount.MaxLength = 8;
            this.customTextBoxTrAmount.Name = "customTextBoxTrAmount";
            this.customTextBoxTrAmount.RegularExpression = true;
            this.customTextBoxTrAmount.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxTrAmount.TabIndex = 121;
            this.customTextBoxTrAmount.ValidationExpression = "(?!^0*$)(?!^0*\\.0*$)^\\d{1,5}(\\.\\d{1,2})?$";
            this.customTextBoxTrAmount.Leave += new System.EventHandler(this.customTextBoxTrAmount_Leave);
            // 
            // customLabelBankName
            // 
            this.customLabelBankName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelBankName.Location = new System.Drawing.Point(83, 119);
            this.customLabelBankName.Name = "customLabelBankName";
            this.customLabelBankName.Required = true;
            this.customLabelBankName.Size = new System.Drawing.Size(128, 23);
            this.customLabelBankName.TabIndex = 122;
            this.customLabelBankName.Text = "Bank Account and Name:";
            // 
            // customLabelAmtHeading
            // 
            this.customLabelAmtHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAmtHeading.Location = new System.Drawing.Point(123, 54);
            this.customLabelAmtHeading.Name = "customLabelAmtHeading";
            this.customLabelAmtHeading.Required = true;
            this.customLabelAmtHeading.Size = new System.Drawing.Size(100, 23);
            this.customLabelAmtHeading.TabIndex = 123;
            this.customLabelAmtHeading.Text = "Transfer Amount:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 116;
            this.label2.Text = "Destination";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::Common.Properties.Resources.plus_icon_small;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(360, 54);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 115;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelCashDrawerName
            // 
            this.labelCashDrawerName.AutoSize = true;
            this.labelCashDrawerName.BackColor = System.Drawing.Color.Transparent;
            this.labelCashDrawerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCashDrawerName.ForeColor = System.Drawing.Color.Black;
            this.labelCashDrawerName.Location = new System.Drawing.Point(246, 29);
            this.labelCashDrawerName.Name = "labelCashDrawerName";
            this.labelCashDrawerName.Size = new System.Drawing.Size(13, 13);
            this.labelCashDrawerName.TabIndex = 113;
            this.labelCashDrawerName.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 101;
            this.label1.Text = "Source";
            // 
            // BankTransferTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(757, 706);
            this.ControlBox = false;
            this.Controls.Add(this.panelTransferData);
            this.Controls.Add(this.panelCurrency);
            this.Controls.Add(this.labelTransferDate);
            this.Controls.Add(this.labelHeading2);
            this.Controls.Add(this.labelHeading1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BankTransferTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BankTransferTo";
            this.Load += new System.EventHandler(this.BankTransferTo_Load);
            this.panelCurrency.ResumeLayout(false);
            this.panelTransferData.ResumeLayout(false);
            this.panelTransferData.PerformLayout();
            this.panelBankData.ResumeLayout(false);
            this.panelBankData.PerformLayout();
            this.panelDepositNo.ResumeLayout(false);
            this.panelDepositNo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading1;
        private System.Windows.Forms.Label labelHeading2;
        private System.Windows.Forms.Label labelTransferDate;
        private System.Windows.Forms.Panel panelCurrency;
        private System.Windows.Forms.Panel panelTransferData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CustomTextBox customTextBoxBagNo;
        private System.Windows.Forms.Label labelCashDrawerName;
        private CustomTextBox customTextBoxTrAmount;
        private CustomLabel customLabelBankName;
        private CustomLabel customLabelAmtHeading;
        private System.Windows.Forms.ComboBox comboBoxBankData;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private CustomLabel customLabelDepositBag;
        private CustomLabel customLabelCashDrawerName;
        private CurrencyEntry currencyEntry1;
        private System.Windows.Forms.Panel panelDepositNo;
        private System.Windows.Forms.Panel panelBankData;
        private CustomLabel customLabelNewBankName;
        private CustomTextBox customTextBoxAcctNumber;
        private CustomTextBox customTextBoxBankName;
        private CustomLabel customLabelNewAcctNo;
        private CustomTextBox customTextBoxRoutingNumber;
        private CustomLabel customLabelRoutingNo;

    }
}
