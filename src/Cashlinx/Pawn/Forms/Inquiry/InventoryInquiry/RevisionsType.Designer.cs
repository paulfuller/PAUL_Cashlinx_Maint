using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    partial class RevisionsType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevisionsType));
            this.report_type_cb = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CancelBtn = new CustomButton();
            this.ContinueBtn = new CustomButton();
            this.labelHeading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // report_type_cb
            // 
            this.report_type_cb.FormattingEnabled = true;
            this.report_type_cb.Items.AddRange(new object[] {
            "Cost Revision",
            "Retail Price Change",
            "Status Change"});
            this.report_type_cb.Location = new System.Drawing.Point(81, 78);
            this.report_type_cb.Name = "report_type_cb";
            this.report_type_cb.Size = new System.Drawing.Size(187, 69);
            this.report_type_cb.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Please Select Revision Type";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelBtn.BackColor = System.Drawing.Color.Transparent;
            this.CancelBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CancelBtn.BackgroundImage")));
            this.CancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CancelBtn.FlatAppearance.BorderSize = 0;
            this.CancelBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CancelBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.ForeColor = System.Drawing.Color.White;
            this.CancelBtn.Location = new System.Drawing.Point(7, 166);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(0);
            this.CancelBtn.MaximumSize = new System.Drawing.Size(100, 50);
            this.CancelBtn.MinimumSize = new System.Drawing.Size(100, 50);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(100, 50);
            this.CancelBtn.TabIndex = 12;
            this.CancelBtn.Text = "Back";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ContinueBtn.BackColor = System.Drawing.Color.Transparent;
            this.ContinueBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ContinueBtn.BackgroundImage")));
            this.ContinueBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ContinueBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContinueBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ContinueBtn.FlatAppearance.BorderSize = 0;
            this.ContinueBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ContinueBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ContinueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueBtn.ForeColor = System.Drawing.Color.White;
            this.ContinueBtn.Location = new System.Drawing.Point(214, 166);
            this.ContinueBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ContinueBtn.MaximumSize = new System.Drawing.Size(100, 50);
            this.ContinueBtn.MinimumSize = new System.Drawing.Size(100, 50);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(100, 50);
            this.ContinueBtn.TabIndex = 11;
            this.ContinueBtn.Text = "OK";
            this.ContinueBtn.UseVisualStyleBackColor = false;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(23, 9);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(146, 19);
            this.labelHeading.TabIndex = 10;
            this.labelHeading.Text = "Item Revision Type";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RevisionsType
            // 
            this.AcceptButton = this.ContinueBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(323, 225);
            this.ControlBox = false;
            this.Controls.Add(this.report_type_cb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ContinueBtn);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RevisionsType";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "RevisionsType";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox report_type_cb;
        private System.Windows.Forms.Label label1;
        private CustomButton CancelBtn;
        private CustomButton ContinueBtn;
        private System.Windows.Forms.Label labelHeading;

    }
}