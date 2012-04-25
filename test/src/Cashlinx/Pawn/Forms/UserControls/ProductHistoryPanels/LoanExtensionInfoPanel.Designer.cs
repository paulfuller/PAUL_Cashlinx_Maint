using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls.ProductHistoryPanels
{
    partial class LoanExtensionInfoPanel
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
            this.PH_MadeByEmployeeLabel = new System.Windows.Forms.Label();
            this.PH_MadeByEmployeeText = new System.Windows.Forms.Label();
            this.PH_CurrentLoanStatusLabel = new System.Windows.Forms.Label();
            this.PH_CurrentLoanStatusText = new System.Windows.Forms.Label();
            this.PH_TerminalIDShopLabel = new System.Windows.Forms.Label();
            this.PH_TerminalIDShopText = new System.Windows.Forms.Label();
            this.PH_ExtensionDTLabel = new System.Windows.Forms.Label();
            this.PH_ExtensionDTText = new System.Windows.Forms.Label();
            this.PH_ExtAmtPaidToDTLabel = new System.Windows.Forms.Label();
            this.PH_ExtAmtPaidToDTText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PH_MadeByEmployeeLabel
            // 
            this.PH_MadeByEmployeeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_MadeByEmployeeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_MadeByEmployeeLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_MadeByEmployeeLabel.Location = new System.Drawing.Point(2, 120);
            this.PH_MadeByEmployeeLabel.Name = "PH_MadeByEmployeeLabel";
            this.PH_MadeByEmployeeLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_MadeByEmployeeLabel.TabIndex = 153;
            this.PH_MadeByEmployeeLabel.Text = "Made By Emp:";
            this.PH_MadeByEmployeeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_MadeByEmployeeText
            // 
            this.PH_MadeByEmployeeText.BackColor = System.Drawing.Color.Transparent;
            this.PH_MadeByEmployeeText.ForeColor = System.Drawing.Color.Black;
            this.PH_MadeByEmployeeText.Location = new System.Drawing.Point(169, 120);
            this.PH_MadeByEmployeeText.Name = "PH_MadeByEmployeeText";
            this.PH_MadeByEmployeeText.Size = new System.Drawing.Size(117, 30);
            this.PH_MadeByEmployeeText.TabIndex = 154;
            this.PH_MadeByEmployeeText.Text = "xxx";
            this.PH_MadeByEmployeeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_CurrentLoanStatusLabel
            // 
            this.PH_CurrentLoanStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_CurrentLoanStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_CurrentLoanStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_CurrentLoanStatusLabel.Location = new System.Drawing.Point(2, 60);
            this.PH_CurrentLoanStatusLabel.Name = "PH_CurrentLoanStatusLabel";
            this.PH_CurrentLoanStatusLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_CurrentLoanStatusLabel.TabIndex = 149;
            this.PH_CurrentLoanStatusLabel.Text = "Current Loan Status:";
            this.PH_CurrentLoanStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_CurrentLoanStatusText
            // 
            this.PH_CurrentLoanStatusText.BackColor = System.Drawing.Color.Transparent;
            this.PH_CurrentLoanStatusText.ForeColor = System.Drawing.Color.Black;
            this.PH_CurrentLoanStatusText.Location = new System.Drawing.Point(169, 60);
            this.PH_CurrentLoanStatusText.Name = "PH_CurrentLoanStatusText";
            this.PH_CurrentLoanStatusText.Size = new System.Drawing.Size(117, 30);
            this.PH_CurrentLoanStatusText.TabIndex = 150;
            this.PH_CurrentLoanStatusText.Text = "PickUp";
            this.PH_CurrentLoanStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_TerminalIDShopLabel
            // 
            this.PH_TerminalIDShopLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_TerminalIDShopLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_TerminalIDShopLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_TerminalIDShopLabel.Location = new System.Drawing.Point(2, 30);
            this.PH_TerminalIDShopLabel.Name = "PH_TerminalIDShopLabel";
            this.PH_TerminalIDShopLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_TerminalIDShopLabel.TabIndex = 147;
            this.PH_TerminalIDShopLabel.Text = "Terminal ID/Shop:";
            this.PH_TerminalIDShopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_TerminalIDShopText
            // 
            this.PH_TerminalIDShopText.BackColor = System.Drawing.Color.Transparent;
            this.PH_TerminalIDShopText.ForeColor = System.Drawing.Color.Black;
            this.PH_TerminalIDShopText.Location = new System.Drawing.Point(169, 30);
            this.PH_TerminalIDShopText.Name = "PH_TerminalIDShopText";
            this.PH_TerminalIDShopText.Size = new System.Drawing.Size(117, 30);
            this.PH_TerminalIDShopText.TabIndex = 148;
            this.PH_TerminalIDShopText.Text = "0";
            this.PH_TerminalIDShopText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ExtensionDTLabel
            // 
            this.PH_ExtensionDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtensionDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ExtensionDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtensionDTLabel.Location = new System.Drawing.Point(2, 0);
            this.PH_ExtensionDTLabel.Name = "PH_ExtensionDTLabel";
            this.PH_ExtensionDTLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_ExtensionDTLabel.TabIndex = 145;
            this.PH_ExtensionDTLabel.Text = "Extension Date/Time:";
            this.PH_ExtensionDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ExtensionDTText
            // 
            this.PH_ExtensionDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtensionDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtensionDTText.Location = new System.Drawing.Point(169, 0);
            this.PH_ExtensionDTText.Name = "PH_ExtensionDTText";
            this.PH_ExtensionDTText.Size = new System.Drawing.Size(135, 30);
            this.PH_ExtensionDTText.TabIndex = 146;
            this.PH_ExtensionDTText.Text = "12/31/2000 12:00 AM";
            this.PH_ExtensionDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ExtAmtPaidToDTLabel
            // 
            this.PH_ExtAmtPaidToDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtAmtPaidToDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ExtAmtPaidToDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtAmtPaidToDTLabel.Location = new System.Drawing.Point(2, 90);
            this.PH_ExtAmtPaidToDTLabel.Name = "PH_ExtAmtPaidToDTLabel";
            this.PH_ExtAmtPaidToDTLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_ExtAmtPaidToDTLabel.TabIndex = 155;
            this.PH_ExtAmtPaidToDTLabel.Text = "Ext. Amount Paid to Date:";
            this.PH_ExtAmtPaidToDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ExtAmtPaidToDTText
            // 
            this.PH_ExtAmtPaidToDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtAmtPaidToDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtAmtPaidToDTText.Location = new System.Drawing.Point(169, 90);
            this.PH_ExtAmtPaidToDTText.Name = "PH_ExtAmtPaidToDTText";
            this.PH_ExtAmtPaidToDTText.Size = new System.Drawing.Size(117, 30);
            this.PH_ExtAmtPaidToDTText.TabIndex = 156;
            this.PH_ExtAmtPaidToDTText.Text = "$12.00";
            this.PH_ExtAmtPaidToDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoanExtensionInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.PH_ExtAmtPaidToDTLabel);
            this.Controls.Add(this.PH_ExtAmtPaidToDTText);
            this.Controls.Add(this.PH_MadeByEmployeeLabel);
            this.Controls.Add(this.PH_MadeByEmployeeText);
            this.Controls.Add(this.PH_CurrentLoanStatusLabel);
            this.Controls.Add(this.PH_CurrentLoanStatusText);
            this.Controls.Add(this.PH_TerminalIDShopLabel);
            this.Controls.Add(this.PH_TerminalIDShopText);
            this.Controls.Add(this.PH_ExtensionDTLabel);
            this.Controls.Add(this.PH_ExtensionDTText);
            this.Name = "LoanExtensionInfoPanel";
            this.Size = new System.Drawing.Size(307, 162);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PH_MadeByEmployeeLabel;
        private System.Windows.Forms.Label PH_MadeByEmployeeText;
        private System.Windows.Forms.Label PH_CurrentLoanStatusLabel;
        private System.Windows.Forms.Label PH_CurrentLoanStatusText;
        private System.Windows.Forms.Label PH_TerminalIDShopLabel;
        private System.Windows.Forms.Label PH_TerminalIDShopText;
        private System.Windows.Forms.Label PH_ExtensionDTLabel;
        private System.Windows.Forms.Label PH_ExtensionDTText;
        private System.Windows.Forms.Label PH_ExtAmtPaidToDTLabel;
        private System.Windows.Forms.Label PH_ExtAmtPaidToDTText;


    }
}
