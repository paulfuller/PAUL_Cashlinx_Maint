namespace Pawn.Forms.UserControls
{
    partial class RefundQuantityItem
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
            this.lblIcn = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblRetailPrice = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtRefundQuantity = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblIcn
            // 
            this.lblIcn.Location = new System.Drawing.Point(3, 8);
            this.lblIcn.Name = "lblIcn";
            this.lblIcn.Size = new System.Drawing.Size(136, 17);
            this.lblIcn.TabIndex = 0;
            this.lblIcn.Text = "label1";
            this.lblIcn.Click += new System.EventHandler(this.RefundQuantityItem_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescription.Location = new System.Drawing.Point(145, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(271, 32);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "label2";
            this.lblDescription.Click += new System.EventHandler(this.RefundQuantityItem_Click);
            // 
            // lblRetailPrice
            // 
            this.lblRetailPrice.Location = new System.Drawing.Point(422, 8);
            this.lblRetailPrice.Name = "lblRetailPrice";
            this.lblRetailPrice.Size = new System.Drawing.Size(56, 17);
            this.lblRetailPrice.TabIndex = 2;
            this.lblRetailPrice.Text = "label3";
            this.lblRetailPrice.Click += new System.EventHandler(this.RefundQuantityItem_Click);
            // 
            // lblDiscount
            // 
            this.lblDiscount.Location = new System.Drawing.Point(484, 8);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(56, 17);
            this.lblDiscount.TabIndex = 3;
            this.lblDiscount.Text = "label4";
            this.lblDiscount.Click += new System.EventHandler(this.RefundQuantityItem_Click);
            // 
            // lblQuantity
            // 
            this.lblQuantity.Location = new System.Drawing.Point(546, 8);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(58, 17);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "label5";
            this.lblQuantity.Click += new System.EventHandler(this.RefundQuantityItem_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(699, 8);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(67, 17);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "label7";
            this.lblTotal.Click += new System.EventHandler(this.RefundQuantityItem_Click);
            // 
            // txtRefundQuantity
            // 
            this.txtRefundQuantity.Location = new System.Drawing.Point(610, 6);
            this.txtRefundQuantity.Name = "txtRefundQuantity";
            this.txtRefundQuantity.Size = new System.Drawing.Size(60, 20);
            this.txtRefundQuantity.TabIndex = 7;
            // 
            // RefundQuantityItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtRefundQuantity);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblRetailPrice);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblIcn);
            this.Name = "RefundQuantityItem";
            this.Size = new System.Drawing.Size(766, 32);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RefundQuantityItem_Paint);
            this.Click += new System.EventHandler(this.RefundQuantityItem_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIcn;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblRetailPrice;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtRefundQuantity;
    }
}
