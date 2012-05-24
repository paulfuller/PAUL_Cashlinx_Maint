using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Tender
{
    partial class ReceiptConfirmation
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
            this.receiptLabel = new System.Windows.Forms.Label();
            this.okButton = new CustomButton();
            this.customLabel1 = new CustomLabel();
            this.customerNameFieldLabel = new CustomLabel();
            this.amountReceivedFieldLabel = new CustomLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customLabel3 = new CustomLabel();
            this.tickerNumberFieldLabel = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // receiptLabel
            // 
            this.receiptLabel.AutoSize = true;
            this.receiptLabel.BackColor = System.Drawing.Color.Transparent;
            this.receiptLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.receiptLabel.Location = new System.Drawing.Point(272, 30);
            this.receiptLabel.Name = "receiptLabel";
            this.receiptLabel.Size = new System.Drawing.Size(157, 19);
            this.receiptLabel.TabIndex = 1;
            this.receiptLabel.Text = "Receipt Confirmation";
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(578, 258);
            this.okButton.Margin = new System.Windows.Forms.Padding(0);
            this.okButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.okButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 50);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // customLabel1
            // 
            this.customLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(141, 9);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(114, 16);
            this.customLabel1.TabIndex = 3;
            this.customLabel1.Text = "Customer Name:";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customerNameFieldLabel
            // 
            this.customerNameFieldLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customerNameFieldLabel.AutoSize = true;
            this.customerNameFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.customerNameFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerNameFieldLabel.Location = new System.Drawing.Point(261, 9);
            this.customerNameFieldLabel.Name = "customerNameFieldLabel";
            this.customerNameFieldLabel.Size = new System.Drawing.Size(139, 16);
            this.customerNameFieldLabel.TabIndex = 5;
            this.customerNameFieldLabel.Text = "<customerNameField>";
            this.customerNameFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // amountReceivedFieldLabel
            // 
            this.amountReceivedFieldLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.amountReceivedFieldLabel.AutoSize = true;
            this.amountReceivedFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.amountReceivedFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountReceivedFieldLabel.Location = new System.Drawing.Point(261, 41);
            this.amountReceivedFieldLabel.Name = "amountReceivedFieldLabel";
            this.amountReceivedFieldLabel.Size = new System.Drawing.Size(147, 16);
            this.amountReceivedFieldLabel.TabIndex = 6;
            this.amountReceivedFieldLabel.Text = "<amountReceivedField>";
            this.amountReceivedFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.customLabel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.amountReceivedFieldLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customerNameFieldLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tickerNumberFieldLabel, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(78, 73);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.57143F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.42857F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(517, 100);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // customLabel3
            // 
            this.customLabel3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabel3.AutoSize = true;
            this.customLabel3.BackColor = System.Drawing.Color.Transparent;
            this.customLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel3.Location = new System.Drawing.Point(151, 74);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(104, 16);
            this.customLabel3.TabIndex = 7;
            this.customLabel3.Text = "Ticket Number:";
            this.customLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tickerNumberFieldLabel
            // 
            this.tickerNumberFieldLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tickerNumberFieldLabel.AutoSize = true;
            this.tickerNumberFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.tickerNumberFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tickerNumberFieldLabel.Location = new System.Drawing.Point(261, 74);
            this.tickerNumberFieldLabel.Name = "tickerNumberFieldLabel";
            this.tickerNumberFieldLabel.Size = new System.Drawing.Size(128, 16);
            this.tickerNumberFieldLabel.TabIndex = 8;
            this.tickerNumberFieldLabel.Text = "<ticketNumberField>";
            this.tickerNumberFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabel2
            // 
            this.customLabel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(82, 41);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(173, 16);
            this.customLabel2.TabIndex = 4;
            this.customLabel2.Text = "Received From Customer:";
            this.customLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReceiptConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 320);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.receiptLabel);
            this.MaximumSize = new System.Drawing.Size(700, 320);
            this.MinimumSize = new System.Drawing.Size(700, 320);
            this.Name = "ReceiptConfirmation";
            this.Text = "ReceiptConfirmation";
            this.Load += new System.EventHandler(this.ReceiptConfirmation_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label receiptLabel;
        private CustomButton okButton;
        private CustomLabel customLabel1;
        private CustomLabel customerNameFieldLabel;
        private CustomLabel amountReceivedFieldLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomLabel customLabel3;
        private CustomLabel customLabel2;
        private CustomLabel tickerNumberFieldLabel;
    }
}