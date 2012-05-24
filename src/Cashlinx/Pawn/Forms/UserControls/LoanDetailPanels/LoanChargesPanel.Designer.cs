using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    partial class LoanChargesPanel
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
            this.PH_InterestChargeText = new System.Windows.Forms.Label();
            this.PH_InterestChargeLabel = new System.Windows.Forms.Label();
            this.PH_ServiceChargeText = new System.Windows.Forms.Label();
            this.PH_ServiceChargeLabel = new System.Windows.Forms.Label();
            this.PH_FeesLabel = new System.Windows.Forms.LinkLabel();
            this.PH_FeesText = new System.Windows.Forms.Label();
            this.PH_LateChargeText = new System.Windows.Forms.Label();
            this.PH_LateChargeLabel = new System.Windows.Forms.Label();
            this.PH_Amount1Text = new System.Windows.Forms.Label();
            this.PH_Amount1Label = new System.Windows.Forms.Label();
            this.PH_Amount2Text = new System.Windows.Forms.Label();
            this.PH_Amount2Label = new System.Windows.Forms.Label();
            this.PH_Amount3Text = new System.Windows.Forms.Label();
            this.PH_Amount3Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PH_PrincipalAmountText
            // 
            this.PH_PrincipalAmountText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PrincipalAmountText.ForeColor = System.Drawing.Color.Black;
            this.PH_PrincipalAmountText.Location = new System.Drawing.Point(170, 3);
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
            this.PH_PrincipalAmountLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_PrincipalAmountLabel.TabIndex = 129;
            this.PH_PrincipalAmountLabel.Text = "Principal Amount:";
            this.PH_PrincipalAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_InterestChargeText
            // 
            this.PH_InterestChargeText.BackColor = System.Drawing.Color.Transparent;
            this.PH_InterestChargeText.ForeColor = System.Drawing.Color.Black;
            this.PH_InterestChargeText.Location = new System.Drawing.Point(170, 33);
            this.PH_InterestChargeText.Name = "PH_InterestChargeText";
            this.PH_InterestChargeText.Size = new System.Drawing.Size(86, 30);
            this.PH_InterestChargeText.TabIndex = 132;
            this.PH_InterestChargeText.Text = "$0.00";
            this.PH_InterestChargeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_InterestChargeLabel
            // 
            this.PH_InterestChargeLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PH_InterestChargeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_InterestChargeLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_InterestChargeLabel.Location = new System.Drawing.Point(6, 33);
            this.PH_InterestChargeLabel.Name = "PH_InterestChargeLabel";
            this.PH_InterestChargeLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_InterestChargeLabel.TabIndex = 131;
            this.PH_InterestChargeLabel.Text = "Interest Charge:";
            this.PH_InterestChargeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ServiceChargeText
            // 
            this.PH_ServiceChargeText.BackColor = System.Drawing.Color.Transparent;
            this.PH_ServiceChargeText.ForeColor = System.Drawing.Color.Black;
            this.PH_ServiceChargeText.Location = new System.Drawing.Point(170, 63);
            this.PH_ServiceChargeText.Name = "PH_ServiceChargeText";
            this.PH_ServiceChargeText.Size = new System.Drawing.Size(86, 30);
            this.PH_ServiceChargeText.TabIndex = 134;
            this.PH_ServiceChargeText.Text = "$0.00";
            this.PH_ServiceChargeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ServiceChargeLabel
            // 
            this.PH_ServiceChargeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ServiceChargeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ServiceChargeLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ServiceChargeLabel.Location = new System.Drawing.Point(6, 63);
            this.PH_ServiceChargeLabel.Name = "PH_ServiceChargeLabel";
            this.PH_ServiceChargeLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_ServiceChargeLabel.TabIndex = 133;
            this.PH_ServiceChargeLabel.Text = "Service Charge:";
            this.PH_ServiceChargeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_FeesLabel
            // 
            this.PH_FeesLabel.AutoSize = true;
            this.PH_FeesLabel.Location = new System.Drawing.Point(134, 102);
            this.PH_FeesLabel.Name = "PH_FeesLabel";
            this.PH_FeesLabel.Size = new System.Drawing.Size(33, 13);
            this.PH_FeesLabel.TabIndex = 135;
            this.PH_FeesLabel.TabStop = true;
            this.PH_FeesLabel.Text = "Fees:";
            // 
            // PH_FeesText
            // 
            this.PH_FeesText.BackColor = System.Drawing.Color.Transparent;
            this.PH_FeesText.ForeColor = System.Drawing.Color.Black;
            this.PH_FeesText.Location = new System.Drawing.Point(170, 93);
            this.PH_FeesText.Name = "PH_FeesText";
            this.PH_FeesText.Size = new System.Drawing.Size(86, 30);
            this.PH_FeesText.TabIndex = 136;
            this.PH_FeesText.Text = "$0.00";
            this.PH_FeesText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_LateChargeText
            // 
            this.PH_LateChargeText.BackColor = System.Drawing.Color.Transparent;
            this.PH_LateChargeText.ForeColor = System.Drawing.Color.Black;
            this.PH_LateChargeText.Location = new System.Drawing.Point(170, 123);
            this.PH_LateChargeText.Name = "PH_LateChargeText";
            this.PH_LateChargeText.Size = new System.Drawing.Size(86, 30);
            this.PH_LateChargeText.TabIndex = 138;
            this.PH_LateChargeText.Text = "$0.00";
            this.PH_LateChargeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_LateChargeLabel
            // 
            this.PH_LateChargeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_LateChargeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_LateChargeLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_LateChargeLabel.Location = new System.Drawing.Point(6, 123);
            this.PH_LateChargeLabel.Name = "PH_LateChargeLabel";
            this.PH_LateChargeLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_LateChargeLabel.TabIndex = 137;
            this.PH_LateChargeLabel.Text = "Late Charge:";
            this.PH_LateChargeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_Amount1Text
            // 
            this.PH_Amount1Text.BackColor = System.Drawing.Color.Transparent;
            this.PH_Amount1Text.ForeColor = System.Drawing.Color.Black;
            this.PH_Amount1Text.Location = new System.Drawing.Point(170, 153);
            this.PH_Amount1Text.Name = "PH_Amount1Text";
            this.PH_Amount1Text.Size = new System.Drawing.Size(86, 30);
            this.PH_Amount1Text.TabIndex = 140;
            this.PH_Amount1Text.Text = "$0.00";
            this.PH_Amount1Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_Amount1Label
            // 
            this.PH_Amount1Label.BackColor = System.Drawing.Color.Transparent;
            this.PH_Amount1Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_Amount1Label.ForeColor = System.Drawing.Color.Black;
            this.PH_Amount1Label.Location = new System.Drawing.Point(3, 153);
            this.PH_Amount1Label.Name = "PH_Amount1Label";
            this.PH_Amount1Label.Size = new System.Drawing.Size(164, 30);
            this.PH_Amount1Label.TabIndex = 139;
            this.PH_Amount1Label.Text = "Amount1:";
            this.PH_Amount1Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_Amount2Text
            // 
            this.PH_Amount2Text.BackColor = System.Drawing.Color.Transparent;
            this.PH_Amount2Text.ForeColor = System.Drawing.Color.Black;
            this.PH_Amount2Text.Location = new System.Drawing.Point(170, 183);
            this.PH_Amount2Text.Name = "PH_Amount2Text";
            this.PH_Amount2Text.Size = new System.Drawing.Size(86, 30);
            this.PH_Amount2Text.TabIndex = 142;
            this.PH_Amount2Text.Text = "$0.00";
            this.PH_Amount2Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_Amount2Label
            // 
            this.PH_Amount2Label.BackColor = System.Drawing.Color.Transparent;
            this.PH_Amount2Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_Amount2Label.ForeColor = System.Drawing.Color.Black;
            this.PH_Amount2Label.Location = new System.Drawing.Point(6, 183);
            this.PH_Amount2Label.Name = "PH_Amount2Label";
            this.PH_Amount2Label.Size = new System.Drawing.Size(161, 30);
            this.PH_Amount2Label.TabIndex = 141;
            this.PH_Amount2Label.Text = "Amount2:";
            this.PH_Amount2Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_Amount3Text
            // 
            this.PH_Amount3Text.BackColor = System.Drawing.Color.Transparent;
            this.PH_Amount3Text.ForeColor = System.Drawing.Color.Black;
            this.PH_Amount3Text.Location = new System.Drawing.Point(170, 213);
            this.PH_Amount3Text.Name = "PH_Amount3Text";
            this.PH_Amount3Text.Size = new System.Drawing.Size(86, 30);
            this.PH_Amount3Text.TabIndex = 144;
            this.PH_Amount3Text.Text = "$0.00";
            this.PH_Amount3Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PH_Amount3Text.Visible = false;
            // 
            // PH_Amount3Label
            // 
            this.PH_Amount3Label.BackColor = System.Drawing.Color.Transparent;
            this.PH_Amount3Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_Amount3Label.ForeColor = System.Drawing.Color.Black;
            this.PH_Amount3Label.Location = new System.Drawing.Point(6, 213);
            this.PH_Amount3Label.Name = "PH_Amount3Label";
            this.PH_Amount3Label.Size = new System.Drawing.Size(161, 30);
            this.PH_Amount3Label.TabIndex = 143;
            this.PH_Amount3Label.Text = "Amount3:";
            this.PH_Amount3Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PH_Amount3Label.Visible = false;
            // 
            // LoanChargesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.PH_Amount3Text);
            this.Controls.Add(this.PH_Amount3Label);
            this.Controls.Add(this.PH_Amount2Text);
            this.Controls.Add(this.PH_Amount2Label);
            this.Controls.Add(this.PH_Amount1Text);
            this.Controls.Add(this.PH_Amount1Label);
            this.Controls.Add(this.PH_LateChargeText);
            this.Controls.Add(this.PH_LateChargeLabel);
            this.Controls.Add(this.PH_FeesText);
            this.Controls.Add(this.PH_FeesLabel);
            this.Controls.Add(this.PH_ServiceChargeText);
            this.Controls.Add(this.PH_ServiceChargeLabel);
            this.Controls.Add(this.PH_InterestChargeText);
            this.Controls.Add(this.PH_InterestChargeLabel);
            this.Controls.Add(this.PH_PrincipalAmountText);
            this.Controls.Add(this.PH_PrincipalAmountLabel);
            this.Name = "LoanChargesPanel";
            this.Size = new System.Drawing.Size(259, 261);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PH_PrincipalAmountText;
        private System.Windows.Forms.Label PH_PrincipalAmountLabel;
        private System.Windows.Forms.Label PH_InterestChargeText;
        private System.Windows.Forms.Label PH_InterestChargeLabel;
        private System.Windows.Forms.Label PH_ServiceChargeText;
        private System.Windows.Forms.Label PH_ServiceChargeLabel;
        private System.Windows.Forms.LinkLabel PH_FeesLabel;
        private System.Windows.Forms.Label PH_FeesText;
        private System.Windows.Forms.Label PH_LateChargeText;
        private System.Windows.Forms.Label PH_LateChargeLabel;
        private System.Windows.Forms.Label PH_Amount1Text;
        private System.Windows.Forms.Label PH_Amount1Label;
        private System.Windows.Forms.Label PH_Amount2Text;
        private System.Windows.Forms.Label PH_Amount2Label;
        private System.Windows.Forms.Label PH_Amount3Text;
        private System.Windows.Forms.Label PH_Amount3Label;
    }
}
