using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls.ProductHistoryPanels
{
    partial class LoanBasicInfoPanel
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
            this.PH_LoanAmountText = new System.Windows.Forms.Label();
            this.PH_LoanAmountLabel = new System.Windows.Forms.Label();
            this.PH_DueDTText = new System.Windows.Forms.Label();
            this.PH_DueDTLabel = new System.Windows.Forms.Label();
            this.PH_OriginationDTText = new System.Windows.Forms.Label();
            this.PH_OriginationDTLabel = new System.Windows.Forms.Label();
            this.PH_OriginalLoanNumberText = new System.Windows.Forms.Label();
            this.PH_OriginalLoanNumberLabel = new System.Windows.Forms.Label();
            this.PH_PreviousLoanNumberText = new System.Windows.Forms.Label();
            this.PH_PreviousLoanNumberLabel = new System.Windows.Forms.Label();
            this.PH_CurrentLoanNumberText = new System.Windows.Forms.Label();
            this.PH_CurrentLoanNumberLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PH_LoanAmountText
            // 
            this.PH_LoanAmountText.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanAmountText.ForeColor = System.Drawing.Color.Black;
            this.PH_LoanAmountText.Location = new System.Drawing.Point(169, 62);
            this.PH_LoanAmountText.Name = "PH_LoanAmountText";
            this.PH_LoanAmountText.Size = new System.Drawing.Size(129, 30);
            this.PH_LoanAmountText.TabIndex = 152;
            this.PH_LoanAmountText.Text = "$0.00";
            this.PH_LoanAmountText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_LoanAmountLabel
            // 
            this.PH_LoanAmountLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanAmountLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_LoanAmountLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_LoanAmountLabel.Location = new System.Drawing.Point(2, 62);
            this.PH_LoanAmountLabel.Name = "PH_LoanAmountLabel";
            this.PH_LoanAmountLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_LoanAmountLabel.TabIndex = 151;
            this.PH_LoanAmountLabel.Text = "Original Loan Amount:";
            this.PH_LoanAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_DueDTText
            // 
            this.PH_DueDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_DueDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_DueDTText.Location = new System.Drawing.Point(169, 32);
            this.PH_DueDTText.Name = "PH_DueDTText";
            this.PH_DueDTText.Size = new System.Drawing.Size(129, 30);
            this.PH_DueDTText.TabIndex = 150;
            this.PH_DueDTText.Text = "12/31/2010";
            this.PH_DueDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_DueDTLabel
            // 
            this.PH_DueDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_DueDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_DueDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_DueDTLabel.Location = new System.Drawing.Point(2, 32);
            this.PH_DueDTLabel.Name = "PH_DueDTLabel";
            this.PH_DueDTLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_DueDTLabel.TabIndex = 149;
            this.PH_DueDTLabel.Text = "Due Date:";
            this.PH_DueDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_OriginationDTText
            // 
            this.PH_OriginationDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_OriginationDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_OriginationDTText.Location = new System.Drawing.Point(169, 2);
            this.PH_OriginationDTText.Name = "PH_OriginationDTText";
            this.PH_OriginationDTText.Size = new System.Drawing.Size(129, 30);
            this.PH_OriginationDTText.TabIndex = 148;
            this.PH_OriginationDTText.Text = "12/31/2000 11:00 AM";
            this.PH_OriginationDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_OriginationDTLabel
            // 
            this.PH_OriginationDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_OriginationDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_OriginationDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_OriginationDTLabel.Location = new System.Drawing.Point(2, 2);
            this.PH_OriginationDTLabel.Name = "PH_OriginationDTLabel";
            this.PH_OriginationDTLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_OriginationDTLabel.TabIndex = 147;
            this.PH_OriginationDTLabel.Text = "Origination Date/Time:";
            this.PH_OriginationDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_OriginalLoanNumberText
            // 
            this.PH_OriginalLoanNumberText.BackColor = System.Drawing.Color.Transparent;
            this.PH_OriginalLoanNumberText.ForeColor = System.Drawing.Color.Black;
            this.PH_OriginalLoanNumberText.Location = new System.Drawing.Point(169, 92);
            this.PH_OriginalLoanNumberText.Name = "PH_OriginalLoanNumberText";
            this.PH_OriginalLoanNumberText.Size = new System.Drawing.Size(129, 30);
            this.PH_OriginalLoanNumberText.TabIndex = 154;
            this.PH_OriginalLoanNumberText.Text = "000000";
            this.PH_OriginalLoanNumberText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_OriginalLoanNumberLabel
            // 
            this.PH_OriginalLoanNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_OriginalLoanNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_OriginalLoanNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_OriginalLoanNumberLabel.Location = new System.Drawing.Point(2, 92);
            this.PH_OriginalLoanNumberLabel.Name = "PH_OriginalLoanNumberLabel";
            this.PH_OriginalLoanNumberLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_OriginalLoanNumberLabel.TabIndex = 153;
            this.PH_OriginalLoanNumberLabel.Text = "Original Loan Number:";
            this.PH_OriginalLoanNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_PreviousLoanNumberText
            // 
            this.PH_PreviousLoanNumberText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PreviousLoanNumberText.ForeColor = System.Drawing.Color.Black;
            this.PH_PreviousLoanNumberText.Location = new System.Drawing.Point(169, 122);
            this.PH_PreviousLoanNumberText.Name = "PH_PreviousLoanNumberText";
            this.PH_PreviousLoanNumberText.Size = new System.Drawing.Size(129, 30);
            this.PH_PreviousLoanNumberText.TabIndex = 156;
            this.PH_PreviousLoanNumberText.Text = "000000";
            this.PH_PreviousLoanNumberText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_PreviousLoanNumberLabel
            // 
            this.PH_PreviousLoanNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_PreviousLoanNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_PreviousLoanNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_PreviousLoanNumberLabel.Location = new System.Drawing.Point(2, 122);
            this.PH_PreviousLoanNumberLabel.Name = "PH_PreviousLoanNumberLabel";
            this.PH_PreviousLoanNumberLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_PreviousLoanNumberLabel.TabIndex = 155;
            this.PH_PreviousLoanNumberLabel.Text = "Previous Loan Number:";
            this.PH_PreviousLoanNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_CurrentLoanNumberText
            // 
            this.PH_CurrentLoanNumberText.BackColor = System.Drawing.Color.Transparent;
            this.PH_CurrentLoanNumberText.ForeColor = System.Drawing.Color.Black;
            this.PH_CurrentLoanNumberText.Location = new System.Drawing.Point(169, 152);
            this.PH_CurrentLoanNumberText.Name = "PH_CurrentLoanNumberText";
            this.PH_CurrentLoanNumberText.Size = new System.Drawing.Size(129, 30);
            this.PH_CurrentLoanNumberText.TabIndex = 158;
            this.PH_CurrentLoanNumberText.Text = "000000";
            this.PH_CurrentLoanNumberText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_CurrentLoanNumberLabel
            // 
            this.PH_CurrentLoanNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_CurrentLoanNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_CurrentLoanNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_CurrentLoanNumberLabel.Location = new System.Drawing.Point(2, 152);
            this.PH_CurrentLoanNumberLabel.Name = "PH_CurrentLoanNumberLabel";
            this.PH_CurrentLoanNumberLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_CurrentLoanNumberLabel.TabIndex = 157;
            this.PH_CurrentLoanNumberLabel.Text = "Current Loan Number:";
            this.PH_CurrentLoanNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LoanBasicInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.PH_CurrentLoanNumberText);
            this.Controls.Add(this.PH_CurrentLoanNumberLabel);
            this.Controls.Add(this.PH_PreviousLoanNumberText);
            this.Controls.Add(this.PH_PreviousLoanNumberLabel);
            this.Controls.Add(this.PH_OriginalLoanNumberText);
            this.Controls.Add(this.PH_OriginalLoanNumberLabel);
            this.Controls.Add(this.PH_LoanAmountText);
            this.Controls.Add(this.PH_LoanAmountLabel);
            this.Controls.Add(this.PH_DueDTText);
            this.Controls.Add(this.PH_DueDTLabel);
            this.Controls.Add(this.PH_OriginationDTText);
            this.Controls.Add(this.PH_OriginationDTLabel);
            this.Name = "LoanBasicInfoPanel";
            this.Size = new System.Drawing.Size(301, 227);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PH_LoanAmountText;
        private System.Windows.Forms.Label PH_LoanAmountLabel;
        private System.Windows.Forms.Label PH_DueDTText;
        private System.Windows.Forms.Label PH_DueDTLabel;
        private System.Windows.Forms.Label PH_OriginationDTText;
        private System.Windows.Forms.Label PH_OriginationDTLabel;
        private System.Windows.Forms.Label PH_OriginalLoanNumberText;
        private System.Windows.Forms.Label PH_OriginalLoanNumberLabel;
        private System.Windows.Forms.Label PH_PreviousLoanNumberText;
        private System.Windows.Forms.Label PH_PreviousLoanNumberLabel;
        private System.Windows.Forms.Label PH_CurrentLoanNumberText;
        private System.Windows.Forms.Label PH_CurrentLoanNumberLabel;

    }
}
