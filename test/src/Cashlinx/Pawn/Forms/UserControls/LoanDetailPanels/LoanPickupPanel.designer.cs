using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    partial class LoanPickupPanel
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
            this.PH_PickupByPledgorLabel = new System.Windows.Forms.Label();
            this.PH_PickupByPledgorText = new System.Windows.Forms.Label();
            this.PH_ExtAmtPaidToDTLabel = new System.Windows.Forms.Label();
            this.PH_ExtAmtPaidToDTText = new System.Windows.Forms.Label();
            this.PH_RenewDTLabel = new System.Windows.Forms.Label();
            this.PH_RenewDTText = new System.Windows.Forms.Label();
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
            this.PH_CurrentLoanStatusText.ForeColor = System.Drawing.Color.Red;
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
            // PH_PickupByPledgorLabel
            // 
            this.PH_PickupByPledgorLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_PickupByPledgorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_PickupByPledgorLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_PickupByPledgorLabel.Location = new System.Drawing.Point(2, 90);
            this.PH_PickupByPledgorLabel.Name = "PH_PickupByPledgorLabel";
            this.PH_PickupByPledgorLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_PickupByPledgorLabel.TabIndex = 155;
            this.PH_PickupByPledgorLabel.Text = "Pick By Pledgor?:";
            this.PH_PickupByPledgorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_PickupByPledgorText
            // 
            this.PH_PickupByPledgorText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PickupByPledgorText.ForeColor = System.Drawing.Color.Black;
            this.PH_PickupByPledgorText.Location = new System.Drawing.Point(169, 90);
            this.PH_PickupByPledgorText.Name = "PH_PickupByPledgorText";
            this.PH_PickupByPledgorText.Size = new System.Drawing.Size(117, 30);
            this.PH_PickupByPledgorText.TabIndex = 156;
            this.PH_PickupByPledgorText.Text = "Yes";
            this.PH_PickupByPledgorText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ExtAmtPaidToDTLabel
            // 
            this.PH_ExtAmtPaidToDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtAmtPaidToDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ExtAmtPaidToDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtAmtPaidToDTLabel.Location = new System.Drawing.Point(0, 150);
            this.PH_ExtAmtPaidToDTLabel.Name = "PH_ExtAmtPaidToDTLabel";
            this.PH_ExtAmtPaidToDTLabel.Size = new System.Drawing.Size(163, 30);
            this.PH_ExtAmtPaidToDTLabel.TabIndex = 157;
            this.PH_ExtAmtPaidToDTLabel.Text = "Firearm Background Ref ID:";
            this.PH_ExtAmtPaidToDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ExtAmtPaidToDTText
            // 
            this.PH_ExtAmtPaidToDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_ExtAmtPaidToDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_ExtAmtPaidToDTText.Location = new System.Drawing.Point(169, 150);
            this.PH_ExtAmtPaidToDTText.Name = "PH_ExtAmtPaidToDTText";
            this.PH_ExtAmtPaidToDTText.Size = new System.Drawing.Size(117, 30);
            this.PH_ExtAmtPaidToDTText.TabIndex = 158;
            this.PH_ExtAmtPaidToDTText.Text = "xxx";
            this.PH_ExtAmtPaidToDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_RenewDTLabel
            // 
            this.PH_RenewDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_RenewDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_RenewDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_RenewDTLabel.Location = new System.Drawing.Point(3, 0);
            this.PH_RenewDTLabel.Name = "PH_RenewDTLabel";
            this.PH_RenewDTLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_RenewDTLabel.TabIndex = 159;
            this.PH_RenewDTLabel.Text = "Pickup Date/Time:";
            this.PH_RenewDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_RenewDTText
            // 
            this.PH_RenewDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_RenewDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_RenewDTText.Location = new System.Drawing.Point(170, 0);
            this.PH_RenewDTText.Name = "PH_RenewDTText";
            this.PH_RenewDTText.Size = new System.Drawing.Size(134, 30);
            this.PH_RenewDTText.TabIndex = 160;
            this.PH_RenewDTText.Text = "12/21/2012 12:00 AM";
            this.PH_RenewDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoanPickupPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.PH_RenewDTLabel);
            this.Controls.Add(this.PH_RenewDTText);
            this.Controls.Add(this.PH_ExtAmtPaidToDTLabel);
            this.Controls.Add(this.PH_ExtAmtPaidToDTText);
            this.Controls.Add(this.PH_PickupByPledgorLabel);
            this.Controls.Add(this.PH_PickupByPledgorText);
            this.Controls.Add(this.PH_MadeByEmployeeLabel);
            this.Controls.Add(this.PH_MadeByEmployeeText);
            this.Controls.Add(this.PH_CurrentLoanStatusLabel);
            this.Controls.Add(this.PH_CurrentLoanStatusText);
            this.Controls.Add(this.PH_TerminalIDShopLabel);
            this.Controls.Add(this.PH_TerminalIDShopText);
            this.Name = "LoanPickupPanel";
            this.Size = new System.Drawing.Size(307, 203);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PH_MadeByEmployeeLabel;
        private System.Windows.Forms.Label PH_MadeByEmployeeText;
        private System.Windows.Forms.Label PH_CurrentLoanStatusLabel;
        private System.Windows.Forms.Label PH_CurrentLoanStatusText;
        private System.Windows.Forms.Label PH_TerminalIDShopLabel;
        private System.Windows.Forms.Label PH_TerminalIDShopText;
        private System.Windows.Forms.Label PH_PickupByPledgorLabel;
        private System.Windows.Forms.Label PH_PickupByPledgorText;
        private System.Windows.Forms.Label PH_RenewDTText;
        private System.Windows.Forms.Label PH_RenewDTLabel;
        private System.Windows.Forms.Label PH_ExtAmtPaidToDTLabel;
        private System.Windows.Forms.Label PH_ExtAmtPaidToDTText;
    }
}
