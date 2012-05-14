using Common.Libraries.Forms.Components;

namespace Support.Forms.Customer.Products.ProductHistory
{
    partial class SaleRefund_Dialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelMSRRefundNoHeading = new CustomLabel();
            this.customLabelOrigMSRRefNo = new CustomLabel();
            this.customLabelStoreNoHeading = new CustomLabel();
            this.customLabelSaleStoreNo = new CustomLabel();
            this.customLabelSaleAmtHeading = new CustomLabel();
            this.customLabelOrigDateHeading = new CustomLabel();
            this.customLabelStatusHeading = new CustomLabel();
            this.customLabelEmpNo = new CustomLabel();
            this.customLabelDateTime = new CustomLabel();
            this.customLabelRefundAmt = new CustomLabel();
            this.customLabelCustIDHeading = new CustomLabel();
            this.customLabelCustID = new CustomLabel();
            this.customLabelTerminalNo = new CustomLabel();
            this.customLabelCashDrawerHeading = new CustomLabel();
            this.customLabelTerminalHeading = new CustomLabel();
            this.customLabelCashDrawer = new CustomLabel();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_CloseButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(21, 27);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(108, 16);
            this.labelHeading.TabIndex = 1;
            this.labelHeading.Text = "Void Retail Sale";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.66166F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.33834F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel1.Controls.Add(this.customLabelMSRRefundNoHeading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelOrigMSRRefNo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelStoreNoHeading, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSaleStoreNo, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSaleAmtHeading, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelOrigDateHeading, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelStatusHeading, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelEmpNo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDateTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelRefundAmt, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustIDHeading, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustID, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTerminalNo, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawerHeading, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTerminalHeading, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawer, 5, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 76);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.4299F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.1028F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.23365F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.23365F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 108);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // customLabelMSRRefundNoHeading
            // 
            this.customLabelMSRRefundNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelMSRRefundNoHeading.Location = new System.Drawing.Point(24, 24);
            this.customLabelMSRRefundNoHeading.Name = "customLabelMSRRefundNoHeading";
            this.customLabelMSRRefundNoHeading.Size = new System.Drawing.Size(159, 29);
            this.customLabelMSRRefundNoHeading.TabIndex = 1;
            this.customLabelMSRRefundNoHeading.Text = "Original MSR Refund Number:";
            this.customLabelMSRRefundNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelOrigMSRRefNo
            // 
            this.customLabelOrigMSRRefNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelOrigMSRRefNo.Location = new System.Drawing.Point(211, 24);
            this.customLabelOrigMSRRefNo.Name = "customLabelOrigMSRRefNo";
            this.customLabelOrigMSRRefNo.Size = new System.Drawing.Size(87, 29);
            this.customLabelOrigMSRRefNo.TabIndex = 6;
            this.customLabelOrigMSRRefNo.Text = "11111111";
            this.customLabelOrigMSRRefNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelStoreNoHeading
            // 
            this.customLabelStoreNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelStoreNoHeading.Location = new System.Drawing.Point(545, 0);
            this.customLabelStoreNoHeading.Name = "customLabelStoreNoHeading";
            this.customLabelStoreNoHeading.Size = new System.Drawing.Size(114, 24);
            this.customLabelStoreNoHeading.TabIndex = 17;
            this.customLabelStoreNoHeading.Text = "Shop Number:";
            this.customLabelStoreNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelSaleStoreNo
            // 
            this.customLabelSaleStoreNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSaleStoreNo.Location = new System.Drawing.Point(665, 0);
            this.customLabelSaleStoreNo.Name = "customLabelSaleStoreNo";
            this.customLabelSaleStoreNo.Size = new System.Drawing.Size(68, 24);
            this.customLabelSaleStoreNo.TabIndex = 20;
            this.customLabelSaleStoreNo.Text = "02030";
            this.customLabelSaleStoreNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelSaleAmtHeading
            // 
            this.customLabelSaleAmtHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelSaleAmtHeading.Location = new System.Drawing.Point(332, 0);
            this.customLabelSaleAmtHeading.Name = "customLabelSaleAmtHeading";
            this.customLabelSaleAmtHeading.Size = new System.Drawing.Size(99, 24);
            this.customLabelSaleAmtHeading.TabIndex = 9;
            this.customLabelSaleAmtHeading.Text = "Refund Amount:";
            this.customLabelSaleAmtHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelOrigDateHeading
            // 
            this.customLabelOrigDateHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelOrigDateHeading.Location = new System.Drawing.Point(35, 0);
            this.customLabelOrigDateHeading.Name = "customLabelOrigDateHeading";
            this.customLabelOrigDateHeading.Size = new System.Drawing.Size(138, 24);
            this.customLabelOrigDateHeading.TabIndex = 4;
            this.customLabelOrigDateHeading.Text = "Origination Date and Time:";
            this.customLabelOrigDateHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelStatusHeading
            // 
            this.customLabelStatusHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelStatusHeading.Location = new System.Drawing.Point(332, 24);
            this.customLabelStatusHeading.Name = "customLabelStatusHeading";
            this.customLabelStatusHeading.Size = new System.Drawing.Size(99, 29);
            this.customLabelStatusHeading.TabIndex = 11;
            this.customLabelStatusHeading.Text = "Employee Number:";
            this.customLabelStatusHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelEmpNo
            // 
            this.customLabelEmpNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelEmpNo.Location = new System.Drawing.Point(445, 25);
            this.customLabelEmpNo.Name = "customLabelEmpNo";
            this.customLabelEmpNo.Size = new System.Drawing.Size(94, 27);
            this.customLabelEmpNo.TabIndex = 15;
            this.customLabelEmpNo.Text = "783456";
            this.customLabelEmpNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelDateTime
            // 
            this.customLabelDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelDateTime.Location = new System.Drawing.Point(211, 0);
            this.customLabelDateTime.Name = "customLabelDateTime";
            this.customLabelDateTime.Size = new System.Drawing.Size(107, 24);
            this.customLabelDateTime.TabIndex = 13;
            this.customLabelDateTime.Text = "10/10/2010 10:10 AM";
            this.customLabelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelRefundAmt
            // 
            this.customLabelRefundAmt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelRefundAmt.Location = new System.Drawing.Point(445, 0);
            this.customLabelRefundAmt.Name = "customLabelRefundAmt";
            this.customLabelRefundAmt.Size = new System.Drawing.Size(68, 24);
            this.customLabelRefundAmt.TabIndex = 25;
            this.customLabelRefundAmt.Text = "$0.00";
            this.customLabelRefundAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelCustIDHeading
            // 
            this.customLabelCustIDHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelCustIDHeading.Location = new System.Drawing.Point(37, 53);
            this.customLabelCustIDHeading.Name = "customLabelCustIDHeading";
            this.customLabelCustIDHeading.Size = new System.Drawing.Size(133, 27);
            this.customLabelCustIDHeading.TabIndex = 3;
            this.customLabelCustIDHeading.Text = "Customer Identification:";
            this.customLabelCustIDHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCustID
            // 
            this.customLabelCustID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelCustID.Location = new System.Drawing.Point(211, 53);
            this.customLabelCustID.Name = "customLabelCustID";
            this.customLabelCustID.Size = new System.Drawing.Size(87, 27);
            this.customLabelCustID.TabIndex = 24;
            this.customLabelCustID.Text = "TX DL 3432432";
            this.customLabelCustID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelTerminalNo
            // 
            this.customLabelTerminalNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelTerminalNo.Location = new System.Drawing.Point(665, 25);
            this.customLabelTerminalNo.Name = "customLabelTerminalNo";
            this.customLabelTerminalNo.Size = new System.Drawing.Size(90, 27);
            this.customLabelTerminalNo.TabIndex = 14;
            this.customLabelTerminalNo.Text = "1111";
            this.customLabelTerminalNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelCashDrawerHeading
            // 
            this.customLabelCashDrawerHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelCashDrawerHeading.Location = new System.Drawing.Point(545, 53);
            this.customLabelCashDrawerHeading.Name = "customLabelCashDrawerHeading";
            this.customLabelCashDrawerHeading.Size = new System.Drawing.Size(114, 27);
            this.customLabelCashDrawerHeading.TabIndex = 18;
            this.customLabelCashDrawerHeading.Text = "Cash Drawer:";
            this.customLabelCashDrawerHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelTerminalHeading
            // 
            this.customLabelTerminalHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelTerminalHeading.Location = new System.Drawing.Point(552, 24);
            this.customLabelTerminalHeading.Name = "customLabelTerminalHeading";
            this.customLabelTerminalHeading.Size = new System.Drawing.Size(99, 29);
            this.customLabelTerminalHeading.TabIndex = 10;
            this.customLabelTerminalHeading.Text = "Terminal ID:";
            this.customLabelTerminalHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCashDrawer
            // 
            this.customLabelCashDrawer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelCashDrawer.Location = new System.Drawing.Point(665, 53);
            this.customLabelCashDrawer.Name = "customLabelCashDrawer";
            this.customLabelCashDrawer.Size = new System.Drawing.Size(90, 27);
            this.customLabelCashDrawer.TabIndex = 8;
            this.customLabelCashDrawer.Text = "store#_xyz";
            this.customLabelCashDrawer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AllowUserToAddRows = false;
            this.dataGridViewItems.AllowUserToDeleteRows = false;
            this.dataGridViewItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icn,
            this.description,
            this.status,
            this.amount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewItems.Location = new System.Drawing.Point(12, 190);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.ReadOnly = true;
            this.dataGridViewItems.RowHeadersVisible = false;
            this.dataGridViewItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewItems.Size = new System.Drawing.Size(809, 126);
            this.dataGridViewItems.TabIndex = 3;
            // 
            // icn
            // 
            this.icn.HeaderText = "ICN";
            this.icn.Name = "icn";
            this.icn.ReadOnly = true;
            this.icn.Width = 170;
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 360;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 150;
            // 
            // amount
            // 
            this.amount.HeaderText = "Sale Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 150;
            // 
            // PH_CloseButton
            // 
            this.PH_CloseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PH_CloseButton.AutoSize = true;
            this.PH_CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.PH_CloseButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.PH_CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PH_CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PH_CloseButton.FlatAppearance.BorderSize = 0;
            this.PH_CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PH_CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PH_CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PH_CloseButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_CloseButton.ForeColor = System.Drawing.Color.White;
            this.PH_CloseButton.Location = new System.Drawing.Point(702, 319);
            this.PH_CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.PH_CloseButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.Name = "PH_CloseButton";
            this.PH_CloseButton.Size = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.TabIndex = 158;
            this.PH_CloseButton.Text = "Close";
            this.PH_CloseButton.UseVisualStyleBackColor = false;
            this.PH_CloseButton.Click += new System.EventHandler(this.PH_CloseButton_Click);
            // 
            // SaleRefund_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 378);
            this.Controls.Add(this.PH_CloseButton);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelHeading);
            this.Name = "SaleRefund_Dialog";
            this.Text = "SaleRefund_Dialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomLabel customLabelCustIDHeading;
        private CustomLabel customLabelMSRRefundNoHeading;
        private CustomLabel customLabelOrigMSRRefNo;
        private CustomLabel customLabelStoreNoHeading;
        private CustomLabel customLabelCashDrawerHeading;
        private CustomLabel customLabelSaleStoreNo;
        private CustomLabel customLabelSaleAmtHeading;
        private CustomLabel customLabelOrigDateHeading;
        private CustomLabel customLabelTerminalHeading;
        private CustomLabel customLabelTerminalNo;
        private CustomLabel customLabelStatusHeading;
        private CustomLabel customLabelEmpNo;
        private CustomLabel customLabelCashDrawer;
        private CustomLabel customLabelDateTime;
        private CustomLabel customLabelCustID;
        private CustomLabel customLabelRefundAmt;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.Button PH_CloseButton;
    }
}