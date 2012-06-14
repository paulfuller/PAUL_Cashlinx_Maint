using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using Pawn.Forms;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Inquiry.GunBookInquiry
{
    partial class GunBookInquirySearch
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.Label label19;
            System.Windows.Forms.Label label20;
            System.Windows.Forms.Label label21;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GunBookInquirySearch));
            this.sortDir_cb = new System.Windows.Forms.ComboBox();
            this.sortBy_cb = new System.Windows.Forms.ComboBox();
            this.Clear_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.Find_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.Cancel_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.labelHeading = new System.Windows.Forms.Label();
            this.gunNumber = new System.Windows.Forms.TextBox();
            this.gunNumberTo = new System.Windows.Forms.TextBox();
            this.originalGunNumber = new System.Windows.Forms.TextBox();
            this.originalGunNumberTo = new System.Windows.Forms.TextBox();
            this.txtLoanTicketNumber = new System.Windows.Forms.TextBox();
            this.ucICN = new UserControls.ICN();
            this.txtCaliber = new System.Windows.Forms.TextBox();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.txtGunBookPageTo = new System.Windows.Forms.TextBox();
            this.txtGunBookPage = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Tahoma", 11F);
            label4.Location = new System.Drawing.Point(367, 650);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(90, 27);
            label4.TabIndex = 66;
            label4.Text = "Sort By:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Tahoma", 11F);
            label5.Location = new System.Drawing.Point(42, 127);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(143, 27);
            label5.TabIndex = 80;
            label5.Text = "Gun Number:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Font = new System.Drawing.Font("Tahoma", 11F);
            label7.Location = new System.Drawing.Point(472, 127);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(45, 27);
            label7.TabIndex = 82;
            label7.Text = "To:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Tahoma", 11F);
            label8.Location = new System.Drawing.Point(24, 172);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(225, 27);
            label8.TabIndex = 84;
            label8.Text = "Original Gun Number:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Font = new System.Drawing.Font("Tahoma", 11F);
            label9.Location = new System.Drawing.Point(472, 169);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(45, 27);
            label9.TabIndex = 86;
            label9.Text = "To:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = System.Drawing.Color.Transparent;
            label14.Font = new System.Drawing.Font("Tahoma", 11F);
            label14.Location = new System.Drawing.Point(33, 221);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(216, 27);
            label14.TabIndex = 88;
            label14.Text = "Loan Ticket Number:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = System.Drawing.Color.Transparent;
            label13.Font = new System.Drawing.Font("Tahoma", 11F);
            label13.Location = new System.Drawing.Point(42, 269);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(87, 27);
            label13.TabIndex = 91;
            label13.Text = "Caliber:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = System.Drawing.Color.Transparent;
            label15.Font = new System.Drawing.Font("Tahoma", 11F);
            label15.Location = new System.Drawing.Point(42, 317);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(141, 27);
            label15.TabIndex = 93;
            label15.Text = "Manufacturer";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = System.Drawing.Color.Transparent;
            label16.Font = new System.Drawing.Font("Tahoma", 11F);
            label16.Location = new System.Drawing.Point(472, 317);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(70, 27);
            label16.TabIndex = 95;
            label16.Text = "Model";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = System.Drawing.Color.Transparent;
            label17.Font = new System.Drawing.Font("Tahoma", 11F);
            label17.Location = new System.Drawing.Point(654, 317);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(150, 27);
            label17.TabIndex = 97;
            label17.Text = "Serial Number";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = System.Drawing.Color.Transparent;
            label18.Font = new System.Drawing.Font("Tahoma", 11F);
            label18.Location = new System.Drawing.Point(156, 365);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(60, 27);
            label18.TabIndex = 99;
            label18.Text = "Type";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = System.Drawing.Color.Transparent;
            label19.Font = new System.Drawing.Font("Tahoma", 11F);
            label19.Location = new System.Drawing.Point(156, 405);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(72, 27);
            label19.TabIndex = 101;
            label19.Text = "Status";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = System.Drawing.Color.Transparent;
            label20.Font = new System.Drawing.Font("Tahoma", 11F);
            label20.Location = new System.Drawing.Point(503, 448);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(45, 27);
            label20.TabIndex = 105;
            label20.Text = "To:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = System.Drawing.Color.Transparent;
            label21.Font = new System.Drawing.Font("Tahoma", 11F);
            label21.Location = new System.Drawing.Point(73, 448);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(169, 27);
            label21.TabIndex = 103;
            label21.Text = "Gun Book Page:";
            // 
            // sortDir_cb
            // 
            this.sortDir_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sortDir_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortDir_cb.ForeColor = System.Drawing.Color.Black;
            this.sortDir_cb.FormattingEnabled = true;
            this.sortDir_cb.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.sortDir_cb.Location = new System.Drawing.Point(605, 651);
            this.sortDir_cb.Name = "sortDir_cb";
            this.sortDir_cb.Size = new System.Drawing.Size(87, 29);
            this.sortDir_cb.TabIndex = 11;
            // 
            // sortBy_cb
            // 
            this.sortBy_cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.sortBy_cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.sortBy_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sortBy_cb.Cursor = System.Windows.Forms.Cursors.Default;
            this.sortBy_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortBy_cb.ForeColor = System.Drawing.Color.Black;
            this.sortBy_cb.FormattingEnabled = true;
            this.sortBy_cb.Location = new System.Drawing.Point(444, 651);
            this.sortBy_cb.Name = "sortBy_cb";
            this.sortBy_cb.Size = new System.Drawing.Size(144, 29);
            this.sortBy_cb.TabIndex = 10;
            // 
            // Clear_btn
            // 
            this.Clear_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Clear_btn.BackColor = System.Drawing.Color.Transparent;
            this.Clear_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Clear_btn.BackgroundImage")));
            this.Clear_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Clear_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Clear_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Clear_btn.FlatAppearance.BorderSize = 0;
            this.Clear_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Clear_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Clear_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clear_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear_btn.ForeColor = System.Drawing.Color.White;
            this.Clear_btn.Location = new System.Drawing.Point(591, 723);
            this.Clear_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Clear_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(100, 50);
            this.Clear_btn.TabIndex = 13;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = false;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // Find_btn
            // 
            this.Find_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Find_btn.BackColor = System.Drawing.Color.Transparent;
            this.Find_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Find_btn.BackgroundImage")));
            this.Find_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Find_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Find_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Find_btn.FlatAppearance.BorderSize = 0;
            this.Find_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Find_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Find_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Find_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Find_btn.ForeColor = System.Drawing.Color.White;
            this.Find_btn.Location = new System.Drawing.Point(712, 723);
            this.Find_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Find_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.Name = "Find_btn";
            this.Find_btn.Size = new System.Drawing.Size(100, 50);
            this.Find_btn.TabIndex = 14;
            this.Find_btn.Text = "Find";
            this.Find_btn.UseVisualStyleBackColor = false;
            this.Find_btn.Click += new System.EventHandler(this.Find_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel_btn.BackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel_btn.BackgroundImage")));
            this.Cancel_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Cancel_btn.FlatAppearance.BorderSize = 0;
            this.Cancel_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_btn.ForeColor = System.Drawing.Color.White;
            this.Cancel_btn.Location = new System.Drawing.Point(29, 723);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 12;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(600, 48);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(378, 29);
            this.labelHeading.TabIndex = 49;
            this.labelHeading.Text = "Gun Book Inquiry - Search Criteria";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelHeading.Click += new System.EventHandler(this.labelHeading_Click);
            // 
            // gunNumber
            // 
            this.gunNumber.Location = new System.Drawing.Point(253, 125);
            this.gunNumber.Name = "gunNumber";
            this.gunNumber.Size = new System.Drawing.Size(205, 27);
            this.gunNumber.TabIndex = 5;
            this.gunNumber.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // gunNumberTo
            // 
            this.gunNumberTo.Location = new System.Drawing.Point(509, 125);
            this.gunNumberTo.Name = "gunNumberTo";
            this.gunNumberTo.Size = new System.Drawing.Size(87, 27);
            this.gunNumberTo.TabIndex = 10;
            // 
            // originalGunNumber
            // 
            this.originalGunNumber.Location = new System.Drawing.Point(253, 174);
            this.originalGunNumber.Name = "originalGunNumber";
            this.originalGunNumber.Size = new System.Drawing.Size(205, 27);
            this.originalGunNumber.TabIndex = 15;
            this.originalGunNumber.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // originalGunNumberTo
            // 
            this.originalGunNumberTo.Location = new System.Drawing.Point(509, 167);
            this.originalGunNumberTo.Name = "originalGunNumberTo";
            this.originalGunNumberTo.Size = new System.Drawing.Size(87, 27);
            this.originalGunNumberTo.TabIndex = 20;
            // 
            // txtLoanTicketNumber
            // 
            this.txtLoanTicketNumber.Location = new System.Drawing.Point(253, 223);
            this.txtLoanTicketNumber.Name = "txtLoanTicketNumber";
            this.txtLoanTicketNumber.Size = new System.Drawing.Size(123, 27);
            this.txtLoanTicketNumber.TabIndex = 25;
            // 
            // ucICN
            // 
            this.ucICN.Location = new System.Drawing.Point(474, 220);
            this.ucICN.Name = "ucICN";
            this.ucICN.Size = new System.Drawing.Size(380, 48);
            this.ucICN.TabIndex = 30;
            // 
            // txtCaliber
            // 
            this.txtCaliber.Location = new System.Drawing.Point(262, 271);
            this.txtCaliber.Name = "txtCaliber";
            this.txtCaliber.Size = new System.Drawing.Size(123, 27);
            this.txtCaliber.TabIndex = 35;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Location = new System.Drawing.Point(253, 317);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(87, 27);
            this.txtManufacturer.TabIndex = 40;
            this.txtManufacturer.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(545, 317);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(87, 27);
            this.txtModel.TabIndex = 45;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Location = new System.Drawing.Point(810, 317);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(87, 27);
            this.txtSerialNumber.TabIndex = 50;
            // 
            // cbType
            // 
            this.cbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbType.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.ForeColor = System.Drawing.Color.Black;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            global::Pawn.Properties.Resources.OverrideMachineName,
            "RIFLE W/SC",
            "DERRINGER",
            "REVOLVER",
            "PISTOL",
            "RIFLE",
            "TARGET HAN",
            "SHOTGUN",
            "COMB. RIFL"});
            this.cbType.Location = new System.Drawing.Point(233, 366);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(144, 29);
            this.cbType.TabIndex = 55;
            // 
            // cbStatus
            // 
            this.cbStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.ForeColor = System.Drawing.Color.Black;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            global::Pawn.Properties.Resources.OverrideMachineName,
            "TEMP",
            "PENDING",
            "PICKEDUP",
            "IP",
            "PURCHASE",
            "LAYAWAY",
            "RELEASE TO CLAIMANT",
            "POLICE SEIZE",
            "PFI",
            "SOLD",
            "RETURN",
            "REFUND",
            "TO",
            "CHARGEOFF",
            "VOID",
            "REJ"});
            this.cbStatus.Location = new System.Drawing.Point(233, 406);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(144, 29);
            this.cbStatus.TabIndex = 60;
            // 
            // txtGunBookPageTo
            // 
            this.txtGunBookPageTo.Location = new System.Drawing.Point(540, 446);
            this.txtGunBookPageTo.Name = "txtGunBookPageTo";
            this.txtGunBookPageTo.Size = new System.Drawing.Size(87, 27);
            this.txtGunBookPageTo.TabIndex = 70;
            // 
            // txtGunBookPage
            // 
            this.txtGunBookPage.Location = new System.Drawing.Point(284, 446);
            this.txtGunBookPage.Name = "txtGunBookPage";
            this.txtGunBookPage.Size = new System.Drawing.Size(205, 27);
            this.txtGunBookPage.TabIndex = 65;
            // 
            // GunBookInquirySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.Cancel_btn;
            this.ClientSize = new System.Drawing.Size(1349, 776);
            this.ControlBox = false;
            this.Controls.Add(this.txtGunBookPageTo);
            this.Controls.Add(label20);
            this.Controls.Add(label21);
            this.Controls.Add(this.txtGunBookPage);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(label19);
            this.Controls.Add(this.cbType);
            this.Controls.Add(label18);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(label17);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(label16);
            this.Controls.Add(this.txtManufacturer);
            this.Controls.Add(label15);
            this.Controls.Add(this.txtCaliber);
            this.Controls.Add(label13);
            this.Controls.Add(this.ucICN);
            this.Controls.Add(this.txtLoanTicketNumber);
            this.Controls.Add(label14);
            this.Controls.Add(this.originalGunNumberTo);
            this.Controls.Add(label9);
            this.Controls.Add(label8);
            this.Controls.Add(this.originalGunNumber);
            this.Controls.Add(this.gunNumberTo);
            this.Controls.Add(label7);
            this.Controls.Add(label5);
            this.Controls.Add(this.gunNumber);
            this.Controls.Add(this.sortDir_cb);
            this.Controls.Add(this.sortBy_cb);
            this.Controls.Add(label4);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.Find_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GunBookInquirySearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GunBookInquirySearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox sortDir_cb;
        private System.Windows.Forms.ComboBox sortBy_cb;
        private CustomButton Clear_btn;
        private CustomButton Find_btn;
        private CustomButton Cancel_btn;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TextBox gunNumber;
        private System.Windows.Forms.TextBox gunNumberTo;
        private System.Windows.Forms.TextBox originalGunNumber;
        private System.Windows.Forms.TextBox originalGunNumberTo;
        private System.Windows.Forms.TextBox txtLoanTicketNumber;
        private ICN ucICN;
        private System.Windows.Forms.TextBox txtCaliber;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.TextBox txtGunBookPageTo;
        private System.Windows.Forms.TextBox txtGunBookPage;

    }
}