using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class PreviousAuditTrakker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviousAuditTrakker));
            this.btnOk = new CustomButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPreviousLocation = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(77, 116);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnOk.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 50);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(66, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Audit Location Scanned:";
            // 
            // lblPreviousLocation
            // 
            this.lblPreviousLocation.BackColor = System.Drawing.Color.Transparent;
            this.lblPreviousLocation.Location = new System.Drawing.Point(77, 47);
            this.lblPreviousLocation.Name = "lblPreviousLocation";
            this.lblPreviousLocation.Size = new System.Drawing.Size(100, 13);
            this.lblPreviousLocation.TabIndex = 2;
            this.lblPreviousLocation.Text = "label2";
            this.lblPreviousLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lblPreviousLocation);
            this.panel1.Location = new System.Drawing.Point(2, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 174);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Previous Audit Trakker Inquiry";
            // 
            // PreviousAuditTrakker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audit.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(261, 214);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "PreviousAuditTrakker";
            this.Text = "PreviousAuditTrakker";
            this.Load += new System.EventHandler(this.PreviousAuditTrakker_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPreviousLocation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
    }
}