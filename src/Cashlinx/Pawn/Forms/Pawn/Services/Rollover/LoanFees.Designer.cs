namespace Pawn.Forms.Pawn.Services.Rollover
{
    partial class LoanFees
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
            this.rolloverLoanHeaderLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.feesTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // rolloverLoanHeaderLabel
            // 
            this.rolloverLoanHeaderLabel.AutoSize = true;
            this.rolloverLoanHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.rolloverLoanHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rolloverLoanHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.rolloverLoanHeaderLabel.Location = new System.Drawing.Point(93, 22);
            this.rolloverLoanHeaderLabel.Name = "rolloverLoanHeaderLabel";
            this.rolloverLoanHeaderLabel.Size = new System.Drawing.Size(182, 19);
            this.rolloverLoanHeaderLabel.TabIndex = 132;
            this.rolloverLoanHeaderLabel.Text = "Fees For Pawn Loan XXX";
            this.rolloverLoanHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.closeButton.AutoSize = true;
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(244, 572);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.closeButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 50);
            this.closeButton.TabIndex = 144;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // feesTablePanel
            // 
            this.feesTablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.feesTablePanel.AutoScroll = true;
            this.feesTablePanel.AutoSize = true;
            this.feesTablePanel.BackColor = System.Drawing.Color.White;
            this.feesTablePanel.ColumnCount = 3;
            this.feesTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.feesTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.feesTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.feesTablePanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feesTablePanel.ForeColor = System.Drawing.Color.Black;
            this.feesTablePanel.Location = new System.Drawing.Point(23, 68);
            this.feesTablePanel.Name = "feesTablePanel";
            this.feesTablePanel.RowCount = 2;
            this.feesTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.feesTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.feesTablePanel.Size = new System.Drawing.Size(321, 20);
            this.feesTablePanel.TabIndex = 146;
            // 
            // LoanFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(369, 632);
            this.ControlBox = false;
            this.Controls.Add(this.feesTablePanel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.rolloverLoanHeaderLabel);
            this.Name = "LoanFees";
            this.Text = "LoanFees";
            this.Load += new System.EventHandler(this.LoanFees_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label rolloverLoanHeaderLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TableLayoutPanel feesTablePanel;
    }
}