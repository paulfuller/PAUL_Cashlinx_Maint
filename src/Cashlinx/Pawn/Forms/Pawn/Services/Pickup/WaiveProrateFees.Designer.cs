using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Pickup
{
    partial class WaiveProrateFees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaiveProrateFees));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelLoanNumber = new System.Windows.Forms.Label();
            this.customLabelLoanAmtHeading = new CustomLabel();
            this.customLabelInterest = new CustomLabel();
            this.customLabelServiceChargeHeading = new CustomLabel();
            this.tableLayoutPanelFeeAmount = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelLoanAmtValue = new CustomLabel();
            this.customLabelInterestValue = new CustomLabel();
            this.customLabelSrvChargeValue = new CustomLabel();
            this.tableLayoutPanelAddlFeeHeading = new System.Windows.Forms.TableLayoutPanel();
            this.customLabel3 = new CustomLabel();
            this.customLabelAddlFeesHeading = new CustomLabel();
            this.customLabelTotalPickupAmtHeading = new CustomLabel();
            this.customLabelTotalPickupAmt = new CustomLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customButtonCancel = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.panelAdditionalFeesHeading = new System.Windows.Forms.Panel();
            this.tableLayoutPanelAddlFees = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelPageNo = new CustomLabel();
            this.tableLayoutPanelFeeAmount.SuspendLayout();
            this.tableLayoutPanelAddlFeeHeading.SuspendLayout();
            this.panelAdditionalFeesHeading.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(13, 22);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(224, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Pickup Amount for Pawn Loan";
            // 
            // labelLoanNumber
            // 
            this.labelLoanNumber.AutoSize = true;
            this.labelLoanNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanNumber.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoanNumber.ForeColor = System.Drawing.Color.White;
            this.labelLoanNumber.Location = new System.Drawing.Point(243, 22);
            this.labelLoanNumber.Name = "labelLoanNumber";
            this.labelLoanNumber.Size = new System.Drawing.Size(99, 19);
            this.labelLoanNumber.TabIndex = 1;
            this.labelLoanNumber.Text = "loan number";
            // 
            // customLabelLoanAmtHeading
            // 
            this.customLabelLoanAmtHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelLoanAmtHeading.AutoSize = true;
            this.customLabelLoanAmtHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelLoanAmtHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelLoanAmtHeading.Location = new System.Drawing.Point(48, 0);
            this.customLabelLoanAmtHeading.Name = "customLabelLoanAmtHeading";
            this.customLabelLoanAmtHeading.Size = new System.Drawing.Size(74, 13);
            this.customLabelLoanAmtHeading.TabIndex = 2;
            this.customLabelLoanAmtHeading.Text = "Loan Amount:";
            this.customLabelLoanAmtHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelInterest
            // 
            this.customLabelInterest.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelInterest.AutoSize = true;
            this.customLabelInterest.BackColor = System.Drawing.Color.Transparent;
            this.customLabelInterest.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelInterest.Location = new System.Drawing.Point(72, 13);
            this.customLabelInterest.Name = "customLabelInterest";
            this.customLabelInterest.Size = new System.Drawing.Size(50, 13);
            this.customLabelInterest.TabIndex = 3;
            this.customLabelInterest.Text = "Interest:";
            // 
            // customLabelServiceChargeHeading
            // 
            this.customLabelServiceChargeHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelServiceChargeHeading.AutoSize = true;
            this.customLabelServiceChargeHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelServiceChargeHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelServiceChargeHeading.Location = new System.Drawing.Point(38, 26);
            this.customLabelServiceChargeHeading.Name = "customLabelServiceChargeHeading";
            this.customLabelServiceChargeHeading.Size = new System.Drawing.Size(84, 13);
            this.customLabelServiceChargeHeading.TabIndex = 4;
            this.customLabelServiceChargeHeading.Text = "Service Charge:";
            this.customLabelServiceChargeHeading.Visible = false;
            // 
            // tableLayoutPanelFeeAmount
            // 
            this.tableLayoutPanelFeeAmount.AutoSize = true;
            this.tableLayoutPanelFeeAmount.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelFeeAmount.ColumnCount = 2;
            this.tableLayoutPanelFeeAmount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFeeAmount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFeeAmount.Controls.Add(this.customLabelLoanAmtHeading, 0, 0);
            this.tableLayoutPanelFeeAmount.Controls.Add(this.customLabelServiceChargeHeading, 0, 2);
            this.tableLayoutPanelFeeAmount.Controls.Add(this.customLabelInterest, 0, 1);
            this.tableLayoutPanelFeeAmount.Controls.Add(this.customLabelLoanAmtValue, 1, 0);
            this.tableLayoutPanelFeeAmount.Controls.Add(this.customLabelInterestValue, 1, 1);
            this.tableLayoutPanelFeeAmount.Controls.Add(this.customLabelSrvChargeValue, 1, 2);
            this.tableLayoutPanelFeeAmount.Location = new System.Drawing.Point(126, 83);
            this.tableLayoutPanelFeeAmount.Name = "tableLayoutPanelFeeAmount";
            this.tableLayoutPanelFeeAmount.RowCount = 5;
            this.tableLayoutPanelFeeAmount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFeeAmount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFeeAmount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFeeAmount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFeeAmount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFeeAmount.Size = new System.Drawing.Size(251, 62);
            this.tableLayoutPanelFeeAmount.TabIndex = 5;
            // 
            // customLabelLoanAmtValue
            // 
            this.customLabelLoanAmtValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelLoanAmtValue.AutoSize = true;
            this.customLabelLoanAmtValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelLoanAmtValue.Location = new System.Drawing.Point(128, 0);
            this.customLabelLoanAmtValue.Name = "customLabelLoanAmtValue";
            this.customLabelLoanAmtValue.Size = new System.Drawing.Size(13, 13);
            this.customLabelLoanAmtValue.TabIndex = 5;
            this.customLabelLoanAmtValue.Text = "$";
            // 
            // customLabelInterestValue
            // 
            this.customLabelInterestValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelInterestValue.AutoSize = true;
            this.customLabelInterestValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelInterestValue.Location = new System.Drawing.Point(128, 13);
            this.customLabelInterestValue.Name = "customLabelInterestValue";
            this.customLabelInterestValue.Size = new System.Drawing.Size(13, 13);
            this.customLabelInterestValue.TabIndex = 6;
            this.customLabelInterestValue.Text = "$";
            // 
            // customLabelSrvChargeValue
            // 
            this.customLabelSrvChargeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSrvChargeValue.AutoSize = true;
            this.customLabelSrvChargeValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelSrvChargeValue.Location = new System.Drawing.Point(128, 26);
            this.customLabelSrvChargeValue.Name = "customLabelSrvChargeValue";
            this.customLabelSrvChargeValue.Size = new System.Drawing.Size(13, 13);
            this.customLabelSrvChargeValue.TabIndex = 7;
            this.customLabelSrvChargeValue.Text = "$";
            this.customLabelSrvChargeValue.Visible = false;
            // 
            // tableLayoutPanelAddlFeeHeading
            // 
            this.tableLayoutPanelAddlFeeHeading.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAddlFeeHeading.ColumnCount = 3;
            this.tableLayoutPanelAddlFeeHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 234F));
            this.tableLayoutPanelAddlFeeHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanelAddlFeeHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tableLayoutPanelAddlFeeHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelAddlFeeHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelAddlFeeHeading.Controls.Add(this.customLabel3, 2, 0);
            this.tableLayoutPanelAddlFeeHeading.Controls.Add(this.customLabelAddlFeesHeading, 0, 0);
            this.tableLayoutPanelAddlFeeHeading.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelAddlFeeHeading.Name = "tableLayoutPanelAddlFeeHeading";
            this.tableLayoutPanelAddlFeeHeading.RowCount = 1;
            this.tableLayoutPanelAddlFeeHeading.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAddlFeeHeading.Size = new System.Drawing.Size(477, 22);
            this.tableLayoutPanelAddlFeeHeading.TabIndex = 6;
            this.tableLayoutPanelAddlFeeHeading.Visible = false;
            // 
            // customLabel3
            // 
            this.customLabel3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabel3.AutoSize = true;
            this.customLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel3.Location = new System.Drawing.Point(289, 4);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(37, 13);
            this.customLabel3.TabIndex = 3;
            this.customLabel3.Text = "Waive";
            // 
            // customLabelAddlFeesHeading
            // 
            this.customLabelAddlFeesHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelAddlFeesHeading.AutoSize = true;
            this.customLabelAddlFeesHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelAddlFeesHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAddlFeesHeading.Location = new System.Drawing.Point(151, 4);
            this.customLabelAddlFeesHeading.Name = "customLabelAddlFeesHeading";
            this.customLabelAddlFeesHeading.Size = new System.Drawing.Size(80, 13);
            this.customLabelAddlFeesHeading.TabIndex = 0;
            this.customLabelAddlFeesHeading.Text = "Additional Fees";
            this.customLabelAddlFeesHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customLabelTotalPickupAmtHeading
            // 
            this.customLabelTotalPickupAmtHeading.AutoSize = true;
            this.customLabelTotalPickupAmtHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTotalPickupAmtHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTotalPickupAmtHeading.Location = new System.Drawing.Point(140, 329);
            this.customLabelTotalPickupAmtHeading.Name = "customLabelTotalPickupAmtHeading";
            this.customLabelTotalPickupAmtHeading.Size = new System.Drawing.Size(108, 13);
            this.customLabelTotalPickupAmtHeading.TabIndex = 7;
            this.customLabelTotalPickupAmtHeading.Text = "Total Pickup Amount:";
            // 
            // customLabelTotalPickupAmt
            // 
            this.customLabelTotalPickupAmt.AutoSize = true;
            this.customLabelTotalPickupAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTotalPickupAmt.Location = new System.Drawing.Point(255, 329);
            this.customLabelTotalPickupAmt.Name = "customLabelTotalPickupAmt";
            this.customLabelTotalPickupAmt.Size = new System.Drawing.Size(13, 13);
            this.customLabelTotalPickupAmt.TabIndex = 8;
            this.customLabelTotalPickupAmt.Text = "$";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(1, 358);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 2);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
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
            this.customButtonCancel.Location = new System.Drawing.Point(12, 363);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 10;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(400, 363);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 11;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // panelAdditionalFeesHeading
            // 
            this.panelAdditionalFeesHeading.BackColor = System.Drawing.Color.LightGray;
            this.panelAdditionalFeesHeading.Controls.Add(this.tableLayoutPanelAddlFeeHeading);
            this.panelAdditionalFeesHeading.Location = new System.Drawing.Point(12, 212);
            this.panelAdditionalFeesHeading.Name = "panelAdditionalFeesHeading";
            this.panelAdditionalFeesHeading.Size = new System.Drawing.Size(477, 22);
            this.panelAdditionalFeesHeading.TabIndex = 12;
            // 
            // tableLayoutPanelAddlFees
            // 
            this.tableLayoutPanelAddlFees.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanelAddlFees.AutoSize = true;
            this.tableLayoutPanelAddlFees.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAddlFees.ColumnCount = 3;
            this.tableLayoutPanelAddlFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 234F));
            this.tableLayoutPanelAddlFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanelAddlFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 226F));
            this.tableLayoutPanelAddlFees.Location = new System.Drawing.Point(12, 232);
            this.tableLayoutPanelAddlFees.Name = "tableLayoutPanelAddlFees";
            this.tableLayoutPanelAddlFees.RowCount = 1;
            this.tableLayoutPanelAddlFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAddlFees.Size = new System.Drawing.Size(512, 25);
            this.tableLayoutPanelAddlFees.TabIndex = 13;
            this.tableLayoutPanelAddlFees.Visible = false;
            // 
            // customLabelPageNo
            // 
            this.customLabelPageNo.AutoSize = true;
            this.customLabelPageNo.BackColor = System.Drawing.Color.Transparent;
            this.customLabelPageNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPageNo.ForeColor = System.Drawing.Color.White;
            this.customLabelPageNo.Location = new System.Drawing.Point(415, 27);
            this.customLabelPageNo.Name = "customLabelPageNo";
            this.customLabelPageNo.Size = new System.Drawing.Size(35, 13);
            this.customLabelPageNo.TabIndex = 14;
            this.customLabelPageNo.Text = "1 of 1";
            // 
            // WaiveProrateFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.ClientSize = new System.Drawing.Size(536, 428);
            this.ControlBox = false;
            this.Controls.Add(this.customLabelPageNo);
            this.Controls.Add(this.tableLayoutPanelAddlFees);
            this.Controls.Add(this.panelAdditionalFeesHeading);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.customLabelTotalPickupAmt);
            this.Controls.Add(this.customLabelTotalPickupAmtHeading);
            this.Controls.Add(this.tableLayoutPanelFeeAmount);
            this.Controls.Add(this.labelLoanNumber);
            this.Controls.Add(this.labelHeading);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaiveProrateFees";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "WaiveProrateFees";
            this.Load += new System.EventHandler(this.WaiveProrateFees_Load);
            this.tableLayoutPanelFeeAmount.ResumeLayout(false);
            this.tableLayoutPanelFeeAmount.PerformLayout();
            this.tableLayoutPanelAddlFeeHeading.ResumeLayout(false);
            this.tableLayoutPanelAddlFeeHeading.PerformLayout();
            this.panelAdditionalFeesHeading.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelLoanNumber;
        private CustomLabel customLabelLoanAmtHeading;
        private CustomLabel customLabelInterest;
        private CustomLabel customLabelServiceChargeHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFeeAmount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAddlFeeHeading;
        private CustomLabel customLabelAddlFeesHeading;
        private CustomLabel customLabel3;
        private CustomLabel customLabelTotalPickupAmtHeading;
        private CustomLabel customLabelTotalPickupAmt;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private CustomLabel customLabelLoanAmtValue;
        private CustomLabel customLabelInterestValue;
        private CustomLabel customLabelSrvChargeValue;
        private System.Windows.Forms.Panel panelAdditionalFeesHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAddlFees;
        private CustomLabel customLabelPageNo;

    }
}