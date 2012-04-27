using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls
{
    partial class LoanStatusInfoPanel
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
            this.PH_RenewByPledgorLabel = new System.Windows.Forms.Label();
            this.PH_RenewByPledgorText = new System.Windows.Forms.Label();
            this.PH_CurrentLoanStatusLabel = new System.Windows.Forms.Label();
            this.PH_CurrentLoanStatusText = new System.Windows.Forms.Label();
            this.PH_TerminalIDShopLabel = new System.Windows.Forms.Label();
            this.PH_TerminalIDShopText = new System.Windows.Forms.Label();
            this.PH_PickupDTLabel = new System.Windows.Forms.Label();
            this.PH_PickupDTText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.PH_MadeByEmployeeLabel.Text = "Made By Emp";
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
            // PH_RenewByPledgorLabel
            // 
            this.PH_RenewByPledgorLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_RenewByPledgorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_RenewByPledgorLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_RenewByPledgorLabel.Location = new System.Drawing.Point(2, 90);
            this.PH_RenewByPledgorLabel.Name = "PH_RenewByPledgorLabel";
            this.PH_RenewByPledgorLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_RenewByPledgorLabel.TabIndex = 151;
            this.PH_RenewByPledgorLabel.Text = "Renew By Pledgor";
            this.PH_RenewByPledgorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_RenewByPledgorText
            // 
            this.PH_RenewByPledgorText.BackColor = System.Drawing.Color.Transparent;
            this.PH_RenewByPledgorText.ForeColor = System.Drawing.Color.Black;
            this.PH_RenewByPledgorText.Location = new System.Drawing.Point(169, 90);
            this.PH_RenewByPledgorText.Name = "PH_RenewByPledgorText";
            this.PH_RenewByPledgorText.Size = new System.Drawing.Size(117, 30);
            this.PH_RenewByPledgorText.TabIndex = 152;
            this.PH_RenewByPledgorText.Text = "No";
            this.PH_RenewByPledgorText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.PH_CurrentLoanStatusLabel.Text = "Current Loan Status";
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
            this.PH_TerminalIDShopLabel.Text = "Terminal ID/Shop";
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
            // PH_PickupDTLabel
            // 
            this.PH_PickupDTLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_PickupDTLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_PickupDTLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_PickupDTLabel.Location = new System.Drawing.Point(2, 0);
            this.PH_PickupDTLabel.Name = "PH_PickupDTLabel";
            this.PH_PickupDTLabel.Size = new System.Drawing.Size(161, 30);
            this.PH_PickupDTLabel.TabIndex = 145;
            this.PH_PickupDTLabel.Text = "Pickup Date/Time";
            this.PH_PickupDTLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_PickupDTText
            // 
            this.PH_PickupDTText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PickupDTText.ForeColor = System.Drawing.Color.Black;
            this.PH_PickupDTText.Location = new System.Drawing.Point(169, 0);
            this.PH_PickupDTText.Name = "PH_PickupDTText";
            this.PH_PickupDTText.Size = new System.Drawing.Size(117, 30);
            this.PH_PickupDTText.TabIndex = 146;
            this.PH_PickupDTText.Text = "12/31/2000 12:00 AM";
            this.PH_PickupDTText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 30);
            this.label1.TabIndex = 155;
            this.label1.Text = "Made By Emp";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(169, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 30);
            this.label2.TabIndex = 156;
            this.label2.Text = "xxx";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoanStatusInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PH_MadeByEmployeeLabel);
            this.Controls.Add(this.PH_MadeByEmployeeText);
            this.Controls.Add(this.PH_RenewByPledgorLabel);
            this.Controls.Add(this.PH_RenewByPledgorText);
            this.Controls.Add(this.PH_CurrentLoanStatusLabel);
            this.Controls.Add(this.PH_CurrentLoanStatusText);
            this.Controls.Add(this.PH_TerminalIDShopLabel);
            this.Controls.Add(this.PH_TerminalIDShopText);
            this.Controls.Add(this.PH_PickupDTLabel);
            this.Controls.Add(this.PH_PickupDTText);
            this.Name = "LoanStatusInfoPanel";
            this.Size = new System.Drawing.Size(289, 191);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PH_MadeByEmployeeLabel;
        private System.Windows.Forms.Label PH_MadeByEmployeeText;
        private System.Windows.Forms.Label PH_RenewByPledgorLabel;
        private System.Windows.Forms.Label PH_RenewByPledgorText;
        private System.Windows.Forms.Label PH_CurrentLoanStatusLabel;
        private System.Windows.Forms.Label PH_CurrentLoanStatusText;
        private System.Windows.Forms.Label PH_TerminalIDShopLabel;
        private System.Windows.Forms.Label PH_TerminalIDShopText;
        private System.Windows.Forms.Label PH_PickupDTLabel;
        private System.Windows.Forms.Label PH_PickupDTText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;


    }
}
