using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    partial class LoanPFIPanel
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
            this.PH_PrincipalAmountText = new System.Windows.Forms.Label();
            this.PH_PrincipalAmountLabel = new System.Windows.Forms.Label();
            this.PH_ExtFeeText = new System.Windows.Forms.Label();
            this.PH_ExtFeeLabel = new System.Windows.Forms.Label();
            this.PH_PFIEligibilityDTText = new System.Windows.Forms.Label();
            this.PH_PFIEligibilityDTLabel = new System.Windows.Forms.Label();
            this.PH_ExtPFIEligibilityDTText = new System.Windows.Forms.Label();
            this.PH_PFINotificationDTText = new System.Windows.Forms.Label();
            this.PH_PFINotificationDTLabel = new System.Windows.Forms.Label();
            this.PH_ExtPFINotificationDTText = new System.Windows.Forms.Label();
            this.PH_ExtPFINotificationDTLabel = new System.Windows.Forms.Label();
            this.PH_ExtPFIEligibilityDTLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PH_PrincipalAmountText
            // 
            this.PH_PrincipalAmountText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PrincipalAmountText.ForeColor = System.Drawing.Color.Black;
            this.PH_PrincipalAmountText.Location = new System.Drawing.Point(193, 3);
            this.PH_PrincipalAmountText.Name = "PH_PrincipalAmountText";
            this.PH_PrincipalAmountText.Size = new System.Drawing.Size(86, 30);
            this.PH_PrincipalAmountText.TabIndex = 130;
            this.PH_PrincipalAmountText.Text = "$0.00";
            this.PH_PrincipalAmountText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_PrincipalAmountLabel
            // 
            this.PH_PrincipalAmountLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_PrincipalAmountLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_PrincipalAmountLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_PrincipalAmountLabel.Location = new System.Drawing.Point(3, 3);
            this.PH_PrincipalAmountLabel.Name = "PH_PrincipalAmountLabel";
            this.PH_PrincipalAmountLabel.Size = new System.Drawing.Size(181, 30);
            this.PH_PrincipalAmountLabel.TabIndex = 129;
            this.PH_PrincipalAmountLabel.Text = "Principal Amount:";
            this.PH_PrincipalAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ExtFeeText
            // 
            this.PH_ExtFeeText.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtFeeText.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtFeeText.Location = new System.Drawing.Point(193, 33);
            this.PH_ExtFeeText.Name = "PH_ExtFeeText";
            this.PH_ExtFeeText.Size = new System.Drawing.Size(86, 30);
            this.PH_ExtFeeText.TabIndex = 132;
            this.PH_ExtFeeText.Text = "$0.00";
            this.PH_ExtFeeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ExtFeeLabel
            // 
            this.PH_ExtFeeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtFeeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ExtFeeLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtFeeLabel.Location = new System.Drawing.Point(6, 33);
            this.PH_ExtFeeLabel.Name = "PH_ExtFeeLabel";
            this.PH_ExtFeeLabel.Size = new System.Drawing.Size(181, 30);
            this.PH_ExtFeeLabel.TabIndex = 131;
            this.PH_ExtFeeLabel.Text = "Extension Fee:";
            this.PH_ExtFeeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_PFIEligibilityDTText
            // 
            this.PH_PFIEligibilityDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PFIEligibilityDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_PFIEligibilityDTText.Location = new System.Drawing.Point(193, 63);
            this.PH_PFIEligibilityDTText.Name = "PH_PFIEligibilityDTText";
            this.PH_PFIEligibilityDTText.Size = new System.Drawing.Size(86, 30);
            this.PH_PFIEligibilityDTText.TabIndex = 134;
            this.PH_PFIEligibilityDTText.Text = "12/21/2012";
            this.PH_PFIEligibilityDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_PFIEligibilityDTLabel
            // 
            this.PH_PFIEligibilityDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_PFIEligibilityDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_PFIEligibilityDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_PFIEligibilityDTLabel.Location = new System.Drawing.Point(6, 63);
            this.PH_PFIEligibilityDTLabel.Name = "PH_PFIEligibilityDTLabel";
            this.PH_PFIEligibilityDTLabel.Size = new System.Drawing.Size(181, 30);
            this.PH_PFIEligibilityDTLabel.TabIndex = 133;
            this.PH_PFIEligibilityDTLabel.Text = "PFI Eligibility Date:";
            this.PH_PFIEligibilityDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ExtPFIEligibilityDTText
            // 
            this.PH_ExtPFIEligibilityDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtPFIEligibilityDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtPFIEligibilityDTText.Location = new System.Drawing.Point(193, 93);
            this.PH_ExtPFIEligibilityDTText.Name = "PH_ExtPFIEligibilityDTText";
            this.PH_ExtPFIEligibilityDTText.Size = new System.Drawing.Size(86, 30);
            this.PH_ExtPFIEligibilityDTText.TabIndex = 136;
            this.PH_ExtPFIEligibilityDTText.Text = "12/21/2012";
            this.PH_ExtPFIEligibilityDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_PFINotificationDTText
            // 
            this.PH_PFINotificationDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PFINotificationDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_PFINotificationDTText.Location = new System.Drawing.Point(193, 123);
            this.PH_PFINotificationDTText.Name = "PH_PFINotificationDTText";
            this.PH_PFINotificationDTText.Size = new System.Drawing.Size(86, 30);
            this.PH_PFINotificationDTText.TabIndex = 138;
            this.PH_PFINotificationDTText.Text = "12/21/2012";
            this.PH_PFINotificationDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_PFINotificationDTLabel
            // 
            this.PH_PFINotificationDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_PFINotificationDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_PFINotificationDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_PFINotificationDTLabel.Location = new System.Drawing.Point(6, 123);
            this.PH_PFINotificationDTLabel.Name = "PH_PFINotificationDTLabel";
            this.PH_PFINotificationDTLabel.Size = new System.Drawing.Size(181, 30);
            this.PH_PFINotificationDTLabel.TabIndex = 137;
            this.PH_PFINotificationDTLabel.Text = "PFI Notification Date:";
            this.PH_PFINotificationDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ExtPFINotificationDTText
            // 
            this.PH_ExtPFINotificationDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtPFINotificationDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtPFINotificationDTText.Location = new System.Drawing.Point(193, 153);
            this.PH_ExtPFINotificationDTText.Name = "PH_ExtPFINotificationDTText";
            this.PH_ExtPFINotificationDTText.Size = new System.Drawing.Size(86, 30);
            this.PH_ExtPFINotificationDTText.TabIndex = 140;
            this.PH_ExtPFINotificationDTText.Text = "12/21/2012";
            this.PH_ExtPFINotificationDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ExtPFINotificationDTLabel
            // 
            this.PH_ExtPFINotificationDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtPFINotificationDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ExtPFINotificationDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtPFINotificationDTLabel.Location = new System.Drawing.Point(0, 153);
            this.PH_ExtPFINotificationDTLabel.Name = "PH_ExtPFINotificationDTLabel";
            this.PH_ExtPFINotificationDTLabel.Size = new System.Drawing.Size(187, 30);
            this.PH_ExtPFINotificationDTLabel.TabIndex = 139;
            this.PH_ExtPFINotificationDTLabel.Text = "Extension PFI Notification Date:";
            this.PH_ExtPFINotificationDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ExtPFIEligibilityDTLabel
            // 
            this.PH_ExtPFIEligibilityDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtPFIEligibilityDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ExtPFIEligibilityDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtPFIEligibilityDTLabel.Location = new System.Drawing.Point(6, 93);
            this.PH_ExtPFIEligibilityDTLabel.Name = "PH_ExtPFIEligibilityDTLabel";
            this.PH_ExtPFIEligibilityDTLabel.Size = new System.Drawing.Size(181, 30);
            this.PH_ExtPFIEligibilityDTLabel.TabIndex = 143;
            this.PH_ExtPFIEligibilityDTLabel.Text = "Extended PFI Eligibility Date:";
            this.PH_ExtPFIEligibilityDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LoanPFIPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.PH_ExtPFIEligibilityDTLabel);
            this.Controls.Add(this.PH_ExtPFINotificationDTText);
            this.Controls.Add(this.PH_ExtPFINotificationDTLabel);
            this.Controls.Add(this.PH_PFINotificationDTText);
            this.Controls.Add(this.PH_PFINotificationDTLabel);
            this.Controls.Add(this.PH_ExtPFIEligibilityDTText);
            this.Controls.Add(this.PH_PFIEligibilityDTText);
            this.Controls.Add(this.PH_PFIEligibilityDTLabel);
            this.Controls.Add(this.PH_ExtFeeText);
            this.Controls.Add(this.PH_ExtFeeLabel);
            this.Controls.Add(this.PH_PrincipalAmountText);
            this.Controls.Add(this.PH_PrincipalAmountLabel);
            this.Name = "LoanPFIPanel";
            this.Size = new System.Drawing.Size(280, 227);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PH_PrincipalAmountText;
        private System.Windows.Forms.Label PH_PrincipalAmountLabel;
        private System.Windows.Forms.Label PH_ExtFeeText;
        private System.Windows.Forms.Label PH_ExtFeeLabel;
        private System.Windows.Forms.Label PH_PFIEligibilityDTText;
        private System.Windows.Forms.Label PH_PFIEligibilityDTLabel;
        private System.Windows.Forms.Label PH_ExtPFIEligibilityDTText;
        private System.Windows.Forms.Label PH_PFINotificationDTText;
        private System.Windows.Forms.Label PH_PFINotificationDTLabel;
        private System.Windows.Forms.Label PH_ExtPFINotificationDTText;
        private System.Windows.Forms.Label PH_ExtPFINotificationDTLabel;
        private System.Windows.Forms.Label PH_ExtPFIEligibilityDTLabel;
    }
}
