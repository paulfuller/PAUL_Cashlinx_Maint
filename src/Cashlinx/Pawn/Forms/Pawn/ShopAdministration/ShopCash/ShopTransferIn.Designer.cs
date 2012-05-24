using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    partial class ShopTransferIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopTransferIn));
            this.label1 = new System.Windows.Forms.Label();
            this.labelTransferNumber = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTransferDate = new System.Windows.Forms.Label();
            this.panelCurrency = new System.Windows.Forms.Panel();
            this.currencyEntry1 = new CurrencyEntry();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customTextBoxComment = new CustomTextBox();
            this.labelDestShop = new System.Windows.Forms.Label();
            this.customLabelComment = new CustomLabel();
            this.customButtonAccept = new CustomButton();
            this.customButtonReject = new CustomButton();
            this.customButtonBack = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTrAmount = new System.Windows.Forms.Label();
            this.labelTrAmtHeading = new System.Windows.Forms.Label();
            this.richTextBoxDestComment = new System.Windows.Forms.RichTextBox();
            this.labelDestCommentHdng = new System.Windows.Forms.Label();
            this.labelDestMgrName = new System.Windows.Forms.Label();
            this.labelDestMgrHdng = new System.Windows.Forms.Label();
            this.labelDestPhone = new System.Windows.Forms.Label();
            this.labelDestAddr2 = new System.Windows.Forms.Label();
            this.labelDestAddr1 = new System.Windows.Forms.Label();
            this.labelDestinationShop = new System.Windows.Forms.Label();
            this.labelDestination = new System.Windows.Forms.Label();
            this.labelSourceComment = new System.Windows.Forms.Label();
            this.labelCommentHdng = new System.Windows.Forms.Label();
            this.labelBagNo = new System.Windows.Forms.Label();
            this.labelBagHeading = new System.Windows.Forms.Label();
            this.labelTransporterName = new System.Windows.Forms.Label();
            this.labelTransportedHdng = new System.Windows.Forms.Label();
            this.labelManagerName = new System.Windows.Forms.Label();
            this.labelManagerHdng = new System.Windows.Forms.Label();
            this.labelSourceShopPhone = new System.Windows.Forms.Label();
            this.labelSourceShopAddr2 = new System.Windows.Forms.Label();
            this.labelSourceShopAddr1 = new System.Windows.Forms.Label();
            this.labelSourceShop = new System.Windows.Forms.Label();
            this.labelSourceShopNo = new System.Windows.Forms.Label();
            this.labelSource = new System.Windows.Forms.Label();
            this.panelCurrency.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shop Transfer In";
            // 
            // labelTransferNumber
            // 
            this.labelTransferNumber.AutoSize = true;
            this.labelTransferNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferNumber.ForeColor = System.Drawing.Color.White;
            this.labelTransferNumber.Location = new System.Drawing.Point(152, 37);
            this.labelTransferNumber.Name = "labelTransferNumber";
            this.labelTransferNumber.Size = new System.Drawing.Size(48, 16);
            this.labelTransferNumber.TabIndex = 1;
            this.labelTransferNumber.Text = "99999";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.White;
            this.labelStatus.Location = new System.Drawing.Point(206, 37);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(65, 16);
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Pending";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(501, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Transfer Date:";
            // 
            // labelTransferDate
            // 
            this.labelTransferDate.AutoSize = true;
            this.labelTransferDate.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferDate.ForeColor = System.Drawing.Color.White;
            this.labelTransferDate.Location = new System.Drawing.Point(614, 37);
            this.labelTransferDate.Name = "labelTransferDate";
            this.labelTransferDate.Size = new System.Drawing.Size(92, 16);
            this.labelTransferDate.TabIndex = 4;
            this.labelTransferDate.Text = "mm/dd/yyyy";
            // 
            // panelCurrency
            // 
            this.panelCurrency.BackColor = System.Drawing.Color.Transparent;
            this.panelCurrency.Controls.Add(this.currencyEntry1);
            this.panelCurrency.Location = new System.Drawing.Point(6, 111);
            this.panelCurrency.Name = "panelCurrency";
            this.panelCurrency.Size = new System.Drawing.Size(755, 303);
            this.panelCurrency.TabIndex = 5;
            // 
            // currencyEntry1
            // 
            this.currencyEntry1.BackColor = System.Drawing.Color.Transparent;
            this.currencyEntry1.Location = new System.Drawing.Point(3, 0);
            this.currencyEntry1.Name = "currencyEntry1";
            this.currencyEntry1.Size = new System.Drawing.Size(749, 303);
            this.currencyEntry1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.customTextBoxComment);
            this.panel1.Controls.Add(this.labelDestShop);
            this.panel1.Controls.Add(this.customLabelComment);
            this.panel1.Controls.Add(this.customButtonAccept);
            this.panel1.Controls.Add(this.customButtonReject);
            this.panel1.Controls.Add(this.customButtonBack);
            this.panel1.Controls.Add(this.customButtonCancel);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.labelTrAmount);
            this.panel1.Controls.Add(this.labelTrAmtHeading);
            this.panel1.Controls.Add(this.richTextBoxDestComment);
            this.panel1.Controls.Add(this.labelDestCommentHdng);
            this.panel1.Controls.Add(this.labelDestMgrName);
            this.panel1.Controls.Add(this.labelDestMgrHdng);
            this.panel1.Controls.Add(this.labelDestPhone);
            this.panel1.Controls.Add(this.labelDestAddr2);
            this.panel1.Controls.Add(this.labelDestAddr1);
            this.panel1.Controls.Add(this.labelDestinationShop);
            this.panel1.Controls.Add(this.labelDestination);
            this.panel1.Controls.Add(this.labelSourceComment);
            this.panel1.Controls.Add(this.labelCommentHdng);
            this.panel1.Controls.Add(this.labelBagNo);
            this.panel1.Controls.Add(this.labelBagHeading);
            this.panel1.Controls.Add(this.labelTransporterName);
            this.panel1.Controls.Add(this.labelTransportedHdng);
            this.panel1.Controls.Add(this.labelManagerName);
            this.panel1.Controls.Add(this.labelManagerHdng);
            this.panel1.Controls.Add(this.labelSourceShopPhone);
            this.panel1.Controls.Add(this.labelSourceShopAddr2);
            this.panel1.Controls.Add(this.labelSourceShopAddr1);
            this.panel1.Controls.Add(this.labelSourceShop);
            this.panel1.Controls.Add(this.labelSourceShopNo);
            this.panel1.Controls.Add(this.labelSource);
            this.panel1.Location = new System.Drawing.Point(6, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 346);
            this.panel1.TabIndex = 6;
            // 
            // customTextBoxComment
            // 
            this.customTextBoxComment.CausesValidation = false;
            this.customTextBoxComment.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxComment.Location = new System.Drawing.Point(498, 231);
            this.customTextBoxComment.Name = "customTextBoxComment";
            this.customTextBoxComment.Size = new System.Drawing.Size(202, 21);
            this.customTextBoxComment.TabIndex = 154;
            this.customTextBoxComment.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxComment.Visible = false;
            // 
            // labelDestShop
            // 
            this.labelDestShop.AutoSize = true;
            this.labelDestShop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestShop.Location = new System.Drawing.Point(466, 37);
            this.labelDestShop.Name = "labelDestShop";
            this.labelDestShop.Size = new System.Drawing.Size(37, 13);
            this.labelDestShop.TabIndex = 31;
            this.labelDestShop.Text = "02030";
            // 
            // customLabelComment
            // 
            this.customLabelComment.BackColor = System.Drawing.Color.Transparent;
            this.customLabelComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelComment.Location = new System.Drawing.Point(418, 234);
            this.customLabelComment.Name = "customLabelComment";
            this.customLabelComment.Size = new System.Drawing.Size(85, 13);
            this.customLabelComment.TabIndex = 153;
            this.customLabelComment.Text = "Void Comment:";
            this.customLabelComment.Visible = false;
            // 
            // customButtonAccept
            // 
            this.customButtonAccept.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAccept.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonAccept.BackgroundImage")));
            this.customButtonAccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAccept.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAccept.FlatAppearance.BorderSize = 0;
            this.customButtonAccept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAccept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAccept.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAccept.ForeColor = System.Drawing.Color.White;
            this.customButtonAccept.Location = new System.Drawing.Point(630, 287);
            this.customButtonAccept.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAccept.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAccept.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAccept.Name = "customButtonAccept";
            this.customButtonAccept.Size = new System.Drawing.Size(100, 50);
            this.customButtonAccept.TabIndex = 30;
            this.customButtonAccept.Text = "Accept";
            this.customButtonAccept.UseVisualStyleBackColor = false;
            this.customButtonAccept.Click += new System.EventHandler(this.customButtonAccept_Click);
            // 
            // customButtonReject
            // 
            this.customButtonReject.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReject.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonReject.BackgroundImage")));
            this.customButtonReject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReject.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReject.FlatAppearance.BorderSize = 0;
            this.customButtonReject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReject.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReject.ForeColor = System.Drawing.Color.White;
            this.customButtonReject.Location = new System.Drawing.Point(505, 287);
            this.customButtonReject.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReject.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReject.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReject.Name = "customButtonReject";
            this.customButtonReject.Size = new System.Drawing.Size(100, 50);
            this.customButtonReject.TabIndex = 29;
            this.customButtonReject.Text = "Reject";
            this.customButtonReject.UseVisualStyleBackColor = false;
            this.customButtonReject.Click += new System.EventHandler(this.customButtonReject_Click);
            // 
            // customButtonBack
            // 
            this.customButtonBack.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBack.BackgroundImage")));
            this.customButtonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBack.FlatAppearance.BorderSize = 0;
            this.customButtonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBack.ForeColor = System.Drawing.Color.White;
            this.customButtonBack.Location = new System.Drawing.Point(139, 287);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 28;
            this.customButtonBack.Text = "Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Click += new System.EventHandler(this.customButtonBack_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(10, 287);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 27;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Common.Properties.Resources.plus_icon_small;
            this.pictureBox1.Location = new System.Drawing.Point(389, 227);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelTrAmount
            // 
            this.labelTrAmount.AutoSize = true;
            this.labelTrAmount.Location = new System.Drawing.Point(318, 227);
            this.labelTrAmount.Name = "labelTrAmount";
            this.labelTrAmount.Size = new System.Drawing.Size(37, 13);
            this.labelTrAmount.TabIndex = 25;
            this.labelTrAmount.Text = "$1000";
            // 
            // labelTrAmtHeading
            // 
            this.labelTrAmtHeading.AutoSize = true;
            this.labelTrAmtHeading.Location = new System.Drawing.Point(224, 227);
            this.labelTrAmtHeading.Name = "labelTrAmtHeading";
            this.labelTrAmtHeading.Size = new System.Drawing.Size(88, 13);
            this.labelTrAmtHeading.TabIndex = 24;
            this.labelTrAmtHeading.Text = "Transfer Amount:";
            // 
            // richTextBoxDestComment
            // 
            this.richTextBoxDestComment.Location = new System.Drawing.Point(469, 162);
            this.richTextBoxDestComment.Name = "richTextBoxDestComment";
            this.richTextBoxDestComment.Size = new System.Drawing.Size(211, 55);
            this.richTextBoxDestComment.TabIndex = 23;
            this.richTextBoxDestComment.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // labelDestCommentHdng
            // 
            this.labelDestCommentHdng.AutoSize = true;
            this.labelDestCommentHdng.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestCommentHdng.Location = new System.Drawing.Point(395, 165);
            this.labelDestCommentHdng.Name = "labelDestCommentHdng";
            this.labelDestCommentHdng.Size = new System.Drawing.Size(54, 13);
            this.labelDestCommentHdng.TabIndex = 22;
            this.labelDestCommentHdng.Text = "Comment:";
            // 
            // labelDestMgrName
            // 
            this.labelDestMgrName.AutoSize = true;
            this.labelDestMgrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestMgrName.Location = new System.Drawing.Point(464, 119);
            this.labelDestMgrName.Name = "labelDestMgrName";
            this.labelDestMgrName.Size = new System.Drawing.Size(80, 13);
            this.labelDestMgrName.TabIndex = 21;
            this.labelDestMgrName.Text = "Manager Name";
            // 
            // labelDestMgrHdng
            // 
            this.labelDestMgrHdng.AutoSize = true;
            this.labelDestMgrHdng.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestMgrHdng.Location = new System.Drawing.Point(393, 119);
            this.labelDestMgrHdng.Name = "labelDestMgrHdng";
            this.labelDestMgrHdng.Size = new System.Drawing.Size(52, 13);
            this.labelDestMgrHdng.TabIndex = 20;
            this.labelDestMgrHdng.Text = "Manager:";
            // 
            // labelDestPhone
            // 
            this.labelDestPhone.AutoSize = true;
            this.labelDestPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestPhone.Location = new System.Drawing.Point(392, 101);
            this.labelDestPhone.Name = "labelDestPhone";
            this.labelDestPhone.Size = new System.Drawing.Size(38, 13);
            this.labelDestPhone.TabIndex = 19;
            this.labelDestPhone.Text = "Phone";
            // 
            // labelDestAddr2
            // 
            this.labelDestAddr2.AutoSize = true;
            this.labelDestAddr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestAddr2.Location = new System.Drawing.Point(392, 82);
            this.labelDestAddr2.Name = "labelDestAddr2";
            this.labelDestAddr2.Size = new System.Drawing.Size(77, 13);
            this.labelDestAddr2.TabIndex = 18;
            this.labelDestAddr2.Text = "Address Line 2";
            // 
            // labelDestAddr1
            // 
            this.labelDestAddr1.AutoSize = true;
            this.labelDestAddr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestAddr1.Location = new System.Drawing.Point(392, 64);
            this.labelDestAddr1.Name = "labelDestAddr1";
            this.labelDestAddr1.Size = new System.Drawing.Size(77, 13);
            this.labelDestAddr1.TabIndex = 17;
            this.labelDestAddr1.Text = "Address Line 1";
            // 
            // labelDestinationShop
            // 
            this.labelDestinationShop.AutoSize = true;
            this.labelDestinationShop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestinationShop.Location = new System.Drawing.Point(392, 37);
            this.labelDestinationShop.Name = "labelDestinationShop";
            this.labelDestinationShop.Size = new System.Drawing.Size(42, 13);
            this.labelDestinationShop.TabIndex = 15;
            this.labelDestinationShop.Text = "Shop #";
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestination.Location = new System.Drawing.Point(392, 10);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(86, 16);
            this.labelDestination.TabIndex = 14;
            this.labelDestination.Text = "Destination";
            // 
            // labelSourceComment
            // 
            this.labelSourceComment.AutoSize = true;
            this.labelSourceComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceComment.Location = new System.Drawing.Point(107, 187);
            this.labelSourceComment.Name = "labelSourceComment";
            this.labelSourceComment.Size = new System.Drawing.Size(116, 13);
            this.labelSourceComment.TabIndex = 13;
            this.labelSourceComment.Text = "Source Shop Comment";
            // 
            // labelCommentHdng
            // 
            this.labelCommentHdng.AutoSize = true;
            this.labelCommentHdng.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCommentHdng.Location = new System.Drawing.Point(35, 187);
            this.labelCommentHdng.Name = "labelCommentHdng";
            this.labelCommentHdng.Size = new System.Drawing.Size(54, 13);
            this.labelCommentHdng.TabIndex = 12;
            this.labelCommentHdng.Text = "Comment:";
            // 
            // labelBagNo
            // 
            this.labelBagNo.AutoSize = true;
            this.labelBagNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBagNo.Location = new System.Drawing.Point(88, 163);
            this.labelBagNo.Name = "labelBagNo";
            this.labelBagNo.Size = new System.Drawing.Size(43, 13);
            this.labelBagNo.TabIndex = 11;
            this.labelBagNo.Text = "Bag No";
            // 
            // labelBagHeading
            // 
            this.labelBagHeading.AutoSize = true;
            this.labelBagHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBagHeading.Location = new System.Drawing.Point(36, 163);
            this.labelBagHeading.Name = "labelBagHeading";
            this.labelBagHeading.Size = new System.Drawing.Size(39, 13);
            this.labelBagHeading.TabIndex = 10;
            this.labelBagHeading.Text = "Bag #:";
            // 
            // labelTransporterName
            // 
            this.labelTransporterName.AutoSize = true;
            this.labelTransporterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransporterName.Location = new System.Drawing.Point(146, 141);
            this.labelTransporterName.Name = "labelTransporterName";
            this.labelTransporterName.Size = new System.Drawing.Size(92, 13);
            this.labelTransporterName.TabIndex = 9;
            this.labelTransporterName.Text = "Transporter Name";
            // 
            // labelTransportedHdng
            // 
            this.labelTransportedHdng.AutoSize = true;
            this.labelTransportedHdng.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransportedHdng.Location = new System.Drawing.Point(36, 141);
            this.labelTransportedHdng.Name = "labelTransportedHdng";
            this.labelTransportedHdng.Size = new System.Drawing.Size(82, 13);
            this.labelTransportedHdng.TabIndex = 8;
            this.labelTransportedHdng.Text = "Transported By:";
            // 
            // labelManagerName
            // 
            this.labelManagerName.AutoSize = true;
            this.labelManagerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelManagerName.Location = new System.Drawing.Point(107, 119);
            this.labelManagerName.Name = "labelManagerName";
            this.labelManagerName.Size = new System.Drawing.Size(80, 13);
            this.labelManagerName.TabIndex = 7;
            this.labelManagerName.Text = "Manager Name";
            // 
            // labelManagerHdng
            // 
            this.labelManagerHdng.AutoSize = true;
            this.labelManagerHdng.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelManagerHdng.Location = new System.Drawing.Point(36, 119);
            this.labelManagerHdng.Name = "labelManagerHdng";
            this.labelManagerHdng.Size = new System.Drawing.Size(52, 13);
            this.labelManagerHdng.TabIndex = 6;
            this.labelManagerHdng.Text = "Manager:";
            // 
            // labelSourceShopPhone
            // 
            this.labelSourceShopPhone.AutoSize = true;
            this.labelSourceShopPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopPhone.Location = new System.Drawing.Point(35, 101);
            this.labelSourceShopPhone.Name = "labelSourceShopPhone";
            this.labelSourceShopPhone.Size = new System.Drawing.Size(38, 13);
            this.labelSourceShopPhone.TabIndex = 5;
            this.labelSourceShopPhone.Text = "Phone";
            // 
            // labelSourceShopAddr2
            // 
            this.labelSourceShopAddr2.AutoSize = true;
            this.labelSourceShopAddr2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopAddr2.Location = new System.Drawing.Point(35, 82);
            this.labelSourceShopAddr2.Name = "labelSourceShopAddr2";
            this.labelSourceShopAddr2.Size = new System.Drawing.Size(77, 13);
            this.labelSourceShopAddr2.TabIndex = 4;
            this.labelSourceShopAddr2.Text = "Address Line 2";
            // 
            // labelSourceShopAddr1
            // 
            this.labelSourceShopAddr1.AutoSize = true;
            this.labelSourceShopAddr1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopAddr1.Location = new System.Drawing.Point(35, 64);
            this.labelSourceShopAddr1.Name = "labelSourceShopAddr1";
            this.labelSourceShopAddr1.Size = new System.Drawing.Size(77, 13);
            this.labelSourceShopAddr1.TabIndex = 3;
            this.labelSourceShopAddr1.Text = "Address Line 1";
            // 
            // labelSourceShop
            // 
            this.labelSourceShop.AutoSize = true;
            this.labelSourceShop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShop.Location = new System.Drawing.Point(91, 37);
            this.labelSourceShop.Name = "labelSourceShop";
            this.labelSourceShop.Size = new System.Drawing.Size(37, 13);
            this.labelSourceShop.TabIndex = 2;
            this.labelSourceShop.Text = "02030";
            // 
            // labelSourceShopNo
            // 
            this.labelSourceShopNo.AutoSize = true;
            this.labelSourceShopNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopNo.Location = new System.Drawing.Point(35, 37);
            this.labelSourceShopNo.Name = "labelSourceShopNo";
            this.labelSourceShopNo.Size = new System.Drawing.Size(42, 13);
            this.labelSourceShopNo.TabIndex = 1;
            this.labelSourceShopNo.Text = "Shop #";
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSource.Location = new System.Drawing.Point(35, 10);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(57, 16);
            this.labelSource.TabIndex = 0;
            this.labelSource.Text = "Source";
            // 
            // ShopTransferIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(773, 775);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelCurrency);
            this.Controls.Add(this.labelTransferDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelTransferNumber);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShopTransferIn";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShopTransferIn";
            this.Load += new System.EventHandler(this.ShopTransferIn_Load);
            this.panelCurrency.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTransferNumber;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTransferDate;
        private System.Windows.Forms.Panel panelCurrency;
        private CurrencyEntry currencyEntry1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelSourceShopAddr1;
        private System.Windows.Forms.Label labelSourceShop;
        private System.Windows.Forms.Label labelSourceShopNo;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.Label labelSourceComment;
        private System.Windows.Forms.Label labelCommentHdng;
        private System.Windows.Forms.Label labelBagNo;
        private System.Windows.Forms.Label labelBagHeading;
        private System.Windows.Forms.Label labelTransporterName;
        private System.Windows.Forms.Label labelTransportedHdng;
        private System.Windows.Forms.Label labelManagerName;
        private System.Windows.Forms.Label labelManagerHdng;
        private System.Windows.Forms.Label labelSourceShopPhone;
        private System.Windows.Forms.Label labelSourceShopAddr2;
        private System.Windows.Forms.Label labelTrAmount;
        private System.Windows.Forms.Label labelTrAmtHeading;
        private System.Windows.Forms.RichTextBox richTextBoxDestComment;
        private System.Windows.Forms.Label labelDestCommentHdng;
        private System.Windows.Forms.Label labelDestMgrName;
        private System.Windows.Forms.Label labelDestMgrHdng;
        private System.Windows.Forms.Label labelDestPhone;
        private System.Windows.Forms.Label labelDestAddr2;
        private System.Windows.Forms.Label labelDestAddr1;
        private System.Windows.Forms.Label labelDestinationShop;
        private CustomButton customButtonAccept;
        private CustomButton customButtonReject;
        private CustomButton customButtonBack;
        private CustomButton customButtonCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelDestShop;
        private CustomTextBox customTextBoxComment;
        private CustomLabel customLabelComment;
    }
}