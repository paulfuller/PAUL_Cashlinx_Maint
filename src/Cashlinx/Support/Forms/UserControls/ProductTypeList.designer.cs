namespace Support.Forms.UserControls
{
    partial class ProductTypeList
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
            this.productList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // productList
            // 
            this.productList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.productList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productList.FormattingEnabled = true;
            this.productList.Location = new System.Drawing.Point(0, 0);
            this.productList.Name = "productList";
            this.productList.Size = new System.Drawing.Size(136, 21);
            this.productList.TabIndex = 0;
            this.productList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.productList_DrawItem);
            this.productList.SelectedIndexChanged += new System.EventHandler(this.productList_SelectedIndexChanged);
            // 
            // ProductTypeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.productList);
            this.Name = "ProductTypeList";
            this.Size = new System.Drawing.Size(136, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox productList;
    }
}
