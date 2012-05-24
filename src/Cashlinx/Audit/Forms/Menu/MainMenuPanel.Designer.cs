namespace Audit.Panels.MenuPanels
{
    partial class MainMenuPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exitButton = new System.Windows.Forms.Button();
            this.InventoryAuditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Transparent;
            this.exitButton.BackgroundImage = global::Audit.Properties.Resources.vistabutton_blue;
            this.exitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.Location = new System.Drawing.Point(334, 705);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.exitButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(100, 50);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            // 
            // InventoryAuditButton
            // 
            this.InventoryAuditButton.BackColor = System.Drawing.Color.Transparent;
            this.InventoryAuditButton.BackgroundImage = global::Audit.ButtonResources.inventoryaudit_button_normal;
            this.InventoryAuditButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.InventoryAuditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InventoryAuditButton.Enabled = false;
            this.InventoryAuditButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.InventoryAuditButton.FlatAppearance.BorderSize = 0;
            this.InventoryAuditButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.InventoryAuditButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.InventoryAuditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InventoryAuditButton.ForeColor = System.Drawing.Color.Transparent;
            this.InventoryAuditButton.Location = new System.Drawing.Point(279, 279);
            this.InventoryAuditButton.Margin = new System.Windows.Forms.Padding(4);
            this.InventoryAuditButton.MaximumSize = new System.Drawing.Size(210, 210);
            this.InventoryAuditButton.MinimumSize = new System.Drawing.Size(210, 210);
            this.InventoryAuditButton.Name = "InventoryAuditButton";
            this.InventoryAuditButton.Size = new System.Drawing.Size(210, 210);
            this.InventoryAuditButton.TabIndex = 11;
            this.InventoryAuditButton.Tag = "InventoryAuditButton|null";
            this.InventoryAuditButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.InventoryAuditButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.InventoryAuditButton.UseVisualStyleBackColor = true;
            // 
            // MainMenuPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InventoryAuditButton);
            this.Controls.Add(this.exitButton);
            this.Name = "MainMenuPanel";
            this.Load += new System.EventHandler(this.MainMenuPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button InventoryAuditButton;

    }
}
