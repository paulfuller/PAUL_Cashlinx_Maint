using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Loan.ProcessTender
{
    partial class DisbursementDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisbursementDetails));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelPayCustomerHeading = new System.Windows.Forms.Label();
            this.labelPayCustomerAmount = new System.Windows.Forms.Label();
            this.labelTenderSelectedHeading = new System.Windows.Forms.Label();
            this.labelTenderSelected = new System.Windows.Forms.Label();
            this.panelTender = new System.Windows.Forms.Panel();
            this.customButtonACPayable = new CustomButton();
            this.customButtonCash = new CustomButton();
            this.labelTenderHeading = new System.Windows.Forms.Label();
            this.customButtonCancel = new CustomButton();
            this.customButtonContinue = new CustomButton();
            this.panelTender.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(199, 22);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(158, 19);
            this.labelHeading.TabIndex = 1;
            this.labelHeading.Text = "Disbursement Details";
            // 
            // labelPayCustomerHeading
            // 
            this.labelPayCustomerHeading.AutoSize = true;
            this.labelPayCustomerHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelPayCustomerHeading.Location = new System.Drawing.Point(52, 95);
            this.labelPayCustomerHeading.Name = "labelPayCustomerHeading";
            this.labelPayCustomerHeading.Size = new System.Drawing.Size(91, 13);
            this.labelPayCustomerHeading.TabIndex = 2;
            this.labelPayCustomerHeading.Text = "Pay to Customer:";
            // 
            // labelPayCustomerAmount
            // 
            this.labelPayCustomerAmount.AutoSize = true;
            this.labelPayCustomerAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelPayCustomerAmount.Location = new System.Drawing.Point(159, 95);
            this.labelPayCustomerAmount.Name = "labelPayCustomerAmount";
            this.labelPayCustomerAmount.Size = new System.Drawing.Size(35, 13);
            this.labelPayCustomerAmount.TabIndex = 3;
            this.labelPayCustomerAmount.Text = "$1.00";
            // 
            // labelTenderSelectedHeading
            // 
            this.labelTenderSelectedHeading.AutoSize = true;
            this.labelTenderSelectedHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelTenderSelectedHeading.Location = new System.Drawing.Point(52, 124);
            this.labelTenderSelectedHeading.Name = "labelTenderSelectedHeading";
            this.labelTenderSelectedHeading.Size = new System.Drawing.Size(89, 13);
            this.labelTenderSelectedHeading.TabIndex = 4;
            this.labelTenderSelectedHeading.Text = "Tender Selected:";
            // 
            // labelTenderSelected
            // 
            this.labelTenderSelected.AutoSize = true;
            this.labelTenderSelected.BackColor = System.Drawing.Color.Transparent;
            this.labelTenderSelected.Location = new System.Drawing.Point(159, 124);
            this.labelTenderSelected.Name = "labelTenderSelected";
            this.labelTenderSelected.Size = new System.Drawing.Size(92, 13);
            this.labelTenderSelected.TabIndex = 5;
            this.labelTenderSelected.Text = "Accounts Payable";
            // 
            // panelTender
            // 
            this.panelTender.BackColor = System.Drawing.Color.Transparent;
            this.panelTender.Controls.Add(this.customButtonACPayable);
            this.panelTender.Controls.Add(this.customButtonCash);
            this.panelTender.Controls.Add(this.labelTenderHeading);
            this.panelTender.Location = new System.Drawing.Point(379, 85);
            this.panelTender.Name = "panelTender";
            this.panelTender.Size = new System.Drawing.Size(208, 195);
            this.panelTender.TabIndex = 6;
            // 
            // customButtonACPayable
            // 
            this.customButtonACPayable.BackColor = System.Drawing.Color.Transparent;
            this.customButtonACPayable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonACPayable.BackgroundImage")));
            this.customButtonACPayable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonACPayable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonACPayable.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonACPayable.FlatAppearance.BorderSize = 0;
            this.customButtonACPayable.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonACPayable.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonACPayable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonACPayable.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonACPayable.ForeColor = System.Drawing.Color.White;
            this.customButtonACPayable.Location = new System.Drawing.Point(42, 120);
            this.customButtonACPayable.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonACPayable.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonACPayable.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonACPayable.Name = "customButtonACPayable";
            this.customButtonACPayable.Size = new System.Drawing.Size(100, 50);
            this.customButtonACPayable.TabIndex = 4;
            this.customButtonACPayable.Text = "Bill to AP";
            this.customButtonACPayable.UseVisualStyleBackColor = false;
            this.customButtonACPayable.Click += new System.EventHandler(this.customButtonACPayable_Click);
            // 
            // customButtonCash
            // 
            this.customButtonCash.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCash.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCash.BackgroundImage")));
            this.customButtonCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCash.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCash.FlatAppearance.BorderSize = 0;
            this.customButtonCash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCash.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCash.ForeColor = System.Drawing.Color.White;
            this.customButtonCash.Location = new System.Drawing.Point(42, 49);
            this.customButtonCash.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCash.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCash.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCash.Name = "customButtonCash";
            this.customButtonCash.Size = new System.Drawing.Size(100, 50);
            this.customButtonCash.TabIndex = 3;
            this.customButtonCash.Text = "CASH";
            this.customButtonCash.UseVisualStyleBackColor = false;
            this.customButtonCash.Click += new System.EventHandler(this.customButtonCash_Click);
            // 
            // labelTenderHeading
            // 
            this.labelTenderHeading.AutoSize = true;
            this.labelTenderHeading.BackColor = System.Drawing.Color.RoyalBlue;
            this.labelTenderHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTenderHeading.ForeColor = System.Drawing.Color.White;
            this.labelTenderHeading.Location = new System.Drawing.Point(23, 4);
            this.labelTenderHeading.Name = "labelTenderHeading";
            this.labelTenderHeading.Size = new System.Drawing.Size(145, 19);
            this.labelTenderHeading.TabIndex = 2;
            this.labelTenderHeading.Text = "Select Tender Type";
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
            this.customButtonCancel.Location = new System.Drawing.Point(41, 358);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 7;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
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
            this.customButtonContinue.Location = new System.Drawing.Point(442, 358);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 8;
            this.customButtonContinue.Text = "Continue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.customButtonContinue_Click);
            // 
            // DisbursementDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 440);
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.panelTender);
            this.Controls.Add(this.labelTenderSelected);
            this.Controls.Add(this.labelTenderSelectedHeading);
            this.Controls.Add(this.labelPayCustomerAmount);
            this.Controls.Add(this.labelPayCustomerHeading);
            this.Controls.Add(this.labelHeading);
            this.Name = "DisbursementDetails";
            this.Text = "DisbursementDetails";
            this.Load += new System.EventHandler(this.DisbursementDetails_Load);
            this.panelTender.ResumeLayout(false);
            this.panelTender.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelPayCustomerHeading;
        private System.Windows.Forms.Label labelPayCustomerAmount;
        private System.Windows.Forms.Label labelTenderSelectedHeading;
        private System.Windows.Forms.Label labelTenderSelected;
        private System.Windows.Forms.Panel panelTender;
        private CustomButton customButtonACPayable;
        private CustomButton customButtonCash;
        private System.Windows.Forms.Label labelTenderHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonContinue;
    }
}