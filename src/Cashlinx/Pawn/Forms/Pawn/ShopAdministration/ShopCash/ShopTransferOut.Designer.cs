using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    partial class ShopTransferOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopTransferOut));
            this.label1 = new System.Windows.Forms.Label();
            this.labelTransferStatus = new System.Windows.Forms.Label();
            this.labelTransferDate = new System.Windows.Forms.Label();
            this.labelHeading2 = new System.Windows.Forms.Label();
            this.panelCurrency = new System.Windows.Forms.Panel();
            this.currencyEntry1 = new CurrencyEntry();
            this.panel2 = new System.Windows.Forms.Panel();
            this.customTextBoxComment = new CustomTextBox();
            this.customLabelComment = new CustomLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.customLabel2ShopNo = new CustomLabel();
            this.customTextBoxTrAmount = new CustomTextBox();
            this.customButtonSubmit = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.labelCashDrawerName = new System.Windows.Forms.Label();
            this.customTextBoxDestShopNo = new CustomTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.customLabelShopNo = new CustomLabel();
            this.labelDestManager = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelDestPhone = new System.Windows.Forms.Label();
            this.labelDestAddr2 = new System.Windows.Forms.Label();
            this.labelDestAddr1 = new System.Windows.Forms.Label();
            this.customButtonFind = new CustomButton();
            this.customTextBoxBagNo = new CustomTextBox();
            this.customLabel2 = new CustomLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.richTextBoxComment = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.labelSourceManager = new System.Windows.Forms.Label();
            this.customTextBoxTransporter = new CustomTextBox();
            this.customLabel1 = new CustomLabel();
            this.labelDrawerHeading = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelSourcePhone = new System.Windows.Forms.Label();
            this.labelSourceShopNo = new System.Windows.Forms.Label();
            this.labelSourceAddr2 = new System.Windows.Forms.Label();
            this.labelSourceAddr1 = new System.Windows.Forms.Label();
            this.labelSourceShopHdng = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTransferTimeHeading = new System.Windows.Forms.Label();
            this.labelTransferTime = new System.Windows.Forms.Label();
            this.panelCurrency.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shop Transfer Out";
            // 
            // labelTransferStatus
            // 
            this.labelTransferStatus.AutoSize = true;
            this.labelTransferStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferStatus.ForeColor = System.Drawing.Color.White;
            this.labelTransferStatus.Location = new System.Drawing.Point(169, 31);
            this.labelTransferStatus.Name = "labelTransferStatus";
            this.labelTransferStatus.Size = new System.Drawing.Size(65, 16);
            this.labelTransferStatus.TabIndex = 2;
            this.labelTransferStatus.Text = "Pending";
            // 
            // labelTransferDate
            // 
            this.labelTransferDate.AutoSize = true;
            this.labelTransferDate.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferDate.ForeColor = System.Drawing.Color.White;
            this.labelTransferDate.Location = new System.Drawing.Point(624, 31);
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
            this.labelHeading2.Location = new System.Drawing.Point(515, 31);
            this.labelHeading2.Name = "labelHeading2";
            this.labelHeading2.Size = new System.Drawing.Size(103, 16);
            this.labelHeading2.TabIndex = 3;
            this.labelHeading2.Text = "Transfer Date:";
            // 
            // panelCurrency
            // 
            this.panelCurrency.BackColor = System.Drawing.Color.Transparent;
            this.panelCurrency.Controls.Add(this.currencyEntry1);
            this.panelCurrency.Location = new System.Drawing.Point(3, 106);
            this.panelCurrency.Name = "panelCurrency";
            this.panelCurrency.Size = new System.Drawing.Size(762, 308);
            this.panelCurrency.TabIndex = 5;
            // 
            // currencyEntry1
            // 
            this.currencyEntry1.BackColor = System.Drawing.Color.Transparent;
            this.currencyEntry1.Location = new System.Drawing.Point(3, 2);
            this.currencyEntry1.Name = "currencyEntry1";
            this.currencyEntry1.Size = new System.Drawing.Size(742, 304);
            this.currencyEntry1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.customTextBoxComment);
            this.panel2.Controls.Add(this.customLabelComment);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.customLabel2ShopNo);
            this.panel2.Controls.Add(this.customTextBoxTrAmount);
            this.panel2.Controls.Add(this.customButtonSubmit);
            this.panel2.Controls.Add(this.customButtonCancel);
            this.panel2.Controls.Add(this.labelCashDrawerName);
            this.panel2.Controls.Add(this.customTextBoxDestShopNo);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.customLabelShopNo);
            this.panel2.Controls.Add(this.labelDestManager);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.labelDestPhone);
            this.panel2.Controls.Add(this.labelDestAddr2);
            this.panel2.Controls.Add(this.labelDestAddr1);
            this.panel2.Controls.Add(this.customButtonFind);
            this.panel2.Controls.Add(this.customTextBoxBagNo);
            this.panel2.Controls.Add(this.customLabel2);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.richTextBoxComment);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.labelSourceManager);
            this.panel2.Controls.Add(this.customTextBoxTransporter);
            this.panel2.Controls.Add(this.customLabel1);
            this.panel2.Controls.Add(this.labelDrawerHeading);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.labelSourcePhone);
            this.panel2.Controls.Add(this.labelSourceShopNo);
            this.panel2.Controls.Add(this.labelSourceAddr2);
            this.panel2.Controls.Add(this.labelSourceAddr1);
            this.panel2.Controls.Add(this.labelSourceShopHdng);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(7, 413);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 309);
            this.panel2.TabIndex = 6;
            // 
            // customTextBoxComment
            // 
            this.customTextBoxComment.CausesValidation = false;
            this.customTextBoxComment.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxComment.Location = new System.Drawing.Point(539, 200);
            this.customTextBoxComment.Name = "customTextBoxComment";
            this.customTextBoxComment.Size = new System.Drawing.Size(202, 21);
            this.customTextBoxComment.TabIndex = 150;
            this.customTextBoxComment.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxComment.Visible = false;
            // 
            // customLabelComment
            // 
            this.customLabelComment.BackColor = System.Drawing.Color.Transparent;
            this.customLabelComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelComment.Location = new System.Drawing.Point(427, 200);
            this.customLabelComment.Name = "customLabelComment";
            this.customLabelComment.Required = true;
            this.customLabelComment.Size = new System.Drawing.Size(83, 12);
            this.customLabelComment.TabIndex = 149;
            this.customLabelComment.Text = "Void Comment:";
            this.customLabelComment.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Common.Properties.Resources.plus_icon_small;
            this.pictureBox1.Location = new System.Drawing.Point(285, 141);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // customLabel2ShopNo
            // 
            this.customLabel2ShopNo.AutoSize = true;
            this.customLabel2ShopNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2ShopNo.Location = new System.Drawing.Point(433, 28);
            this.customLabel2ShopNo.Name = "customLabel2ShopNo";
            this.customLabel2ShopNo.Required = true;
            this.customLabel2ShopNo.Size = new System.Drawing.Size(46, 13);
            this.customLabel2ShopNo.TabIndex = 34;
            this.customLabel2ShopNo.Text = "Shop #:";
            // 
            // customTextBoxTrAmount
            // 
            this.customTextBoxTrAmount.AllowDecimalNumbers = true;
            this.customTextBoxTrAmount.CausesValidation = false;
            this.customTextBoxTrAmount.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxTrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxTrAmount.Location = new System.Drawing.Point(165, 141);
            this.customTextBoxTrAmount.Name = "customTextBoxTrAmount";
            this.customTextBoxTrAmount.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxTrAmount.TabIndex = 33;
            this.customTextBoxTrAmount.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxTrAmount.Leave += new System.EventHandler(this.customTextBoxTrAmount_Leave);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(611, 251);
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
            this.customButtonCancel.Location = new System.Drawing.Point(13, 251);
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
            // labelCashDrawerName
            // 
            this.labelCashDrawerName.AutoSize = true;
            this.labelCashDrawerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCashDrawerName.Location = new System.Drawing.Point(118, 124);
            this.labelCashDrawerName.Name = "labelCashDrawerName";
            this.labelCashDrawerName.Size = new System.Drawing.Size(13, 13);
            this.labelCashDrawerName.TabIndex = 25;
            this.labelCashDrawerName.Text = "1";
            // 
            // customTextBoxDestShopNo
            // 
            this.customTextBoxDestShopNo.AllowOnlyNumbers = true;
            this.customTextBoxDestShopNo.CausesValidation = false;
            this.customTextBoxDestShopNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDestShopNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDestShopNo.Location = new System.Drawing.Point(511, 23);
            this.customTextBoxDestShopNo.Name = "customTextBoxDestShopNo";
            this.customTextBoxDestShopNo.Required = true;
            this.customTextBoxDestShopNo.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxDestShopNo.TabIndex = 26;
            this.customTextBoxDestShopNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(434, 161);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 23;
            this.label18.Text = "Bag #:";
            // 
            // customLabelShopNo
            // 
            this.customLabelShopNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelShopNo.Location = new System.Drawing.Point(45, 145);
            this.customLabelShopNo.Name = "customLabelShopNo";
            this.customLabelShopNo.Required = true;
            this.customLabelShopNo.Size = new System.Drawing.Size(100, 23);
            this.customLabelShopNo.TabIndex = 27;
            this.customLabelShopNo.Text = "Transfer Amount:";
            // 
            // labelDestManager
            // 
            this.labelDestManager.AutoSize = true;
            this.labelDestManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestManager.Location = new System.Drawing.Point(504, 104);
            this.labelDestManager.Name = "labelDestManager";
            this.labelDestManager.Size = new System.Drawing.Size(80, 13);
            this.labelDestManager.TabIndex = 21;
            this.labelDestManager.Text = "Manager Name";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(434, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 20;
            this.label16.Text = "Manager :";
            // 
            // labelDestPhone
            // 
            this.labelDestPhone.AutoSize = true;
            this.labelDestPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestPhone.Location = new System.Drawing.Point(434, 85);
            this.labelDestPhone.Name = "labelDestPhone";
            this.labelDestPhone.Size = new System.Drawing.Size(66, 13);
            this.labelDestPhone.TabIndex = 19;
            this.labelDestPhone.Text = "Shop Phone";
            // 
            // labelDestAddr2
            // 
            this.labelDestAddr2.AutoSize = true;
            this.labelDestAddr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestAddr2.Location = new System.Drawing.Point(433, 66);
            this.labelDestAddr2.Name = "labelDestAddr2";
            this.labelDestAddr2.Size = new System.Drawing.Size(77, 13);
            this.labelDestAddr2.TabIndex = 18;
            this.labelDestAddr2.Text = "Address Line 2";
            // 
            // labelDestAddr1
            // 
            this.labelDestAddr1.AutoSize = true;
            this.labelDestAddr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestAddr1.Location = new System.Drawing.Point(433, 48);
            this.labelDestAddr1.Name = "labelDestAddr1";
            this.labelDestAddr1.Size = new System.Drawing.Size(77, 13);
            this.labelDestAddr1.TabIndex = 17;
            this.labelDestAddr1.Text = "Address Line 1";
            // 
            // customButtonFind
            // 
            this.customButtonFind.BackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonFind.BackgroundImage")));
            this.customButtonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonFind.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonFind.FlatAppearance.BorderSize = 0;
            this.customButtonFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonFind.ForeColor = System.Drawing.Color.White;
            this.customButtonFind.Location = new System.Drawing.Point(641, 9);
            this.customButtonFind.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonFind.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.Name = "customButtonFind";
            this.customButtonFind.Size = new System.Drawing.Size(100, 50);
            this.customButtonFind.TabIndex = 28;
            this.customButtonFind.Text = "Find";
            this.customButtonFind.UseVisualStyleBackColor = false;
            this.customButtonFind.Click += new System.EventHandler(this.customButtonFind_Click);
            // 
            // customTextBoxBagNo
            // 
            this.customTextBoxBagNo.CausesValidation = false;
            this.customTextBoxBagNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBagNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxBagNo.Location = new System.Drawing.Point(557, 163);
            this.customTextBoxBagNo.Name = "customTextBoxBagNo";
            this.customTextBoxBagNo.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxBagNo.TabIndex = 29;
            this.customTextBoxBagNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabel2
            // 
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(434, 132);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Required = true;
            this.customLabel2.Size = new System.Drawing.Size(100, 23);
            this.customLabel2.TabIndex = 30;
            this.customLabel2.Text = "Transported By:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(427, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 16);
            this.label12.TabIndex = 13;
            this.label12.Text = "Destination";
            // 
            // richTextBoxComment
            // 
            this.richTextBoxComment.Location = new System.Drawing.Point(165, 177);
            this.richTextBoxComment.Name = "richTextBoxComment";
            this.richTextBoxComment.Size = new System.Drawing.Size(205, 44);
            this.richTextBoxComment.TabIndex = 12;
            this.richTextBoxComment.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(48, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Comment:";
            // 
            // labelSourceManager
            // 
            this.labelSourceManager.AutoSize = true;
            this.labelSourceManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceManager.Location = new System.Drawing.Point(115, 102);
            this.labelSourceManager.Name = "labelSourceManager";
            this.labelSourceManager.Size = new System.Drawing.Size(80, 13);
            this.labelSourceManager.TabIndex = 10;
            this.labelSourceManager.Text = "Manager Name";
            // 
            // customTextBoxTransporter
            // 
            this.customTextBoxTransporter.CausesValidation = false;
            this.customTextBoxTransporter.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxTransporter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxTransporter.Location = new System.Drawing.Point(557, 134);
            this.customTextBoxTransporter.Name = "customTextBoxTransporter";
            this.customTextBoxTransporter.Size = new System.Drawing.Size(111, 21);
            this.customTextBoxTransporter.TabIndex = 31;
            this.customTextBoxTransporter.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabel1
            // 
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(13, 140);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(100, 23);
            this.customLabel1.TabIndex = 32;
            // 
            // labelDrawerHeading
            // 
            this.labelDrawerHeading.AutoSize = true;
            this.labelDrawerHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDrawerHeading.Location = new System.Drawing.Point(45, 124);
            this.labelDrawerHeading.Name = "labelDrawerHeading";
            this.labelDrawerHeading.Size = new System.Drawing.Size(54, 13);
            this.labelDrawerHeading.TabIndex = 7;
            this.labelDrawerHeading.Text = "Drawer #:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(45, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Manager :";
            // 
            // labelSourcePhone
            // 
            this.labelSourcePhone.AutoSize = true;
            this.labelSourcePhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourcePhone.Location = new System.Drawing.Point(45, 85);
            this.labelSourcePhone.Name = "labelSourcePhone";
            this.labelSourcePhone.Size = new System.Drawing.Size(66, 13);
            this.labelSourcePhone.TabIndex = 5;
            this.labelSourcePhone.Text = "Shop Phone";
            // 
            // labelSourceShopNo
            // 
            this.labelSourceShopNo.AutoSize = true;
            this.labelSourceShopNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopNo.Location = new System.Drawing.Point(99, 29);
            this.labelSourceShopNo.Name = "labelSourceShopNo";
            this.labelSourceShopNo.Size = new System.Drawing.Size(42, 13);
            this.labelSourceShopNo.TabIndex = 4;
            this.labelSourceShopNo.Text = "02030";
            // 
            // labelSourceAddr2
            // 
            this.labelSourceAddr2.AutoSize = true;
            this.labelSourceAddr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceAddr2.Location = new System.Drawing.Point(45, 68);
            this.labelSourceAddr2.Name = "labelSourceAddr2";
            this.labelSourceAddr2.Size = new System.Drawing.Size(77, 13);
            this.labelSourceAddr2.TabIndex = 3;
            this.labelSourceAddr2.Text = "Address Line 2";
            // 
            // labelSourceAddr1
            // 
            this.labelSourceAddr1.AutoSize = true;
            this.labelSourceAddr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceAddr1.Location = new System.Drawing.Point(45, 51);
            this.labelSourceAddr1.Name = "labelSourceAddr1";
            this.labelSourceAddr1.Size = new System.Drawing.Size(77, 13);
            this.labelSourceAddr1.TabIndex = 2;
            this.labelSourceAddr1.Text = "Address Line 1";
            // 
            // labelSourceShopHdng
            // 
            this.labelSourceShopHdng.AutoSize = true;
            this.labelSourceShopHdng.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopHdng.Location = new System.Drawing.Point(45, 29);
            this.labelSourceShopHdng.Name = "labelSourceShopHdng";
            this.labelSourceShopHdng.Size = new System.Drawing.Size(42, 13);
            this.labelSourceShopHdng.TabIndex = 1;
            this.labelSourceShopHdng.Text = "Shop #";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Source";
            // 
            // labelTransferTimeHeading
            // 
            this.labelTransferTimeHeading.AutoSize = true;
            this.labelTransferTimeHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferTimeHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferTimeHeading.ForeColor = System.Drawing.Color.White;
            this.labelTransferTimeHeading.Location = new System.Drawing.Point(515, 55);
            this.labelTransferTimeHeading.Name = "labelTransferTimeHeading";
            this.labelTransferTimeHeading.Size = new System.Drawing.Size(101, 16);
            this.labelTransferTimeHeading.TabIndex = 7;
            this.labelTransferTimeHeading.Text = "Transfer Time:";
            // 
            // labelTransferTime
            // 
            this.labelTransferTime.AutoSize = true;
            this.labelTransferTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferTime.ForeColor = System.Drawing.Color.White;
            this.labelTransferTime.Location = new System.Drawing.Point(627, 55);
            this.labelTransferTime.Name = "labelTransferTime";
            this.labelTransferTime.Size = new System.Drawing.Size(70, 16);
            this.labelTransferTime.TabIndex = 8;
            this.labelTransferTime.Text = "10:00 AM";
            // 
            // ShopTransferOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(777, 734);
            this.Controls.Add(this.labelTransferTime);
            this.Controls.Add(this.labelTransferTimeHeading);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelCurrency);
            this.Controls.Add(this.labelTransferDate);
            this.Controls.Add(this.labelHeading2);
            this.Controls.Add(this.labelTransferStatus);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShopTransferOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShopTransferOut";
            this.Load += new System.EventHandler(this.ShopTransferOut_Load);
            this.panelCurrency.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTransferStatus;
        private System.Windows.Forms.Label labelTransferDate;
        private System.Windows.Forms.Label labelHeading2;
        private System.Windows.Forms.Panel panelCurrency;
        private CurrencyEntry currencyEntry1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSourceManager;
        private CustomTextBox customTextBoxTransporter;
        private CustomLabel customLabel1;
        private System.Windows.Forms.Label labelDrawerHeading;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelSourcePhone;
        private System.Windows.Forms.Label labelSourceShopNo;
        private System.Windows.Forms.Label labelSourceAddr2;
        private System.Windows.Forms.Label labelSourceAddr1;
        private System.Windows.Forms.Label labelSourceShopHdng;
        private System.Windows.Forms.Label label18;
        private CustomLabel customLabelShopNo;
        private System.Windows.Forms.Label labelDestManager;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labelDestPhone;
        private System.Windows.Forms.Label labelDestAddr2;
        private System.Windows.Forms.Label labelDestAddr1;
        private CustomButton customButtonFind;
        private CustomTextBox customTextBoxBagNo;
        private CustomLabel customLabel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox richTextBoxComment;
        private System.Windows.Forms.Label label11;
        private CustomButton customButtonSubmit;
        private CustomButton customButtonCancel;
        private System.Windows.Forms.Label labelCashDrawerName;
        private CustomTextBox customTextBoxDestShopNo;
        private CustomLabel customLabel2ShopNo;
        private CustomTextBox customTextBoxTrAmount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTransferTimeHeading;
        private System.Windows.Forms.Label labelTransferTime;
        private CustomTextBox customTextBoxComment;
        private CustomLabel customLabelComment;
    }
}