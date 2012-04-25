using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    partial class BankTransferInitial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankTransferInitial));
            this.labelHeading1 = new System.Windows.Forms.Label();
            this.labelHeading2 = new System.Windows.Forms.Label();
            this.labelTransferDate = new System.Windows.Forms.Label();
            this.panelCurrency = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxBankData = new System.Windows.Forms.ComboBox();
            this.labelCashDrawerName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customButton2 = new CustomButton();
            this.customButton1 = new CustomButton();
            this.customTextBox30 = new CustomTextBox();
            this.customTextBox29 = new CustomTextBox();
            this.customLabel6 = new CustomLabel();
            this.customLabel5 = new CustomLabel();
            this.customLabel4 = new CustomLabel();
            this.customLabel3 = new CustomLabel();
            this.currencyEntry1 = new CurrencyEntry();
            this.panelCurrency.SuspendLayout();
            this.panel2.SuspendLayout();
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.customButton2);
            this.panel2.Controls.Add(this.customButton1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.comboBoxBankData);
            this.panel2.Controls.Add(this.customTextBox30);
            this.panel2.Controls.Add(this.customTextBox29);
            this.panel2.Controls.Add(this.labelCashDrawerName);
            this.panel2.Controls.Add(this.customLabel6);
            this.panel2.Controls.Add(this.customLabel5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.customLabel4);
            this.panel2.Controls.Add(this.customLabel3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(12, 414);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(735, 216);
            this.panel2.TabIndex = 97;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Common.Properties.Resources.minus_icon_small;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(356, 52);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 97;
            this.pictureBox1.TabStop = false;
            // 
            // comboBoxBankData
            // 
            this.comboBoxBankData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBankData.FormattingEnabled = true;
            this.comboBoxBankData.Location = new System.Drawing.Point(245, 111);
            this.comboBoxBankData.Name = "comboBoxBankData";
            this.comboBoxBankData.Size = new System.Drawing.Size(222, 21);
            this.comboBoxBankData.TabIndex = 96;
            // 
            // labelCashDrawerName
            // 
            this.labelCashDrawerName.AutoSize = true;
            this.labelCashDrawerName.BackColor = System.Drawing.Color.Transparent;
            this.labelCashDrawerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCashDrawerName.ForeColor = System.Drawing.Color.Black;
            this.labelCashDrawerName.Location = new System.Drawing.Point(242, 27);
            this.labelCashDrawerName.Name = "labelCashDrawerName";
            this.labelCashDrawerName.Size = new System.Drawing.Size(13, 13);
            this.labelCashDrawerName.TabIndex = 54;
            this.labelCashDrawerName.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 51;
            this.label2.Text = "Destination";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source";
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.Color.Transparent;
            this.customButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButton2.BackgroundImage")));
            this.customButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButton2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButton2.FlatAppearance.BorderSize = 0;
            this.customButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.White;
            this.customButton2.Location = new System.Drawing.Point(590, 160);
            this.customButton2.Margin = new System.Windows.Forms.Padding(0);
            this.customButton2.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButton2.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButton2.Name = "customButton2";
            this.customButton2.Size = new System.Drawing.Size(100, 50);
            this.customButton2.TabIndex = 99;
            this.customButton2.Text = "Submit";
            this.customButton2.UseVisualStyleBackColor = false;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.Transparent;
            this.customButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButton1.BackgroundImage")));
            this.customButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButton1.FlatAppearance.BorderSize = 0;
            this.customButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.White;
            this.customButton1.Location = new System.Drawing.Point(13, 160);
            this.customButton1.Margin = new System.Windows.Forms.Padding(0);
            this.customButton1.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButton1.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(100, 50);
            this.customButton1.TabIndex = 98;
            this.customButton1.Text = "Cancel";
            this.customButton1.UseVisualStyleBackColor = false;
            // 
            // customTextBox30
            // 
            this.customTextBox30.CausesValidation = false;
            this.customTextBox30.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBox30.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBox30.Location = new System.Drawing.Point(245, 135);
            this.customTextBox30.Name = "customTextBox30";
            this.customTextBox30.ReadOnly = true;
            this.customTextBox30.Size = new System.Drawing.Size(75, 21);
            this.customTextBox30.TabIndex = 95;
            this.customTextBox30.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBox29
            // 
            this.customTextBox29.CausesValidation = false;
            this.customTextBox29.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBox29.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBox29.Location = new System.Drawing.Point(245, 49);
            this.customTextBox29.Name = "customTextBox29";
            this.customTextBox29.ReadOnly = true;
            this.customTextBox29.Size = new System.Drawing.Size(85, 21);
            this.customTextBox29.TabIndex = 94;
            this.customTextBox29.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabel6
            // 
            this.customLabel6.AutoSize = true;
            this.customLabel6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel6.Location = new System.Drawing.Point(110, 135);
            this.customLabel6.Name = "customLabel6";
            this.customLabel6.Required = true;
            this.customLabel6.Size = new System.Drawing.Size(108, 13);
            this.customLabel6.TabIndex = 53;
            this.customLabel6.Text = "Deposit Bag Number:";
            // 
            // customLabel5
            // 
            this.customLabel5.AutoSize = true;
            this.customLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel5.Location = new System.Drawing.Point(92, 111);
            this.customLabel5.Name = "customLabel5";
            this.customLabel5.Required = true;
            this.customLabel5.Size = new System.Drawing.Size(127, 13);
            this.customLabel5.TabIndex = 52;
            this.customLabel5.Text = "Bank Name and Account:";
            // 
            // customLabel4
            // 
            this.customLabel4.AutoSize = true;
            this.customLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel4.Location = new System.Drawing.Point(129, 52);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.Required = true;
            this.customLabel4.Size = new System.Drawing.Size(92, 13);
            this.customLabel4.TabIndex = 50;
            this.customLabel4.Text = "Transfer Amount:";
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel3.Location = new System.Drawing.Point(92, 28);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(144, 13);
            this.customLabel3.TabIndex = 49;
            this.customLabel3.Text = "Transfer From Cash Drawer:";
            // 
            // currencyEntry1
            // 
            this.currencyEntry1.BackColor = System.Drawing.Color.Transparent;
            this.currencyEntry1.Location = new System.Drawing.Point(0, -4);
            this.currencyEntry1.Name = "currencyEntry1";
            this.currencyEntry1.Size = new System.Drawing.Size(738, 319);
            this.currencyEntry1.TabIndex = 0;
            // 
            // BankTransferInitial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(777, 651);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelCurrency);
            this.Controls.Add(this.labelTransferDate);
            this.Controls.Add(this.labelHeading2);
            this.Controls.Add(this.labelHeading1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BankTransferInitial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BankTransferInitial";
            this.panelCurrency.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading1;
        private System.Windows.Forms.Label labelHeading2;
        private System.Windows.Forms.Label labelTransferDate;
        private System.Windows.Forms.Panel panelCurrency;
        private System.Windows.Forms.Panel panel2;
        private CustomLabel customLabel6;
        private CustomLabel customLabel5;
        private System.Windows.Forms.Label label2;
        private CustomLabel customLabel4;
        private CustomLabel customLabel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxBankData;
        private CustomTextBox customTextBox30;
        private CustomTextBox customTextBox29;
        private System.Windows.Forms.Label labelCashDrawerName;
        private CustomButton customButton2;
        private CustomButton customButton1;
        private CurrencyEntry currencyEntry1;

    }
}