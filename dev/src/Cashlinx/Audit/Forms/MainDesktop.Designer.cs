namespace Audit
{
    partial class MainDesktop
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
            this.mainMenuPanel = new Audit.Panels.MenuPanels.MainMenuPanel();
            this.SuspendLayout();
            // 
            // mainMenuPanel
            // 
            this.mainMenuPanel.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.mainMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainMenuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainMenuPanel.CausesValidation = false;
            this.mainMenuPanel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.mainMenuPanel.ForeColor = System.Drawing.Color.Black;
            this.mainMenuPanel.HideOnReset = false;
            this.mainMenuPanel.Location = new System.Drawing.Point(128, 0);
            this.mainMenuPanel.MaximumSize = new System.Drawing.Size(2000, 2000);
            this.mainMenuPanel.MinimumSize = new System.Drawing.Size(768, 768);
            this.mainMenuPanel.Name = "mainMenuPanel";
            this.mainMenuPanel.Size = new System.Drawing.Size(768, 768);
            this.mainMenuPanel.TabIndex = 1;
            this.mainMenuPanel.Tag = "MainMenuPanel|null";
            this.mainMenuPanel.Load += new System.EventHandler(this.mainMenuPanel_Load);
            this.mainMenuPanel.EnabledChanged += new System.EventHandler(this.mainMenuPanel_EnabledChanged);
            // 
            // MainDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.mainMenuPanel);
            this.Name = "MainDesktop";
            this.Text = "MainDesktop";
            this.Load += new System.EventHandler(this.MainDesktop_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainDesktop_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Panels.MenuPanels.MainMenuPanel mainMenuPanel;

    }
}