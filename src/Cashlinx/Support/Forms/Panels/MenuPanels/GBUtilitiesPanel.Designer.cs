namespace Support.Forms.Panels.MenuPanels
{
    partial class GBUtilitiesPanel
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
            this.BackButton = new System.Windows.Forms.Button();
            this.gbeditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Transparent;
            this.BackButton.BackgroundImage = global::Support.MenuButtonResources.back_button_normal;
            this.BackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BackButton.FlatAppearance.BorderSize = 0;
            this.BackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackButton.ForeColor = System.Drawing.Color.Transparent;
            this.BackButton.Location = new System.Drawing.Point(88, 256);
            this.BackButton.Margin = new System.Windows.Forms.Padding(4);
            this.BackButton.MaximumSize = new System.Drawing.Size(205, 105);
            this.BackButton.MinimumSize = new System.Drawing.Size(205, 105);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(205, 105);
            this.BackButton.TabIndex = 25;
            this.BackButton.Tag = "BackButton|ShopAdminPanel";
            this.BackButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BackButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BackButton.UseVisualStyleBackColor = false;
            // 
            // gbeditButton
            // 
            this.gbeditButton.BackColor = System.Drawing.Color.Transparent;
            this.gbeditButton.BackgroundImage = global::Support.MenuButtonResources.gbedit_button_normal;
            this.gbeditButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbeditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gbeditButton.Enabled = false;
            this.gbeditButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.gbeditButton.FlatAppearance.BorderSize = 0;
            this.gbeditButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.gbeditButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.gbeditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbeditButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbeditButton.ForeColor = System.Drawing.Color.Transparent;
            this.gbeditButton.Location = new System.Drawing.Point(88, 106);
            this.gbeditButton.Margin = new System.Windows.Forms.Padding(4);
            this.gbeditButton.MaximumSize = new System.Drawing.Size(205, 105);
            this.gbeditButton.MinimumSize = new System.Drawing.Size(205, 105);
            this.gbeditButton.Name = "gbeditButton";
            this.gbeditButton.Size = new System.Drawing.Size(205, 105);
            this.gbeditButton.TabIndex = 24;
            this.gbeditButton.Tag = "gbeditButton|null";
            this.gbeditButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.gbeditButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.gbeditButton.UseVisualStyleBackColor = false;
            // 
            // GBUtilitiesPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.gbeditButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximumSize = new System.Drawing.Size(399, 432);
            this.MinimumSize = new System.Drawing.Size(399, 432);
            this.Name = "GBUtilitiesPanel";
            this.Size = new System.Drawing.Size(399, 432);
            this.Tag = "GBUtilitiesPanel|ShopAdminPanel";
            this.Load += new System.EventHandler(this.GBUtilitiesPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button gbeditButton;
    }
}
