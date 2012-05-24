using Common.Libraries.Forms.Components;

namespace Pawn.Forms.LoanInquiry
{
    partial class LoanInquiryShops
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanInquiryShops));
            this.labelHeading = new System.Windows.Forms.Label();
            this.imageListLoanInquiryLocations = new System.Windows.Forms.ImageList(this.components);
            this.customButtonCancel = new CustomButton();
            this.customButtonClear = new CustomButton();
            this.customButtonContinue = new CustomButton();
            this.panelCriteriaPanel = new System.Windows.Forms.Panel();
            this.panelLocations = new System.Windows.Forms.Panel();
            this.panelCriteriaPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(28, 41);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(141, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Select Locations";
            // 
            // imageListLoanInquiryLocations
            // 
            this.imageListLoanInquiryLocations.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListLoanInquiryLocations.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListLoanInquiryLocations.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(38, 476);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 16;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonClear
            // 
            this.customButtonClear.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonClear.BackgroundImage")));
            this.customButtonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClear.FlatAppearance.BorderSize = 0;
            this.customButtonClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClear.ForeColor = System.Drawing.Color.White;
            this.customButtonClear.Location = new System.Drawing.Point(218, 476);
            this.customButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClear.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.Name = "customButtonClear";
            this.customButtonClear.Size = new System.Drawing.Size(100, 50);
            this.customButtonClear.TabIndex = 17;
            this.customButtonClear.Text = "C&lear";
            this.customButtonClear.UseVisualStyleBackColor = false;
            this.customButtonClear.Click += new System.EventHandler(this.customButtonClear_Click);
            // 
            // customButtonContinue
            // 
            this.customButtonContinue.BackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonContinue.BackgroundImage")));
            this.customButtonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonContinue.FlatAppearance.BorderSize = 0;
            this.customButtonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonContinue.ForeColor = System.Drawing.Color.White;
            this.customButtonContinue.Location = new System.Drawing.Point(383, 476);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 32;
            this.customButtonContinue.Text = "Co&ntinue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.customButtonContinue_Click);
            // 
            // panelCriteriaPanel
            // 
            this.panelCriteriaPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelCriteriaPanel.Controls.Add(this.panelLocations);
            this.panelCriteriaPanel.Controls.Add(this.customButtonContinue);
            this.panelCriteriaPanel.Controls.Add(this.customButtonClear);
            this.panelCriteriaPanel.Controls.Add(this.customButtonCancel);
            this.panelCriteriaPanel.Location = new System.Drawing.Point(6, 88);
            this.panelCriteriaPanel.Name = "panelCriteriaPanel";
            this.panelCriteriaPanel.Size = new System.Drawing.Size(533, 549);
            this.panelCriteriaPanel.TabIndex = 34;
            // 
            // panelLocations
            // 
            this.panelLocations.AutoScroll = true;
            this.panelLocations.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelLocations.Location = new System.Drawing.Point(48, 9);
            this.panelLocations.Name = "panelLocations";
            this.panelLocations.Size = new System.Drawing.Size(437, 459);
            this.panelLocations.TabIndex = 34;
            // 
            // LoanInquiryLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(545, 646);
            this.ControlBox = false;
            this.Controls.Add(this.panelCriteriaPanel);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoanInquiryLocations";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Loan Inquiry Locations";
            this.Load += new System.EventHandler(this.LoanInquiryMultStore_Load);
            this.panelCriteriaPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.ImageList imageListLoanInquiryLocations;
        private CustomButton customButtonCancel;
        private CustomButton customButtonClear;
        private CustomButton customButtonContinue;
        private System.Windows.Forms.Panel panelCriteriaPanel;
        private System.Windows.Forms.Panel panelLocations;
    }
}