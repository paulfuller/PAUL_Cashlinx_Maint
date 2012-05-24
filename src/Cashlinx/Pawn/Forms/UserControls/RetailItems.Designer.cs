using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls
{
    partial class RetailItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetailItems));
            this.lblRetailPrice = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblICN = new System.Windows.Forms.Label();
            this.btnCoupon = new System.Windows.Forms.Button();
            this.chkSelectControl = new System.Windows.Forms.CheckBox();
            this.cmbSaleType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCouponAmt = new System.Windows.Forms.Label();
            this.txtQty = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtNegotiatedPrice = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtDiscount = new Common.Libraries.Forms.Components.CustomTextBox();
            this.lblComments = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRetailPrice
            // 
            this.lblRetailPrice.AutoSize = true;
            this.lblRetailPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblRetailPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetailPrice.Location = new System.Drawing.Point(473, 12);
            this.lblRetailPrice.Name = "lblRetailPrice";
            this.lblRetailPrice.Size = new System.Drawing.Size(49, 13);
            this.lblRetailPrice.TabIndex = 11;
            this.lblRetailPrice.Text = "$ 200.00";
            this.lblRetailPrice.Click += new System.EventHandler(this.lblRetailPrice_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(812, 11);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(49, 13);
            this.lblTotal.TabIndex = 15;
            this.lblTotal.Text = "$ 200.00";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Courier New", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(277, 4);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(187, 27);
            this.lblDescription.TabIndex = 49;
            this.lblDescription.Text = "0";
            this.lblDescription.Click += new System.EventHandler(this.lblDescription_Click);
            // 
            // lblICN
            // 
            this.lblICN.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblICN.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblICN.Location = new System.Drawing.Point(144, 4);
            this.lblICN.Name = "lblICN";
            this.lblICN.Size = new System.Drawing.Size(117, 24);
            this.lblICN.TabIndex = 50;
            this.lblICN.Text = "0";
            this.lblICN.Click += new System.EventHandler(this.lblICN_Click);
            // 
            // btnCoupon
            // 
            this.btnCoupon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCoupon.AutoSize = true;
            this.btnCoupon.BackColor = System.Drawing.Color.Transparent;
            this.btnCoupon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCoupon.BackgroundImage")));
            this.btnCoupon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCoupon.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnCoupon.FlatAppearance.BorderSize = 0;
            this.btnCoupon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCoupon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCoupon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCoupon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCoupon.ForeColor = System.Drawing.Color.White;
            this.btnCoupon.Location = new System.Drawing.Point(3, 2);
            this.btnCoupon.Name = "btnCoupon";
            this.btnCoupon.Size = new System.Drawing.Size(60, 23);
            this.btnCoupon.TabIndex = 145;
            this.btnCoupon.Text = "Coupon";
            this.btnCoupon.UseVisualStyleBackColor = false;
            this.btnCoupon.Click += new System.EventHandler(this.btnCoupon_Click);
            // 
            // chkSelectControl
            // 
            this.chkSelectControl.AutoSize = true;
            this.chkSelectControl.Location = new System.Drawing.Point(863, 11);
            this.chkSelectControl.Name = "chkSelectControl";
            this.chkSelectControl.Size = new System.Drawing.Size(15, 14);
            this.chkSelectControl.TabIndex = 146;
            this.chkSelectControl.UseVisualStyleBackColor = true;
            this.chkSelectControl.Visible = false;
            this.chkSelectControl.CheckedChanged += new System.EventHandler(this.chkSelectControl_CheckedChanged);
            this.chkSelectControl.CheckStateChanged += new System.EventHandler(this.chkSelectControl_CheckStateChanged);
            // 
            // cmbSaleType
            // 
            this.cmbSaleType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbSaleType.FormattingEnabled = true;
            this.cmbSaleType.Location = new System.Drawing.Point(69, 4);
            this.cmbSaleType.Name = "cmbSaleType";
            this.cmbSaleType.Size = new System.Drawing.Size(69, 21);
            this.cmbSaleType.TabIndex = 147;
            this.cmbSaleType.SelectedIndexChanged += new System.EventHandler(this.cmbSaleType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(620, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 148;
            this.label1.Text = "$";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(595, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 149;
            this.label2.Text = "%";
            // 
            // labelCouponAmt
            // 
            this.labelCouponAmt.AutoSize = true;
            this.labelCouponAmt.BackColor = System.Drawing.Color.Transparent;
            this.labelCouponAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCouponAmt.Location = new System.Drawing.Point(768, 11);
            this.labelCouponAmt.Name = "labelCouponAmt";
            this.labelCouponAmt.Size = new System.Drawing.Size(43, 13);
            this.labelCouponAmt.TabIndex = 150;
            this.labelCouponAmt.Text = "$ 10.00";
            // 
            // txtQty
            // 
            this.txtQty.AllowOnlyNumbers = true;
            this.txtQty.CausesValidation = false;
            this.txtQty.Enabled = false;
            this.txtQty.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(725, 9);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(28, 18);
            this.txtQty.TabIndex = 16;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtQty.Click += new System.EventHandler(this.txtQty_Click);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // txtNegotiatedPrice
            // 
            this.txtNegotiatedPrice.CausesValidation = false;
            this.txtNegotiatedPrice.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtNegotiatedPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNegotiatedPrice.Location = new System.Drawing.Point(634, 9);
            this.txtNegotiatedPrice.Name = "txtNegotiatedPrice";
            this.txtNegotiatedPrice.Size = new System.Drawing.Size(60, 18);
            this.txtNegotiatedPrice.TabIndex = 13;
            this.txtNegotiatedPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNegotiatedPrice.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtNegotiatedPrice.Click += new System.EventHandler(this.txtNegotiatedPrice_Click);
            this.txtNegotiatedPrice.Leave += new System.EventHandler(this.txtNegotiatedPrice_Leave);
            // 
            // txtDiscount
            // 
            this.txtDiscount.CausesValidation = false;
            this.txtDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtDiscount.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(543, 9);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(49, 18);
            this.txtDiscount.TabIndex = 12;
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtDiscount.Click += new System.EventHandler(this.txtDiscount_Click);
            this.txtDiscount.Leave += new System.EventHandler(this.txtDiscount_Leave);
            // 
            // lblComments
            // 
            this.lblComments.Font = new System.Drawing.Font("Courier New", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComments.Location = new System.Drawing.Point(277, 34);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(599, 16);
            this.lblComments.TabIndex = 151;
            this.lblComments.Text = "0";
            // 
            // RetailItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.labelCouponAmt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSaleType);
            this.Controls.Add(this.chkSelectControl);
            this.Controls.Add(this.btnCoupon);
            this.Controls.Add(this.lblICN);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtNegotiatedPrice);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.lblRetailPrice);
            this.Name = "RetailItems";
            this.Size = new System.Drawing.Size(879, 50);
            this.Load += new System.EventHandler(this.RetailItems_Load);
            this.Click += new System.EventHandler(this.RetailItems_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRetailPrice;
        private CustomTextBox txtDiscount;
        private CustomTextBox txtNegotiatedPrice;
        private System.Windows.Forms.Label lblTotal;
        private CustomTextBox txtQty;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblICN;
        private System.Windows.Forms.Button btnCoupon;
        private System.Windows.Forms.CheckBox chkSelectControl;
        private System.Windows.Forms.ComboBox cmbSaleType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCouponAmt;
        private System.Windows.Forms.Label lblComments;
    }
}
