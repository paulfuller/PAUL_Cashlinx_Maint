using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    partial class InternalTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternalTransfer));
            this.labelHeading1 = new System.Windows.Forms.Label();
            this.labelHeading2 = new System.Windows.Forms.Label();
            this.labelTransferDate = new System.Windows.Forms.Label();
            this.panelCurrency = new System.Windows.Forms.Panel();
            this.currencyEntry1 = new CurrencyEntry();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customTextBoxDestUser = new CustomTextBox();
            this.customLabelToCashDrawer = new CustomLabel();
            this.customButtonCancel = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxCashDrawerData = new System.Windows.Forms.ComboBox();
            this.customTextBoxTrAmount = new CustomTextBox();
            this.customTextBoxDestUserPwd = new CustomTextBox();
            this.labelCashDrawerName = new System.Windows.Forms.Label();
            this.customLabelAmtHeading = new CustomLabel();
            this.customLabel5 = new CustomLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.customLabel4 = new CustomLabel();
            this.customLabelCashDrawer = new CustomLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCurrency.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.labelHeading1.Size = new System.Drawing.Size(119, 16);
            this.labelHeading1.TabIndex = 0;
            this.labelHeading1.Text = "Internal Transfer";
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
            this.panelCurrency.Location = new System.Drawing.Point(13, 97);
            this.panelCurrency.Name = "panelCurrency";
            this.panelCurrency.Size = new System.Drawing.Size(752, 326);
            this.panelCurrency.TabIndex = 3;
            // 
            // currencyEntry1
            // 
            this.currencyEntry1.BackColor = System.Drawing.Color.Transparent;
            this.currencyEntry1.Location = new System.Drawing.Point(-1, 3);
            this.currencyEntry1.Name = "currencyEntry1";
            this.currencyEntry1.Size = new System.Drawing.Size(738, 318);
            this.currencyEntry1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.customTextBoxDestUser);
            this.panel1.Controls.Add(this.customLabelToCashDrawer);
            this.panel1.Controls.Add(this.customButtonCancel);
            this.panel1.Controls.Add(this.customButtonSubmit);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.comboBoxCashDrawerData);
            this.panel1.Controls.Add(this.customTextBoxTrAmount);
            this.panel1.Controls.Add(this.customTextBoxDestUserPwd);
            this.panel1.Controls.Add(this.labelCashDrawerName);
            this.panel1.Controls.Add(this.customLabelAmtHeading);
            this.panel1.Controls.Add(this.customLabel5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.customLabel4);
            this.panel1.Controls.Add(this.customLabelCashDrawer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(10, 421);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 240);
            this.panel1.TabIndex = 4;
            // 
            // customTextBoxDestUser
            // 
            this.customTextBoxDestUser.CausesValidation = false;
            this.customTextBoxDestUser.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDestUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDestUser.Location = new System.Drawing.Point(290, 139);
            this.customTextBoxDestUser.Name = "customTextBoxDestUser";
            this.customTextBoxDestUser.Required = true;
            this.customTextBoxDestUser.Size = new System.Drawing.Size(156, 21);
            this.customTextBoxDestUser.TabIndex = 3;
            this.customTextBoxDestUser.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDestUser.TextChanged += new System.EventHandler(this.customTextBoxDestUser_TextChanged);
            // 
            // customLabelToCashDrawer
            // 
            this.customLabelToCashDrawer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelToCashDrawer.Location = new System.Drawing.Point(101, 112);
            this.customLabelToCashDrawer.Name = "customLabelToCashDrawer";
            this.customLabelToCashDrawer.Size = new System.Drawing.Size(133, 23);
            this.customLabelToCashDrawer.TabIndex = 1;
            this.customLabelToCashDrawer.Text = "Transfer To Cash Drawer:";
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
            this.customButtonCancel.Location = new System.Drawing.Point(15, 181);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 5;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(614, 181);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 6;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.EnabledChanged += new System.EventHandler(this.customButtonSubmit_EnabledChanged);
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::Common.Properties.Resources.plus_icon_small;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(381, 53);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 110;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // comboBoxCashDrawerData
            // 
            this.comboBoxCashDrawerData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCashDrawerData.FormattingEnabled = true;
            this.comboBoxCashDrawerData.Location = new System.Drawing.Point(290, 112);
            this.comboBoxCashDrawerData.Name = "comboBoxCashDrawerData";
            this.comboBoxCashDrawerData.Size = new System.Drawing.Size(222, 21);
            this.comboBoxCashDrawerData.TabIndex = 2;
            this.comboBoxCashDrawerData.SelectedIndexChanged += new System.EventHandler(this.comboBoxCashDrawerData_SelectedIndexChanged);
            // 
            // customTextBoxTrAmount
            // 
            this.customTextBoxTrAmount.AllowDecimalNumbers = true;
            this.customTextBoxTrAmount.CausesValidation = false;
            this.customTextBoxTrAmount.ErrorMessage = "Invalid amount entered";
            this.customTextBoxTrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxTrAmount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.customTextBoxTrAmount.Location = new System.Drawing.Point(270, 53);
            this.customTextBoxTrAmount.MaxLength = 8;
            this.customTextBoxTrAmount.Name = "customTextBoxTrAmount";
            this.customTextBoxTrAmount.RegularExpression = true;
            this.customTextBoxTrAmount.Required = true;
            this.customTextBoxTrAmount.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxTrAmount.TabIndex = 1;
            this.customTextBoxTrAmount.ValidationExpression = "(?!^0*$)(?!^0*\\.0*$)^\\d{1,5}(\\.\\d{1,2})?$";
            this.customTextBoxTrAmount.Leave += new System.EventHandler(this.customTextBoxTrAmount_Leave);
            // 
            // customTextBoxDestUserPwd
            // 
            this.customTextBoxDestUserPwd.CausesValidation = false;
            this.customTextBoxDestUserPwd.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDestUserPwd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDestUserPwd.Location = new System.Drawing.Point(290, 166);
            this.customTextBoxDestUserPwd.Name = "customTextBoxDestUserPwd";
            this.customTextBoxDestUserPwd.Required = true;
            this.customTextBoxDestUserPwd.ShortcutsEnabled = false;
            this.customTextBoxDestUserPwd.Size = new System.Drawing.Size(156, 21);
            this.customTextBoxDestUserPwd.TabIndex = 4;
            this.customTextBoxDestUserPwd.UseSystemPasswordChar = true;
            this.customTextBoxDestUserPwd.TextChanged += new System.EventHandler(this.customTextBoxDestUserPwd_TextChanged);
            // 
            // labelCashDrawerName
            // 
            this.labelCashDrawerName.AutoSize = true;
            this.labelCashDrawerName.BackColor = System.Drawing.Color.Transparent;
            this.labelCashDrawerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCashDrawerName.ForeColor = System.Drawing.Color.Black;
            this.labelCashDrawerName.Location = new System.Drawing.Point(267, 28);
            this.labelCashDrawerName.Name = "labelCashDrawerName";
            this.labelCashDrawerName.Size = new System.Drawing.Size(13, 13);
            this.labelCashDrawerName.TabIndex = 106;
            this.labelCashDrawerName.Text = "1";
            // 
            // customLabelAmtHeading
            // 
            this.customLabelAmtHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAmtHeading.Location = new System.Drawing.Point(137, 51);
            this.customLabelAmtHeading.Name = "customLabelAmtHeading";
            this.customLabelAmtHeading.Required = true;
            this.customLabelAmtHeading.Size = new System.Drawing.Size(100, 23);
            this.customLabelAmtHeading.TabIndex = 113;
            this.customLabelAmtHeading.Text = "Transfer Amount:";
            // 
            // customLabel5
            // 
            this.customLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel5.Location = new System.Drawing.Point(124, 139);
            this.customLabel5.Name = "customLabel5";
            this.customLabel5.Required = true;
            this.customLabel5.Size = new System.Drawing.Size(109, 21);
            this.customLabel5.TabIndex = 114;
            this.customLabel5.Text = "Receiving Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 103;
            this.label2.Text = "Destination";
            // 
            // customLabel4
            // 
            this.customLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel4.Location = new System.Drawing.Point(95, 160);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.Required = true;
            this.customLabel4.Size = new System.Drawing.Size(151, 23);
            this.customLabel4.TabIndex = 115;
            this.customLabel4.Text = "Receiving User\'s Password:";
            // 
            // customLabelCashDrawer
            // 
            this.customLabelCashDrawer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCashDrawer.Location = new System.Drawing.Point(92, 28);
            this.customLabelCashDrawer.Name = "customLabelCashDrawer";
            this.customLabelCashDrawer.Size = new System.Drawing.Size(145, 23);
            this.customLabelCashDrawer.TabIndex = 116;
            this.customLabelCashDrawer.Text = "Transfer From Cash Drawer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 100;
            this.label1.Text = "Source";
            // 
            // InternalTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(777, 673);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelCurrency);
            this.Controls.Add(this.labelTransferDate);
            this.Controls.Add(this.labelHeading2);
            this.Controls.Add(this.labelHeading1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InternalTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BankTransferInitial";
            this.Load += new System.EventHandler(this.InternalTransfer_Load);
            this.panelCurrency.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading1;
        private System.Windows.Forms.Label labelHeading2;
        private System.Windows.Forms.Label labelTransferDate;
        private System.Windows.Forms.Panel panelCurrency;
        private System.Windows.Forms.Panel panel1;
        private CustomLabel customLabelToCashDrawer;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxCashDrawerData;
        private CustomTextBox customTextBoxTrAmount;
        private System.Windows.Forms.Label labelCashDrawerName;
        private CustomLabel customLabelAmtHeading;
        private System.Windows.Forms.Label label2;
        private CustomLabel customLabelCashDrawer;
        private System.Windows.Forms.Label label1;
        private CurrencyEntry currencyEntry1;
        private CustomTextBox customTextBoxDestUser;
        private CustomTextBox customTextBoxDestUserPwd;
        private CustomLabel customLabel5;
        private CustomLabel customLabel4;

    }
}
