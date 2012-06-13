using Common.Libraries.Forms.Components;
using Support.Forms.Panels.MenuPanels;

namespace Support.Forms
{
    sealed partial class SupportAppDesktop
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
            this.shopTimeLabel = new System.Windows.Forms.Label();
            this.shopDateLabel = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.mainMenuPanel = new Support.Forms.Panels.MenuPanels.MainMenuPanel();
            this.versionLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.shopAdminMenuPanel = new Support.Forms.Panels.MenuPanels.ShopAdminMenuPanel();
            this.CustomerServiceMenuPanel = new Support.Forms.Panels.MenuPanels.CustomerServiceMenuPanel();
            this.userAdminMenuPanel = new Support.Forms.Panels.MenuPanels.UserAdminMenuPanel();
            this.systemAdminMenuPanel = new Support.Forms.Panels.MenuPanels.SystemAdminMenuPanel();
            this.configMenuPanel2 = new Support.Forms.Panels.MenuPanels.ConfigMenuPanel2();
            this.cashlinxAdminPanel2 = new Support.Forms.Panels.MenuPanels.CashlinxAdminPanel2();
            this.gbUtilitiesPanel = new Support.Forms.Panels.MenuPanels.GBUtilitiesPanel();
            this.SuspendLayout();
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
            this.shopDateLabel.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Location = new System.Drawing.Point(41, 714);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(44, 13);
            this.lblVersion.TabIndex = 25;
            this.lblVersion.Text = "Support";
            // 
            // mainMenuPanel
            // 
            this.mainMenuPanel.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.mainMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainMenuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainMenuPanel.CausesValidation = false;
            this.mainMenuPanel.DesktopSession = null;
            this.mainMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.mainMenuPanel.Location = new System.Drawing.Point(383, -19);
            this.mainMenuPanel.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.mainMenuPanel.MinimumSize = new System.Drawing.Size(695, 768);
            this.mainMenuPanel.Name = "mainMenuPanel";
            this.mainMenuPanel.Size = new System.Drawing.Size(695, 768);
            this.mainMenuPanel.TabIndex = 0;
            this.mainMenuPanel.Tag = "MainMenuPanel|null";
            this.mainMenuPanel.EnabledChanged += new System.EventHandler(this.MainMenuPanel_EnabledChanged);
            // 
            // versionLabel
            // 
            this.versionLabel.AccessibleDescription = "Version Label";
            this.versionLabel.AccessibleName = "Version Label";
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.White;
            this.versionLabel.Location = new System.Drawing.Point(20, 613);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(83, 13);
            this.versionLabel.TabIndex = 18;
            this.versionLabel.Text = "VERSION LABEL";
            this.versionLabel.Visible = false;
            // 
            // shopAdminMenuPanel
            // 
            this.shopAdminMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.shopAdminMenuPanel.Enabled = false;
            this.shopAdminMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.shopAdminMenuPanel.Location = new System.Drawing.Point(504, 7);
            this.shopAdminMenuPanel.MaximumSize = new System.Drawing.Size(432, 586);
            this.shopAdminMenuPanel.MinimumSize = new System.Drawing.Size(432, 586);
            this.shopAdminMenuPanel.Name = "shopAdminMenuPanel";
            this.shopAdminMenuPanel.Size = new System.Drawing.Size(432, 586);
            this.shopAdminMenuPanel.TabIndex = 21;
            this.shopAdminMenuPanel.Tag = "ShopAdminMenuPanel|MainMenuPanel";
            this.shopAdminMenuPanel.Visible = false;
            this.shopAdminMenuPanel.EnabledChanged += new System.EventHandler(this.shopAdminMenuPanel_EnabledChanged);
            // 
            // CustomerServiceMenuPanel
            // 
            this.CustomerServiceMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.CustomerServiceMenuPanel.Enabled = false;
            this.CustomerServiceMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.CustomerServiceMenuPanel.Location = new System.Drawing.Point(23, 84);
            this.CustomerServiceMenuPanel.MaximumSize = new System.Drawing.Size(297, 477);
            this.CustomerServiceMenuPanel.MinimumSize = new System.Drawing.Size(297, 477);
            this.CustomerServiceMenuPanel.Name = "CustomerServiceMenuPanel";
            this.CustomerServiceMenuPanel.Size = new System.Drawing.Size(297, 477);
            this.CustomerServiceMenuPanel.TabIndex = 19;
            this.CustomerServiceMenuPanel.Tag = "CustomerServiceMenuPanel|null";
            this.CustomerServiceMenuPanel.Visible = false;
            this.CustomerServiceMenuPanel.EnabledChanged += new System.EventHandler(this.CustomerServiceMenuPanel_EnabledChanged);
            // 
            // userAdminMenuPanel
            // 
            this.userAdminMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.userAdminMenuPanel.Enabled = false;
            this.userAdminMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.userAdminMenuPanel.Location = new System.Drawing.Point(213, 188);
            this.userAdminMenuPanel.MaximumSize = new System.Drawing.Size(432, 586);
            this.userAdminMenuPanel.MinimumSize = new System.Drawing.Size(432, 586);
            this.userAdminMenuPanel.Name = "userAdminMenuPanel";
            this.userAdminMenuPanel.Size = new System.Drawing.Size(432, 586);
            this.userAdminMenuPanel.TabIndex = 20;
            this.userAdminMenuPanel.Tag = "userAdminMenuPanel|MainMenuPanel";
            this.userAdminMenuPanel.Visible = false;
            this.userAdminMenuPanel.EnabledChanged += new System.EventHandler(this.userAdminMenuPanel_EnabledChanged);
            // 
            // systemAdminMenuPanel
            // 
            this.systemAdminMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.systemAdminMenuPanel.Enabled = false;
            this.systemAdminMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.systemAdminMenuPanel.Location = new System.Drawing.Point(348, 27);
            this.systemAdminMenuPanel.MaximumSize = new System.Drawing.Size(297, 477);
            this.systemAdminMenuPanel.MinimumSize = new System.Drawing.Size(297, 477);
            this.systemAdminMenuPanel.Name = "systemAdminMenuPanel";
            this.systemAdminMenuPanel.Size = new System.Drawing.Size(297, 477);
            this.systemAdminMenuPanel.TabIndex = 22;
            this.systemAdminMenuPanel.Tag = "SystemAdminMenuPanel|MainMenuPanel";
            this.systemAdminMenuPanel.Visible = false;
            this.systemAdminMenuPanel.EnabledChanged += new System.EventHandler(this.systemAdminMenuPanel_EnabledChanged);
            // 
            // configMenuPanel2
            // 
            this.configMenuPanel2.BackColor = System.Drawing.Color.Transparent;
            this.configMenuPanel2.Enabled = false;
            this.configMenuPanel2.ForeColor = System.Drawing.Color.Black;
            this.configMenuPanel2.Location = new System.Drawing.Point(172, 234);
            this.configMenuPanel2.MaximumSize = new System.Drawing.Size(432, 477);
            this.configMenuPanel2.MinimumSize = new System.Drawing.Size(432, 477);
            this.configMenuPanel2.Name = "configMenuPanel2";
            this.configMenuPanel2.Size = new System.Drawing.Size(432, 477);
            this.configMenuPanel2.TabIndex = 23;
            this.configMenuPanel2.Tag = "ConfigMenuPanel2|ShopAdminMenuPanel";
            this.configMenuPanel2.Visible = false;
            this.configMenuPanel2.EnabledChanged += new System.EventHandler(this.configMenuPanel2_EnabledChanged);
            // 
            // cashlinxAdminPanel2
            // 
            this.cashlinxAdminPanel2.BackColor = System.Drawing.Color.Transparent;
            this.cashlinxAdminPanel2.Enabled = false;
            this.cashlinxAdminPanel2.ForeColor = System.Drawing.Color.Black;
            this.cashlinxAdminPanel2.Location = new System.Drawing.Point(67, 217);
            this.cashlinxAdminPanel2.MaximumSize = new System.Drawing.Size(650, 600);
            this.cashlinxAdminPanel2.MinimumSize = new System.Drawing.Size(650, 600);
            this.cashlinxAdminPanel2.Name = "cashlinxAdminPanel2";
            this.cashlinxAdminPanel2.Size = new System.Drawing.Size(650, 600);
            this.cashlinxAdminPanel2.TabIndex = 24;
            this.cashlinxAdminPanel2.Tag = "CashlinxAdminPanel2|SystemAdminMenuPanel";
            this.cashlinxAdminPanel2.Visible = false;
            this.cashlinxAdminPanel2.EnabledChanged += new System.EventHandler(this.cashlinxAdminPanel2_EnabledChanged);
            // 
            // gbUtilitiesPanel
            // 
            this.gbUtilitiesPanel.BackColor = System.Drawing.Color.Transparent;
            this.gbUtilitiesPanel.ForeColor = System.Drawing.Color.Black;
            this.gbUtilitiesPanel.Location = new System.Drawing.Point(44, 7);
            this.gbUtilitiesPanel.MaximumSize = new System.Drawing.Size(399, 432);
            this.gbUtilitiesPanel.MinimumSize = new System.Drawing.Size(399, 432);
            this.gbUtilitiesPanel.Name = "gbUtilitiesPanel";
            this.gbUtilitiesPanel.Size = new System.Drawing.Size(399, 432);
            this.gbUtilitiesPanel.TabIndex = 26;
            this.gbUtilitiesPanel.Tag = "GBUtilitiesPanel|ShopAdminPanel";
            this.gbUtilitiesPanel.Visible = false;
            this.gbUtilitiesPanel.EnabledChanged += new System.EventHandler(this.gbUtilitiesMenuPanel_EnabledChanged);
            // 
            // SupportAppDesktop
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.gbUtilitiesPanel);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.mainMenuPanel);
            this.Controls.Add(this.shopDateLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.shopTimeLabel);
            this.Controls.Add(this.shopAdminMenuPanel);
            this.Controls.Add(this.CustomerServiceMenuPanel);
            this.Controls.Add(this.userAdminMenuPanel);
            this.Controls.Add(this.systemAdminMenuPanel);
            this.Controls.Add(this.configMenuPanel2);
            this.Controls.Add(this.cashlinxAdminPanel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "SupportAppDesktop";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowIcon = false;
            this.Text = "Pawn Support App Desktop";
            this.Load += new System.EventHandler(this.SupportAppDesktop_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewDesktop_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MainMenuPanel mainMenuPanel;
        private System.Windows.Forms.Label shopTimeLabel;
        private System.Windows.Forms.Label shopDateLabel;
        private CustomLabel versionLabel;
        private Panels.MenuPanels.CustomerServiceMenuPanel CustomerServiceMenuPanel;
        private Panels.MenuPanels.UserAdminMenuPanel userAdminMenuPanel;
        private Panels.MenuPanels.ShopAdminMenuPanel shopAdminMenuPanel;
        //private Panels.MenuPanels.LookupMenuPanel lookupMenuPanel;
        private Panels.MenuPanels.SystemAdminMenuPanel systemAdminMenuPanel;
        private Panels.MenuPanels.ConfigMenuPanel2 configMenuPanel2;
        private Panels.MenuPanels.CashlinxAdminPanel2 cashlinxAdminPanel2;
        private System.Windows.Forms.Label lblVersion;
        private GBUtilitiesPanel gbUtilitiesPanel;
    }
}

