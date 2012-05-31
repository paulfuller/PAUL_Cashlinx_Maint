namespace Support.Libraries.Forms
{
    partial class FlowTabController
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
            this.controllerTabs = new System.Windows.Forms.TabControl();
            this.customerTab = new System.Windows.Forms.TabPage();
            this.productsServicesTab = new System.Windows.Forms.TabPage();
            this.itemHistoryTab = new System.Windows.Forms.TabPage();
            this.statsTab = new System.Windows.Forms.TabPage();
            this.commentTab = new System.Windows.Forms.TabPage();
            this.productHistoryTab = new System.Windows.Forms.TabPage();
            this.controllerTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // controllerTabs
            // 
            this.controllerTabs.Controls.Add(this.customerTab);
            this.controllerTabs.Controls.Add(this.productsServicesTab);
            this.controllerTabs.Controls.Add(this.itemHistoryTab);
            this.controllerTabs.Controls.Add(this.statsTab);
            this.controllerTabs.Controls.Add(this.commentTab);
            this.controllerTabs.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controllerTabs.Location = new System.Drawing.Point(2, 3);
            this.controllerTabs.Name = "controllerTabs";
            this.controllerTabs.SelectedIndex = 0;
            this.controllerTabs.Size = new System.Drawing.Size(452, 27);
            this.controllerTabs.TabIndex = 0;
            this.controllerTabs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.controllerTabs_DrawItem);
            this.controllerTabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.controllerTabs_Selecting);
            this.controllerTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.controllerTabs_Selected);
            // 
            // customerTab
            // 
            this.customerTab.BackColor = System.Drawing.Color.Transparent;
            this.customerTab.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerTab.Location = new System.Drawing.Point(4, 25);
            this.customerTab.Name = "customerTab";
            this.customerTab.Padding = new System.Windows.Forms.Padding(3);
            this.customerTab.Size = new System.Drawing.Size(444, 0);
            this.customerTab.TabIndex = 0;
            this.customerTab.Text = "Customer";
            // 
            // productsServicesTab
            // 
            this.productsServicesTab.BackColor = System.Drawing.Color.Transparent;
            this.productsServicesTab.Location = new System.Drawing.Point(4, 25);
            this.productsServicesTab.Name = "productsServicesTab";
            this.productsServicesTab.Padding = new System.Windows.Forms.Padding(3);
            this.productsServicesTab.Size = new System.Drawing.Size(562, 0);
            this.productsServicesTab.TabIndex = 1;
            this.productsServicesTab.Text = "Products & Services";
            // 
            // itemHistoryTab
            // 
            this.itemHistoryTab.BackColor = System.Drawing.Color.Transparent;
            this.itemHistoryTab.Location = new System.Drawing.Point(4, 25);
            this.itemHistoryTab.Name = "itemHistoryTab";
            this.itemHistoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.itemHistoryTab.Size = new System.Drawing.Size(562, 0);
            this.itemHistoryTab.TabIndex = 3;
            this.itemHistoryTab.Text = "Item History";
            // 
            // statsTab
            // 
            this.statsTab.Location = new System.Drawing.Point(4, 25);
            this.statsTab.Name = "statsTab";
            this.statsTab.Padding = new System.Windows.Forms.Padding(3);
            this.statsTab.Size = new System.Drawing.Size(562, 0);
            this.statsTab.TabIndex = 4;
            this.statsTab.Text = "Stats";
            this.statsTab.UseVisualStyleBackColor = true;
            // 
            // commentTab
            // 
            this.commentTab.Location = new System.Drawing.Point(4, 25);
            this.commentTab.Name = "commentTab";
            this.commentTab.Padding = new System.Windows.Forms.Padding(3);
            this.commentTab.Size = new System.Drawing.Size(562, 0);
            this.commentTab.TabIndex = 5;
            this.commentTab.Text = "Comments ";
            this.commentTab.UseVisualStyleBackColor = true;
            // 
            // productHistoryTab
            // 
            this.productHistoryTab.BackColor = System.Drawing.Color.Transparent;
            this.productHistoryTab.Location = new System.Drawing.Point(4, 25);
            this.productHistoryTab.Name = "productHistoryTab";
            this.productHistoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.productHistoryTab.Size = new System.Drawing.Size(569, 0);
            this.productHistoryTab.TabIndex = 2;
            this.productHistoryTab.Text = "Product History";
            // 
            // FlowTabController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.controllerTabs);
            this.MaximumSize = new System.Drawing.Size(900, 30);
            this.MinimumSize = new System.Drawing.Size(900, 1);
            this.Name = "FlowTabController";
            this.Size = new System.Drawing.Size(900, 30);
            this.controllerTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl controllerTabs;
        private System.Windows.Forms.TabPage customerTab;
        private System.Windows.Forms.TabPage productsServicesTab;
        private System.Windows.Forms.TabPage productHistoryTab;
        private System.Windows.Forms.TabPage itemHistoryTab;
        private System.Windows.Forms.TabPage statsTab;
        private System.Windows.Forms.TabPage commentTab;
    }
}
