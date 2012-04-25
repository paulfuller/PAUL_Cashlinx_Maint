using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    partial class LoanFireamPickupPanel
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
            this.PH_FirearmIDLabel = new System.Windows.Forms.Label();
            this.PH_FirearmIDText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PH_FirearmIDLabel
            // 
            this.PH_FirearmIDLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_FirearmIDLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_FirearmIDLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_FirearmIDLabel.Location = new System.Drawing.Point(2, 0);
            this.PH_FirearmIDLabel.Name = "PH_FirearmIDLabel";
            this.PH_FirearmIDLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_FirearmIDLabel.TabIndex = 145;
            this.PH_FirearmIDLabel.Text = "Pickup identification:";
            this.PH_FirearmIDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PH_FirearmIDText
            // 
            this.PH_FirearmIDText.BackColor = System.Drawing.Color.Transparent;
            this.PH_FirearmIDText.ForeColor = System.Drawing.Color.Black;
            this.PH_FirearmIDText.Location = new System.Drawing.Point(169, 0);
            this.PH_FirearmIDText.Name = "PH_FirearmIDText";
            this.PH_FirearmIDText.Size = new System.Drawing.Size(135, 73);
            this.PH_FirearmIDText.TabIndex = 146;
            // 
            // LoanFireamPickupPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.PH_FirearmIDLabel);
            this.Controls.Add(this.PH_FirearmIDText);
            this.Name = "LoanFireamPickupPanel";
            this.Size = new System.Drawing.Size(314, 73);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PH_FirearmIDLabel;
        private System.Windows.Forms.Label PH_FirearmIDText;


    }
}
