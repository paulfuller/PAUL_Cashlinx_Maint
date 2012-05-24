using Common.Libraries.Forms.Components;
using Pawn.Forms.Panels.MenuPanels;

namespace Pawn.Forms
{
    sealed partial class NewDesktop
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
            this.userInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.labelDateHeading = new System.Windows.Forms.Label();
            this.labelCashOver = new System.Windows.Forms.Label();
            this.userRoleField = new System.Windows.Forms.Label();
            this.userRoleLabel = new System.Windows.Forms.Label();
            this.userEmpIdField = new System.Windows.Forms.Label();
            this.userNameField = new System.Windows.Forms.Label();
            this.userEmpIdLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.shopTimeLabel = new System.Windows.Forms.Label();
            this.shopDateLabel = new System.Windows.Forms.Label();
            this.customerInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.customerAddressField = new System.Windows.Forms.Label();
            this.customerDOBField = new System.Windows.Forms.Label();
            this.customerNameField = new System.Windows.Forms.Label();
            this.customerDOBLabel = new System.Windows.Forms.Label();
            this.customerAddressLabel = new System.Windows.Forms.Label();
            this.customerNameLabel = new System.Windows.Forms.Label();
            this.versionLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.mainMenuPanel = new MainMenuPanel();
            this.pawnMenuPanel = new PawnMenuPanel();
            this.lookupMenuPanel = new LookupMenuPanel();
            this.customerHoldsMenuPanel = new CustomerHoldsMenuPanel();
            this.pfiMenuPanel = new PFIMenuPanel();
            this.policeMenuPanel = new PoliceMenuPanel();
            this.utilitiesMenuPanel = new UtilitiesMenuPanel();
            this.reportsMenuPanel = new ReportsMenuPanel();
            this.transferMenuPanel = new TransferMenuPanel();
            this.manageCashMenuPanel = new ManageCashMenuPanel();
            this.refundReturnMenuPanel = new RefundReturnMenuPanel();
            this.customerBuyMenuPanel = new CustomerBuyMenuPanel();
            this.buyMenuPanel = new BuyMenuPanel();
            this.voidMenuPanel = new VoidMenuPanel();
            this.manageInventoryMenuPanel = new ManageInventoryMenuPanel();
            this.cashDrawerMenuPanel = new CashDrawerMenuPanel();
            this.changePricingMenuPanel = new ChangePricingMenuPanel();
            this.gunBookMenuPanel = new GunBookMenuPanel();
            this.safeOperationsMenuPanel = new SafeOperationsMenuPanel();
            this.shopDateField = new System.Windows.Forms.Label();
            this.userInfoGroupBox.SuspendLayout();
            this.customerInfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // userInfoGroupBox
            // 
            this.userInfoGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.userInfoGroupBox.BackgroundImage = global::Pawn.Properties.Resources.blue_white_panel;
            this.userInfoGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.userInfoGroupBox.Controls.Add(this.shopDateField);
            this.userInfoGroupBox.Controls.Add(this.labelDateHeading);
            this.userInfoGroupBox.Controls.Add(this.labelCashOver);
            this.userInfoGroupBox.Controls.Add(this.userRoleField);
            this.userInfoGroupBox.Controls.Add(this.userRoleLabel);
            this.userInfoGroupBox.Controls.Add(this.userEmpIdField);
            this.userInfoGroupBox.Controls.Add(this.userNameField);
            this.userInfoGroupBox.Controls.Add(this.userEmpIdLabel);
            this.userInfoGroupBox.Controls.Add(this.userNameLabel);
            this.userInfoGroupBox.Enabled = false;
            this.userInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userInfoGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userInfoGroupBox.ForeColor = System.Drawing.Color.Black;
            this.userInfoGroupBox.Location = new System.Drawing.Point(7, -1);
            this.userInfoGroupBox.Name = "userInfoGroupBox";
            this.userInfoGroupBox.Size = new System.Drawing.Size(242, 113);
            this.userInfoGroupBox.TabIndex = 10;
            this.userInfoGroupBox.TabStop = false;
            this.userInfoGroupBox.Text = "User Info";
            this.userInfoGroupBox.Visible = false;
            // 
            // labelDateHeading
            // 
            this.labelDateHeading.AutoSize = true;
            this.labelDateHeading.Location = new System.Drawing.Point(50, 75);
            this.labelDateHeading.Name = "labelDateHeading";
            this.labelDateHeading.Size = new System.Drawing.Size(61, 13);
            this.labelDateHeading.TabIndex = 7;
            this.labelDateHeading.Text = "Shop Date:";
            // 
            // labelCashOver
            // 
            this.labelCashOver.AutoSize = true;
            this.labelCashOver.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCashOver.Location = new System.Drawing.Point(46, 93);
            this.labelCashOver.Name = "labelCashOver";
            this.labelCashOver.Size = new System.Drawing.Size(69, 13);
            this.labelCashOver.TabIndex = 6;
            this.labelCashOver.Text = "CASH OVER";
            this.labelCashOver.Visible = false;
            // 
            // userRoleField
            // 
            this.userRoleField.AutoSize = true;
            this.userRoleField.Location = new System.Drawing.Point(112, 59);
            this.userRoleField.Name = "userRoleField";
            this.userRoleField.Size = new System.Drawing.Size(0, 13);
            this.userRoleField.TabIndex = 5;
            // 
            // userRoleLabel
            // 
            this.userRoleLabel.AutoSize = true;
            this.userRoleLabel.Location = new System.Drawing.Point(50, 59);
            this.userRoleLabel.Name = "userRoleLabel";
            this.userRoleLabel.Size = new System.Drawing.Size(57, 13);
            this.userRoleLabel.TabIndex = 4;
            this.userRoleLabel.Text = "User Role:";
            // 
            // userEmpIdField
            // 
            this.userEmpIdField.AutoSize = true;
            this.userEmpIdField.Location = new System.Drawing.Point(112, 37);
            this.userEmpIdField.Name = "userEmpIdField";
            this.userEmpIdField.Size = new System.Drawing.Size(0, 13);
            this.userEmpIdField.TabIndex = 3;
            // 
            // userNameField
            // 
            this.userNameField.AutoSize = true;
            this.userNameField.Location = new System.Drawing.Point(112, 16);
            this.userNameField.Name = "userNameField";
            this.userNameField.Size = new System.Drawing.Size(0, 13);
            this.userNameField.TabIndex = 2;
            // 
            // userEmpIdLabel
            // 
            this.userEmpIdLabel.AutoSize = true;
            this.userEmpIdLabel.Location = new System.Drawing.Point(16, 37);
            this.userEmpIdLabel.Name = "userEmpIdLabel";
            this.userEmpIdLabel.Size = new System.Drawing.Size(93, 13);
            this.userEmpIdLabel.TabIndex = 1;
            this.userEmpIdLabel.Text = "User Employee #:";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(46, 16);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(63, 13);
            this.userNameLabel.TabIndex = 0;
            this.userNameLabel.Text = "User Name:";
            // 
            // shopTimeLabel
            // 
            this.shopTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.shopTimeLabel.Enabled = false;
            this.shopTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopTimeLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.shopTimeLabel.Location = new System.Drawing.Point(786, 738);
            this.shopTimeLabel.Name = "shopTimeLabel";
            this.shopTimeLabel.Size = new System.Drawing.Size(231, 26);
            this.shopTimeLabel.TabIndex = 11;
            this.shopTimeLabel.Text = "Time";
            this.shopTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.shopTimeLabel.Visible = false;
            // 
            // shopDateLabel
            // 
            this.shopDateLabel.AutoSize = true;
            this.shopDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.shopDateLabel.Enabled = false;
            this.shopDateLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopDateLabel.ForeColor = System.Drawing.Color.White;
            this.shopDateLabel.Location = new System.Drawing.Point(902, 707);
            this.shopDateLabel.Name = "shopDateLabel";
            this.shopDateLabel.Size = new System.Drawing.Size(82, 16);
            this.shopDateLabel.TabIndex = 12;
            this.shopDateLabel.Text = "MM/DD/YYYY";
            this.shopDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customerInfoGroupBox
            // 
            this.customerInfoGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.customerInfoGroupBox.BackgroundImage = global::Pawn.Properties.Resources.blue_white_panel;
            this.customerInfoGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customerInfoGroupBox.Controls.Add(this.customerAddressField);
            this.customerInfoGroupBox.Controls.Add(this.customerDOBField);
            this.customerInfoGroupBox.Controls.Add(this.customerNameField);
            this.customerInfoGroupBox.Controls.Add(this.customerDOBLabel);
            this.customerInfoGroupBox.Controls.Add(this.customerAddressLabel);
            this.customerInfoGroupBox.Controls.Add(this.customerNameLabel);
            this.customerInfoGroupBox.Enabled = false;
            this.customerInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customerInfoGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerInfoGroupBox.ForeColor = System.Drawing.Color.Black;
            this.customerInfoGroupBox.Location = new System.Drawing.Point(297, -1);
            this.customerInfoGroupBox.Name = "customerInfoGroupBox";
            this.customerInfoGroupBox.Size = new System.Drawing.Size(424, 54);
            this.customerInfoGroupBox.TabIndex = 13;
            this.customerInfoGroupBox.TabStop = false;
            this.customerInfoGroupBox.Text = "Customer Information";
            this.customerInfoGroupBox.Visible = false;
            // 
            // customerAddressField
            // 
            this.customerAddressField.AutoSize = true;
            this.customerAddressField.Location = new System.Drawing.Point(112, 37);
            this.customerAddressField.Name = "customerAddressField";
            this.customerAddressField.Size = new System.Drawing.Size(0, 13);
            this.customerAddressField.TabIndex = 6;
            // 
            // customerDOBField
            // 
            this.customerDOBField.AutoSize = true;
            this.customerDOBField.Location = new System.Drawing.Point(341, 16);
            this.customerDOBField.Name = "customerDOBField";
            this.customerDOBField.Size = new System.Drawing.Size(0, 13);
            this.customerDOBField.TabIndex = 5;
            // 
            // customerNameField
            // 
            this.customerNameField.AutoSize = true;
            this.customerNameField.Location = new System.Drawing.Point(112, 16);
            this.customerNameField.Name = "customerNameField";
            this.customerNameField.Size = new System.Drawing.Size(0, 13);
            this.customerNameField.TabIndex = 4;
            // 
            // customerDOBLabel
            // 
            this.customerDOBLabel.AutoSize = true;
            this.customerDOBLabel.Location = new System.Drawing.Point(309, 16);
            this.customerDOBLabel.Name = "customerDOBLabel";
            this.customerDOBLabel.Size = new System.Drawing.Size(32, 13);
            this.customerDOBLabel.TabIndex = 3;
            this.customerDOBLabel.Text = "DOB:";
            // 
            // customerAddressLabel
            // 
            this.customerAddressLabel.AutoSize = true;
            this.customerAddressLabel.Location = new System.Drawing.Point(62, 37);
            this.customerAddressLabel.Name = "customerAddressLabel";
            this.customerAddressLabel.Size = new System.Drawing.Size(50, 13);
            this.customerAddressLabel.TabIndex = 1;
            this.customerAddressLabel.Text = "Address:";
            // 
            // customerNameLabel
            // 
            this.customerNameLabel.AutoSize = true;
            this.customerNameLabel.Location = new System.Drawing.Point(25, 16);
            this.customerNameLabel.Name = "customerNameLabel";
            this.customerNameLabel.Size = new System.Drawing.Size(87, 13);
            this.customerNameLabel.TabIndex = 0;
            this.customerNameLabel.Text = "Customer Name:";
            // 
            // versionLabel
            // 
            this.versionLabel.AccessibleDescription = "Version Label";
            this.versionLabel.AccessibleName = "Version Label";
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.White;
            this.versionLabel.Location = new System.Drawing.Point(6, 725);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(83, 13);
            this.versionLabel.TabIndex = 18;
            this.versionLabel.Text = "VERSION LABEL";
            // 
            // mainMenuPanel
            // 
            this.mainMenuPanel.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.mainMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainMenuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainMenuPanel.CausesValidation = false;
            this.mainMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.mainMenuPanel.Location = new System.Drawing.Point(128, 0);
            this.mainMenuPanel.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.mainMenuPanel.MinimumSize = new System.Drawing.Size(768, 768);
            this.mainMenuPanel.Name = "mainMenuPanel";
            this.mainMenuPanel.Size = new System.Drawing.Size(768, 768);
            this.mainMenuPanel.TabIndex = 0;
            this.mainMenuPanel.Tag = "MainMenuPanel|null";
            this.mainMenuPanel.EnabledChanged += new System.EventHandler(this.MainMenuPanel_EnabledChanged);
            // 
            // pawnMenuPanel
            // 
            this.pawnMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.pawnMenuPanel.Enabled = false;
            this.pawnMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.pawnMenuPanel.Location = new System.Drawing.Point(352, 134);
            this.pawnMenuPanel.MaximumSize = new System.Drawing.Size(320, 500);
            this.pawnMenuPanel.MinimumSize = new System.Drawing.Size(320, 500);
            this.pawnMenuPanel.Name = "pawnMenuPanel";
            this.pawnMenuPanel.Size = new System.Drawing.Size(320, 500);
            this.pawnMenuPanel.TabIndex = 9;
            this.pawnMenuPanel.Tag = "PawnMenuPanel|MainMenuPanel";
            this.pawnMenuPanel.Visible = false;
            this.pawnMenuPanel.EnabledChanged += new System.EventHandler(this.PawnMenuPanel_EnabledChanged);
            // 
            // lookupMenuPanel
            // 
            this.lookupMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupMenuPanel.Enabled = false;
            this.lookupMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.lookupMenuPanel.Location = new System.Drawing.Point(362, 109);
            this.lookupMenuPanel.MaximumSize = new System.Drawing.Size(300, 550);
            this.lookupMenuPanel.MinimumSize = new System.Drawing.Size(300, 550);
            this.lookupMenuPanel.Name = "lookupMenuPanel";
            this.lookupMenuPanel.Size = new System.Drawing.Size(300, 550);
            this.lookupMenuPanel.TabIndex = 8;
            this.lookupMenuPanel.Tag = "LookupMenuPanel|MainMenuPanel";
            this.lookupMenuPanel.Visible = false;
            this.lookupMenuPanel.EnabledChanged += new System.EventHandler(this.LookupMenuPanel_EnabledChanged);
            // 
            // customerHoldsMenuPanel
            // 
            this.customerHoldsMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.customerHoldsMenuPanel.Enabled = false;
            this.customerHoldsMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.customerHoldsMenuPanel.Location = new System.Drawing.Point(362, 219);
            this.customerHoldsMenuPanel.MaximumSize = new System.Drawing.Size(300, 330);
            this.customerHoldsMenuPanel.MinimumSize = new System.Drawing.Size(300, 330);
            this.customerHoldsMenuPanel.Name = "customerHoldsMenuPanel";
            this.customerHoldsMenuPanel.Size = new System.Drawing.Size(300, 330);
            this.customerHoldsMenuPanel.TabIndex = 14;
            this.customerHoldsMenuPanel.Tag = "CustomerHoldsMenuPanel|UtilitiesMenuPanel";
            this.customerHoldsMenuPanel.Visible = false;
            this.customerHoldsMenuPanel.EnabledChanged += new System.EventHandler(this.CustomerHoldsMenuPanel_EnabledChanged);
            // 
            // pfiMenuPanel
            // 
            this.pfiMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.pfiMenuPanel.Enabled = false;
            this.pfiMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.pfiMenuPanel.Location = new System.Drawing.Point(362, 162);
            this.pfiMenuPanel.MaximumSize = new System.Drawing.Size(300, 570);
            this.pfiMenuPanel.MinimumSize = new System.Drawing.Size(300, 570);
            this.pfiMenuPanel.Name = "pfiMenuPanel";
            this.pfiMenuPanel.Size = new System.Drawing.Size(300, 570);
            this.pfiMenuPanel.TabIndex = 15;
            this.pfiMenuPanel.Tag = "PFIMenuPanel|UtilitiesMenuPanel";
            this.pfiMenuPanel.Visible = false;
            this.pfiMenuPanel.EnabledChanged += new System.EventHandler(this.PfiMenuPanel_EnabledChanged);
            // 
            // policeMenuPanel
            // 
            this.policeMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.policeMenuPanel.Enabled = false;
            this.policeMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.policeMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.policeMenuPanel.Location = new System.Drawing.Point(362, 134);
            this.policeMenuPanel.MaximumSize = new System.Drawing.Size(300, 600);
            this.policeMenuPanel.MinimumSize = new System.Drawing.Size(300, 600);
            this.policeMenuPanel.Name = "policeMenuPanel";
            this.policeMenuPanel.Size = new System.Drawing.Size(300, 600);
            this.policeMenuPanel.TabIndex = 16;
            this.policeMenuPanel.Tag = "PoliceMenuPanel|UtilitiesMenuPanel";
            this.policeMenuPanel.Visible = false;
            this.policeMenuPanel.EnabledChanged += new System.EventHandler(this.PoliceMenuPanel_EnabledChanged);
            // 
            // utilitiesMenuPanel
            // 
            this.utilitiesMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.utilitiesMenuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.utilitiesMenuPanel.Enabled = false;
            this.utilitiesMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.utilitiesMenuPanel.Location = new System.Drawing.Point(262, 54);
            this.utilitiesMenuPanel.MaximumSize = new System.Drawing.Size(500, 700);
            this.utilitiesMenuPanel.MinimumSize = new System.Drawing.Size(500, 700);
            this.utilitiesMenuPanel.Name = "utilitiesMenuPanel";
            this.utilitiesMenuPanel.Size = new System.Drawing.Size(500, 700);
            this.utilitiesMenuPanel.TabIndex = 17;
            this.utilitiesMenuPanel.Tag = "UtilitiesMenuPanel|MainMenuPanel";
            this.utilitiesMenuPanel.Visible = false;
            this.utilitiesMenuPanel.EnabledChanged += new System.EventHandler(this.UtilitiesMenuPanel_EnabledChanged);
            // 
            // reportsMenuPanel
            // 
            this.reportsMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.reportsMenuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.reportsMenuPanel.Enabled = false;
            this.reportsMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.reportsMenuPanel.Location = new System.Drawing.Point(317, 134);
            this.reportsMenuPanel.MaximumSize = new System.Drawing.Size(380, 695);
            this.reportsMenuPanel.MinimumSize = new System.Drawing.Size(380, 695);
            this.reportsMenuPanel.Name = "reportsMenuPanel";
            this.reportsMenuPanel.Size = new System.Drawing.Size(380, 695);
            this.reportsMenuPanel.TabIndex = 16;
            this.reportsMenuPanel.Tag = "ReportsMenuPanel|MainMenuPanel";
            this.reportsMenuPanel.Visible = false;
            this.reportsMenuPanel.EnabledChanged += new System.EventHandler(this.ReportMenuPanel_EnabledChanged);
            // 
            // transferMenuPanel
            // 
            this.transferMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.transferMenuPanel.Enabled = false;
            this.transferMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transferMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.transferMenuPanel.Location = new System.Drawing.Point(280, 144);
            this.transferMenuPanel.MaximumSize = new System.Drawing.Size(465, 480);
            this.transferMenuPanel.MinimumSize = new System.Drawing.Size(465, 480);
            this.transferMenuPanel.Name = "transferMenuPanel";
            this.transferMenuPanel.Size = new System.Drawing.Size(465, 480);
            this.transferMenuPanel.TabIndex = 19;
            this.transferMenuPanel.Tag = "TransferMenuPanel|UtilitiesMenuPanel";
            this.transferMenuPanel.Visible = false;
            this.transferMenuPanel.EnabledChanged += new System.EventHandler(this.TransferMenuPanel_EnabledChanged);
            // 
            // manageCashMenuPanel
            // 
            this.manageCashMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.manageCashMenuPanel.Enabled = false;
            this.manageCashMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manageCashMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.manageCashMenuPanel.Location = new System.Drawing.Point(360, 112);
            this.manageCashMenuPanel.MaximumSize = new System.Drawing.Size(275, 500);
            this.manageCashMenuPanel.MinimumSize = new System.Drawing.Size(275, 500);
            this.manageCashMenuPanel.Name = "manageCashMenuPanel";
            this.manageCashMenuPanel.Size = new System.Drawing.Size(275, 500);
            this.manageCashMenuPanel.TabIndex = 20;
            this.manageCashMenuPanel.Tag = "ManageCashMenuPanel|UtilitiesMenuPanel";
            this.manageCashMenuPanel.Visible = false;
            this.manageCashMenuPanel.EnabledChanged += new System.EventHandler(this.ManageCashMenuPanel_EnabledChanged);
            // 
            // refundReturnMenuPanel
            // 
            this.refundReturnMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.refundReturnMenuPanel.Enabled = false;
            this.refundReturnMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refundReturnMenuPanel.Location = new System.Drawing.Point(336, 182);
            this.refundReturnMenuPanel.MaximumSize = new System.Drawing.Size(450, 375);
            this.refundReturnMenuPanel.MinimumSize = new System.Drawing.Size(450, 375);
            this.refundReturnMenuPanel.Name = "refundReturnMenuPanel";
            this.refundReturnMenuPanel.Size = new System.Drawing.Size(450, 375);
            this.refundReturnMenuPanel.TabIndex = 22;
            this.refundReturnMenuPanel.Tag = "RefundReturnMenuPanel|UtilitiesMenuPanel";
            this.refundReturnMenuPanel.Visible = false;
            this.refundReturnMenuPanel.EnabledChanged += new System.EventHandler(this.RefundReturnMenuPanel_EnabledChanged);
            // 
            // customerBuyMenuPanel
            // 
            this.customerBuyMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.customerBuyMenuPanel.Enabled = false;
            this.customerBuyMenuPanel.Location = new System.Drawing.Point(352, 171);
            this.customerBuyMenuPanel.Name = "customerBuyMenuPanel";
            this.customerBuyMenuPanel.Size = new System.Drawing.Size(320, 426);
            this.customerBuyMenuPanel.TabIndex = 23;
            this.customerBuyMenuPanel.Tag = "CustomerBuyMenuPanel|BuyMenuPanel";
            this.customerBuyMenuPanel.Visible = false;
            this.customerBuyMenuPanel.EnabledChanged += new System.EventHandler(this.CustomerBuyMenuPanel_EnabledChanged);
            // 
            // buyMenuPanel
            // 
            this.buyMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.buyMenuPanel.Enabled = false;
            this.buyMenuPanel.Location = new System.Drawing.Point(377, 194);
            this.buyMenuPanel.Name = "buyMenuPanel";
            this.buyMenuPanel.Size = new System.Drawing.Size(270, 380);
            this.buyMenuPanel.TabIndex = 24;
            this.buyMenuPanel.Tag = "BuyMenuPanel|MainMenuPanel";
            this.buyMenuPanel.Visible = false;
            this.buyMenuPanel.EnabledChanged += new System.EventHandler(this.BuyMenuPanel_EnabledChanged);
            // 
            // voidMenuPanel
            // 
            this.voidMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.voidMenuPanel.Enabled = false;
            this.voidMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voidMenuPanel.Location = new System.Drawing.Point(257, 34);
            this.voidMenuPanel.MaximumSize = new System.Drawing.Size(510, 700);
            this.voidMenuPanel.MinimumSize = new System.Drawing.Size(510, 700);
            this.voidMenuPanel.Name = "voidMenuPanel";
            this.voidMenuPanel.Size = new System.Drawing.Size(510, 700);
            this.voidMenuPanel.TabIndex = 21;
            this.voidMenuPanel.Tag = "VoidMenuPanel|UtilitiesMenuPanel";
            this.voidMenuPanel.Visible = false;
            this.voidMenuPanel.EnabledChanged += new System.EventHandler(this.VoidMenuPanel_EnabledChanged);
            // 
            // manageInventoryMenuPanel
            // 
            this.manageInventoryMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.manageInventoryMenuPanel.Enabled = false;
            this.manageInventoryMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manageInventoryMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.manageInventoryMenuPanel.Location = new System.Drawing.Point(397, 197);
            this.manageInventoryMenuPanel.Name = "manageInventoryMenuPanel";
            this.manageInventoryMenuPanel.Size = new System.Drawing.Size(437, 582);
            this.manageInventoryMenuPanel.TabIndex = 25;
            this.manageInventoryMenuPanel.Tag = "ManageInventoryMenuPanel|UtilitiesMenuPanel";
            this.manageInventoryMenuPanel.Visible = false;
            this.manageInventoryMenuPanel.EnabledChanged += new System.EventHandler(this.ManageInventoryMenuPanel_EnabledChanged);
            // 
            // cashDrawerMenuPanel
            // 
            this.cashDrawerMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.cashDrawerMenuPanel.Enabled = false;
            this.cashDrawerMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.cashDrawerMenuPanel.Location = new System.Drawing.Point(387, 242);
            this.cashDrawerMenuPanel.Name = "cashDrawerMenuPanel";
            this.cashDrawerMenuPanel.Size = new System.Drawing.Size(260, 355);
            this.cashDrawerMenuPanel.TabIndex = 26;
            this.cashDrawerMenuPanel.Tag = "CashDrawerMenuPanel|ManageCashMenuPanel";
            this.cashDrawerMenuPanel.Visible = false;
            this.cashDrawerMenuPanel.EnabledChanged += new System.EventHandler(this.CashDrawerMenuPanel_EnabledChanged);
            // 
            // changePricingMenuPanel
            // 
            this.changePricingMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.changePricingMenuPanel.Enabled = false;
            this.changePricingMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.changePricingMenuPanel.Location = new System.Drawing.Point(397, 171);
            this.changePricingMenuPanel.Name = "changePricingMenuPanel";
            this.changePricingMenuPanel.Size = new System.Drawing.Size(241, 361);
            this.changePricingMenuPanel.TabIndex = 27;
            this.changePricingMenuPanel.Tag = "ChangePricingMenuPanel|ManageInventoryMenuPanel";
            this.changePricingMenuPanel.Visible = false;
            this.changePricingMenuPanel.EnabledChanged += new System.EventHandler(this.ChangePricingMenuPanel_EnabledChanged);
            // 
            // gunBookMenuPanel
            // 
            this.gunBookMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.gunBookMenuPanel.Enabled = false;
            this.gunBookMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.gunBookMenuPanel.Location = new System.Drawing.Point(392, 134);
            this.gunBookMenuPanel.Name = "gunBookMenuPanel";
            this.gunBookMenuPanel.Size = new System.Drawing.Size(246, 357);
            this.gunBookMenuPanel.TabIndex = 28;
            this.gunBookMenuPanel.Tag = "GunBookMenuPanel|UtilitiesMenuPanel";
            this.gunBookMenuPanel.Visible = false;
            this.gunBookMenuPanel.EnabledChanged += new System.EventHandler(this.GunBookMenuPanel_EnabledChanged);
            // 
            // safeOperationsMenuPanel
            // 
            this.safeOperationsMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.safeOperationsMenuPanel.Enabled = false;
            this.safeOperationsMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.safeOperationsMenuPanel.Location = new System.Drawing.Point(297, 109);
            this.safeOperationsMenuPanel.Name = "safeOperationsMenuPanel";
            this.safeOperationsMenuPanel.Size = new System.Drawing.Size(434, 597);
            this.safeOperationsMenuPanel.TabIndex = 29;
            this.safeOperationsMenuPanel.Tag = "SafeOperationsMenuPanel|ManageCashMenuPanel";
            this.safeOperationsMenuPanel.Visible = false;
            this.safeOperationsMenuPanel.EnabledChanged += new System.EventHandler(this.SafeOperationsMenuPanel_EnabledChanged);
            // 
            // shopDateField
            // 
            this.shopDateField.AutoSize = true;
            this.shopDateField.Location = new System.Drawing.Point(112, 75);
            this.shopDateField.Name = "shopDateField";
            this.shopDateField.Size = new System.Drawing.Size(0, 13);
            this.shopDateField.TabIndex = 8;
            // 
            // NewDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.shopDateLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.customerInfoGroupBox);
            this.Controls.Add(this.userInfoGroupBox);
            this.Controls.Add(this.shopTimeLabel);
            this.Controls.Add(this.mainMenuPanel);
            this.Controls.Add(this.pawnMenuPanel);
            this.Controls.Add(this.lookupMenuPanel);
            this.Controls.Add(this.customerHoldsMenuPanel);
            this.Controls.Add(this.pfiMenuPanel);
            this.Controls.Add(this.policeMenuPanel);
            this.Controls.Add(this.utilitiesMenuPanel);
            this.Controls.Add(this.reportsMenuPanel);
            this.Controls.Add(this.transferMenuPanel);
            this.Controls.Add(this.manageCashMenuPanel);
            this.Controls.Add(this.refundReturnMenuPanel);
            this.Controls.Add(this.customerBuyMenuPanel);
            this.Controls.Add(this.buyMenuPanel);
            this.Controls.Add(this.voidMenuPanel);
            this.Controls.Add(this.manageInventoryMenuPanel);
            this.Controls.Add(this.safeOperationsMenuPanel);
            this.Controls.Add(this.gunBookMenuPanel);
            this.Controls.Add(this.changePricingMenuPanel);
            this.Controls.Add(this.cashDrawerMenuPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "NewDesktop";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowIcon = false;
            this.Text = "Cashlinx Desktop";
            this.Load += new System.EventHandler(this.NewDesktop_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewDesktop_KeyDown);
            this.userInfoGroupBox.ResumeLayout(false);
            this.userInfoGroupBox.PerformLayout();
            this.customerInfoGroupBox.ResumeLayout(false);
            this.customerInfoGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MainMenuPanel mainMenuPanel;
        private LookupMenuPanel lookupMenuPanel;
        private PawnMenuPanel pawnMenuPanel;
        private System.Windows.Forms.GroupBox userInfoGroupBox;
        private System.Windows.Forms.Label userNameField;
        private System.Windows.Forms.Label userEmpIdLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label shopTimeLabel;
        private System.Windows.Forms.Label shopDateLabel;
        private System.Windows.Forms.GroupBox customerInfoGroupBox;
        private System.Windows.Forms.Label customerAddressLabel;
        private System.Windows.Forms.Label customerNameLabel;
        private System.Windows.Forms.Label customerNameField;
        private System.Windows.Forms.Label customerDOBLabel;
        private System.Windows.Forms.Label customerAddressField;
        private System.Windows.Forms.Label customerDOBField;
        private System.Windows.Forms.Label userEmpIdField;
        private CustomerHoldsMenuPanel customerHoldsMenuPanel;
        private PFIMenuPanel pfiMenuPanel;
        private PoliceMenuPanel policeMenuPanel;
        private UtilitiesMenuPanel utilitiesMenuPanel;
        private ReportsMenuPanel reportsMenuPanel;
        private CustomLabel versionLabel;
        private System.Windows.Forms.Label userRoleLabel;
        private System.Windows.Forms.Label userRoleField;
        private TransferMenuPanel transferMenuPanel;
        private ManageCashMenuPanel manageCashMenuPanel;
        private VoidMenuPanel voidMenuPanel;
        private RefundReturnMenuPanel refundReturnMenuPanel;
        private CustomerBuyMenuPanel customerBuyMenuPanel;
        private BuyMenuPanel buyMenuPanel;
        private System.Windows.Forms.Label labelCashOver;
        private ManageInventoryMenuPanel manageInventoryMenuPanel;
        private CashDrawerMenuPanel cashDrawerMenuPanel;
        private ChangePricingMenuPanel changePricingMenuPanel;
        private GunBookMenuPanel gunBookMenuPanel;
        private SafeOperationsMenuPanel safeOperationsMenuPanel;
        private System.Windows.Forms.Label labelDateHeading;
        private System.Windows.Forms.Label shopDateField;
        //private USBUtilities.DetectorForm detectorForm;
    }
}

