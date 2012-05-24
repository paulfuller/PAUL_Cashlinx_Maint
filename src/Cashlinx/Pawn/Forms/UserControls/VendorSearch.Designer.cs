using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls
{
    partial class VendorSearch
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.vendorSearchGroup = new System.Windows.Forms.Panel();
            this.lookupVendorTaxID = new CustomTextBox();
            this.VendorNameRadiobtn = new System.Windows.Forms.RadioButton();
            this.TaxIDRadiobtn = new System.Windows.Forms.RadioButton();
            this.lookupVendorName = new CustomTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.VendorRadiobtn = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.vendorSearchGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.vendorSearchGroup);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.VendorRadiobtn);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 100);
            this.panel1.TabIndex = 31;
            // 
            // vendorSearchGroup
            // 
            this.vendorSearchGroup.Controls.Add(this.lookupVendorTaxID);
            this.vendorSearchGroup.Controls.Add(this.VendorNameRadiobtn);
            this.vendorSearchGroup.Controls.Add(this.TaxIDRadiobtn);
            this.vendorSearchGroup.Controls.Add(this.lookupVendorName);
            this.vendorSearchGroup.Location = new System.Drawing.Point(3, 28);
            this.vendorSearchGroup.Name = "vendorSearchGroup";
            this.vendorSearchGroup.Size = new System.Drawing.Size(437, 62);
            this.vendorSearchGroup.TabIndex = 10;
            // 
            // lookupVendorTaxID
            // 
            this.lookupVendorTaxID.BackColor = System.Drawing.Color.White;
            this.lookupVendorTaxID.CausesValidation = false;
            this.lookupVendorTaxID.Enabled = false;
            this.lookupVendorTaxID.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupVendorTaxID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupVendorTaxID.Location = new System.Drawing.Point(164, 34);
            this.lookupVendorTaxID.MaxLength = 20;
            this.lookupVendorTaxID.Name = "lookupVendorTaxID";
            this.lookupVendorTaxID.Size = new System.Drawing.Size(154, 21);
            this.lookupVendorTaxID.TabIndex = 13;
            this.lookupVendorTaxID.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // VendorNameRadiobtn
            // 
            this.VendorNameRadiobtn.AutoSize = true;
            this.VendorNameRadiobtn.Checked = true;
            this.VendorNameRadiobtn.Location = new System.Drawing.Point(31, 12);
            this.VendorNameRadiobtn.Name = "VendorNameRadiobtn";
            this.VendorNameRadiobtn.Size = new System.Drawing.Size(90, 17);
            this.VendorNameRadiobtn.TabIndex = 14;
            this.VendorNameRadiobtn.TabStop = true;
            this.VendorNameRadiobtn.Text = "Vendor Name";
            this.VendorNameRadiobtn.UseVisualStyleBackColor = true;
            this.VendorNameRadiobtn.CheckedChanged += new System.EventHandler(this.VendorNameRadiobtn_CheckedChanged);
            // 
            // TaxIDRadiobtn
            // 
            this.TaxIDRadiobtn.AutoSize = true;
            this.TaxIDRadiobtn.Location = new System.Drawing.Point(31, 35);
            this.TaxIDRadiobtn.Name = "TaxIDRadiobtn";
            this.TaxIDRadiobtn.Size = new System.Drawing.Size(97, 17);
            this.TaxIDRadiobtn.TabIndex = 15;
            this.TaxIDRadiobtn.Text = "Tax ID Number";
            this.TaxIDRadiobtn.UseVisualStyleBackColor = true;
            // 
            // lookupVendorName
            // 
            this.lookupVendorName.BackColor = System.Drawing.Color.White;
            this.lookupVendorName.CausesValidation = false;
            this.lookupVendorName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupVendorName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupVendorName.Location = new System.Drawing.Point(164, 8);
            this.lookupVendorName.MaxLength = 20;
            this.lookupVendorName.Name = "lookupVendorName";
            this.lookupVendorName.Size = new System.Drawing.Size(154, 21);
            this.lookupVendorName.TabIndex = 11;
            this.lookupVendorName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(0, 24);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(622, 2);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            // 
            // VendorRadiobtn
            // 
            this.VendorRadiobtn.CausesValidation = false;
            this.VendorRadiobtn.Checked = true;
            this.VendorRadiobtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VendorRadiobtn.Location = new System.Drawing.Point(3, 4);
            this.VendorRadiobtn.Name = "VendorRadiobtn";
            this.VendorRadiobtn.Size = new System.Drawing.Size(164, 18);
            this.VendorRadiobtn.TabIndex = 0;
            this.VendorRadiobtn.TabStop = true;
            this.VendorRadiobtn.Text = "Vendor Information";
            this.VendorRadiobtn.UseVisualStyleBackColor = true;
            this.VendorRadiobtn.CheckedChanged += new System.EventHandler(this.VendorRadiobtn_CheckedChanged);
            // 
            // VendorSearch
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "VendorSearch";
            this.Size = new System.Drawing.Size(617, 109);
            this.Load += new System.EventHandler(this.VendorSearch_Load);
            this.panel1.ResumeLayout(false);
            this.vendorSearchGroup.ResumeLayout(false);
            this.vendorSearchGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel vendorSearchGroup;
        private CustomTextBox lookupVendorTaxID;
        private System.Windows.Forms.RadioButton VendorNameRadiobtn;
        private System.Windows.Forms.RadioButton TaxIDRadiobtn;
        private CustomTextBox lookupVendorName;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton VendorRadiobtn;
    }
}
