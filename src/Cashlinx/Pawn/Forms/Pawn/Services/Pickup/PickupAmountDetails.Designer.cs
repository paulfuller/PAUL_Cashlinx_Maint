using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Pickup
{
    partial class PickupAmountDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PickupAmountDetails));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelLoanNumber = new System.Windows.Forms.Label();
            this.tableLayoutPanelPickupAmount = new System.Windows.Forms.TableLayoutPanel();
            this.labelLoanAmtHeading = new System.Windows.Forms.Label();
            this.labelLoanAmount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customButtonCancel = new Common.Libraries.Forms.Components.CustomButton();
            this.tableLayoutPanelFees = new System.Windows.Forms.TableLayoutPanel();
            this.labelCurrentPrincipalHeading = new System.Windows.Forms.Label();
            this.labelCurrentPrincipal = new System.Windows.Forms.Label();
            this.tableLayoutPanelPickupAmount.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(55, 23);
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
            this.labelLoanNumber.Location = new System.Drawing.Point(285, 23);
            this.labelLoanNumber.Name = "labelLoanNumber";
            this.labelLoanNumber.Size = new System.Drawing.Size(105, 19);
            this.labelLoanNumber.TabIndex = 1;
            this.labelLoanNumber.Text = "Loan Number";
            // 
            // tableLayoutPanelPickupAmount
            // 
            this.tableLayoutPanelPickupAmount.AutoSize = true;
            this.tableLayoutPanelPickupAmount.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelPickupAmount.ColumnCount = 2;
            this.tableLayoutPanelPickupAmount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayoutPanelPickupAmount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tableLayoutPanelPickupAmount.Controls.Add(this.labelCurrentPrincipal, 1, 1);
            this.tableLayoutPanelPickupAmount.Controls.Add(this.labelCurrentPrincipalHeading, 0, 1);
            this.tableLayoutPanelPickupAmount.Controls.Add(this.labelLoanAmtHeading, 0, 0);
            this.tableLayoutPanelPickupAmount.Controls.Add(this.labelLoanAmount, 1, 0);
            this.tableLayoutPanelPickupAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanelPickupAmount.Location = new System.Drawing.Point(30, 70);
            this.tableLayoutPanelPickupAmount.Name = "tableLayoutPanelPickupAmount";
            this.tableLayoutPanelPickupAmount.RowCount = 2;
            this.tableLayoutPanelPickupAmount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPickupAmount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPickupAmount.Size = new System.Drawing.Size(363, 45);
            this.tableLayoutPanelPickupAmount.TabIndex = 2;
            // 
            // labelLoanAmtHeading
            // 
            this.labelLoanAmtHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelLoanAmtHeading.AutoSize = true;
            this.labelLoanAmtHeading.Location = new System.Drawing.Point(81, 4);
            this.labelLoanAmtHeading.Name = "labelLoanAmtHeading";
            this.labelLoanAmtHeading.Size = new System.Drawing.Size(74, 13);
            this.labelLoanAmtHeading.TabIndex = 0;
            this.labelLoanAmtHeading.Text = "Loan Amount:";
            // 
            // labelLoanAmount
            // 
            this.labelLoanAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLoanAmount.AutoSize = true;
            this.labelLoanAmount.Location = new System.Drawing.Point(161, 4);
            this.labelLoanAmount.Name = "labelLoanAmount";
            this.labelLoanAmount.Size = new System.Drawing.Size(13, 13);
            this.labelLoanAmount.TabIndex = 7;
            this.labelLoanAmount.Text = "$";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(6, 225);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 2);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
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
            this.customButtonCancel.Location = new System.Drawing.Point(30, 243);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 8;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tableLayoutPanelFees
            // 
            this.tableLayoutPanelFees.AutoSize = true;
            this.tableLayoutPanelFees.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelFees.ColumnCount = 2;
            this.tableLayoutPanelFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.52617F));
            this.tableLayoutPanelFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.47383F));
            this.tableLayoutPanelFees.Location = new System.Drawing.Point(30, 116);
            this.tableLayoutPanelFees.Name = "tableLayoutPanelFees";
            this.tableLayoutPanelFees.RowCount = 1;
            this.tableLayoutPanelFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFees.Size = new System.Drawing.Size(363, 20);
            this.tableLayoutPanelFees.TabIndex = 9;
            // 
            // labelCurrentPrincipalHeading
            // 
            this.labelCurrentPrincipalHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCurrentPrincipalHeading.AutoSize = true;
            this.labelCurrentPrincipalHeading.Location = new System.Drawing.Point(65, 27);
            this.labelCurrentPrincipalHeading.Name = "labelCurrentPrincipalHeading";
            this.labelCurrentPrincipalHeading.Size = new System.Drawing.Size(90, 13);
            this.labelCurrentPrincipalHeading.TabIndex = 8;
            this.labelCurrentPrincipalHeading.Text = "Current Principal:";
            // 
            // labelCurrentPrincipal
            // 
            this.labelCurrentPrincipal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCurrentPrincipal.AutoSize = true;
            this.labelCurrentPrincipal.Location = new System.Drawing.Point(161, 27);
            this.labelCurrentPrincipal.Name = "labelCurrentPrincipal";
            this.labelCurrentPrincipal.Size = new System.Drawing.Size(13, 13);
            this.labelCurrentPrincipal.TabIndex = 10;
            this.labelCurrentPrincipal.Text = "$";
            // 
            // PickupAmountDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(503, 311);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanelFees);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanelPickupAmount);
            this.Controls.Add(this.labelLoanNumber);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PickupAmountDetails";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProrateWaiveLateFees";
            this.tableLayoutPanelPickupAmount.ResumeLayout(false);
            this.tableLayoutPanelPickupAmount.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelLoanNumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPickupAmount;
        private System.Windows.Forms.Label labelLoanAmtHeading;
        private System.Windows.Forms.Label labelLoanAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomButton customButtonCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFees;
        private System.Windows.Forms.Label labelCurrentPrincipal;
        private System.Windows.Forms.Label labelCurrentPrincipalHeading;
    }
}