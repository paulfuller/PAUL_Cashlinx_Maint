using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Products.ProductHistory
{
    partial class VoidSale_dialog
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
            this.labelHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelEmpNoHeading = new CustomLabel();
            this.customLabelCustIDHeading = new CustomLabel();
            this.customLabelMSRNoHeading = new CustomLabel();
            this.customLabelOrigMSRNo = new CustomLabel();
            this.customLabelEmpNo = new CustomLabel();
            this.customLabelStoreNoHeading = new CustomLabel();
            this.customLabelCashDrawerHeading = new CustomLabel();
            this.customLabelSaleStoreNo = new CustomLabel();
            this.customLabelSaleAmtHeading = new CustomLabel();
            this.customLabelOrigDateHeading = new CustomLabel();
            this.customLabelTerminalHeading = new CustomLabel();
            this.customLabelTermailNo = new CustomLabel();
            this.customLabelStatus = new CustomLabel();
            this.customLabelReasonHeading = new CustomLabel();
            this.customLabelReason = new CustomLabel();
            this.customLabelCommentHeading = new CustomLabel();
            this.customLabelComment = new CustomLabel();
            this.customLabelCashDrawer = new CustomLabel();
            this.customLabelDateTime = new CustomLabel();
            this.customLabelCustID = new CustomLabel();
            this.customLabelSaleAmtWTax = new CustomLabel();
            this.customLabelStatusHeading = new CustomLabel();
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
            this.labelHeading.Location = new System.Drawing.Point(23, 28);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(108, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Void Retail Sale";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.Controls.Add(this.customLabelEmpNoHeading, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustIDHeading, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelMSRNoHeading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelOrigMSRNo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelEmpNo, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelStoreNoHeading, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawerHeading, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSaleStoreNo, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSaleAmtHeading, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelOrigDateHeading, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTerminalHeading, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTermailNo, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelStatus, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelReasonHeading, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelReason, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCommentHeading, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelComment, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawer, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDateTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustID, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSaleAmtWTax, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelStatusHeading, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.4299F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.1028F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.23365F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.23365F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 108);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // customLabelEmpNoHeading
            // 
            this.customLabelEmpNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelEmpNoHeading.Location = new System.Drawing.Point(28, 53);
            this.customLabelEmpNoHeading.Name = "customLabelEmpNoHeading";
            this.customLabelEmpNoHeading.Size = new System.Drawing.Size(101, 27);
            this.customLabelEmpNoHeading.TabIndex = 2;
            this.customLabelEmpNoHeading.Text = "Employee Number:";
            this.customLabelEmpNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCustIDHeading
            // 
            this.customLabelCustIDHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelCustIDHeading.Location = new System.Drawing.Point(12, 80);
            this.customLabelCustIDHeading.Name = "customLabelCustIDHeading";
            this.customLabelCustIDHeading.Size = new System.Drawing.Size(133, 28);
            this.customLabelCustIDHeading.TabIndex = 3;
            this.customLabelCustIDHeading.Text = "Customer Identification:";
            this.customLabelCustIDHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelMSRNoHeading
            // 
            this.customLabelMSRNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelMSRNoHeading.Location = new System.Drawing.Point(3, 24);
            this.customLabelMSRNoHeading.Name = "customLabelMSRNoHeading";
            this.customLabelMSRNoHeading.Size = new System.Drawing.Size(152, 29);
            this.customLabelMSRNoHeading.TabIndex = 1;
            this.customLabelMSRNoHeading.Text = "Original MSR Refund Number:";
            this.customLabelMSRNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelOrigMSRNo
            // 
            this.customLabelOrigMSRNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelOrigMSRNo.Location = new System.Drawing.Point(161, 24);
            this.customLabelOrigMSRNo.Name = "customLabelOrigMSRNo";
            this.customLabelOrigMSRNo.Size = new System.Drawing.Size(118, 29);
            this.customLabelOrigMSRNo.TabIndex = 6;
            this.customLabelOrigMSRNo.Text = "11111111";
            this.customLabelOrigMSRNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelEmpNo
            // 
            this.customLabelEmpNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelEmpNo.Location = new System.Drawing.Point(161, 53);
            this.customLabelEmpNo.Name = "customLabelEmpNo";
            this.customLabelEmpNo.Size = new System.Drawing.Size(118, 27);
            this.customLabelEmpNo.TabIndex = 7;
            this.customLabelEmpNo.Text = "123465";
            this.customLabelEmpNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelStoreNoHeading
            // 
            this.customLabelStoreNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelStoreNoHeading.Location = new System.Drawing.Point(621, 0);
            this.customLabelStoreNoHeading.Name = "customLabelStoreNoHeading";
            this.customLabelStoreNoHeading.Size = new System.Drawing.Size(114, 24);
            this.customLabelStoreNoHeading.TabIndex = 17;
            this.customLabelStoreNoHeading.Text = "Shop Number:";
            this.customLabelStoreNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCashDrawerHeading
            // 
            this.customLabelCashDrawerHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelCashDrawerHeading.Location = new System.Drawing.Point(621, 24);
            this.customLabelCashDrawerHeading.Name = "customLabelCashDrawerHeading";
            this.customLabelCashDrawerHeading.Size = new System.Drawing.Size(114, 29);
            this.customLabelCashDrawerHeading.TabIndex = 18;
            this.customLabelCashDrawerHeading.Text = "Cash Drawer:";
            this.customLabelCashDrawerHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelSaleStoreNo
            // 
            this.customLabelSaleStoreNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSaleStoreNo.Location = new System.Drawing.Point(742, 0);
            this.customLabelSaleStoreNo.Name = "customLabelSaleStoreNo";
            this.customLabelSaleStoreNo.Size = new System.Drawing.Size(68, 24);
            this.customLabelSaleStoreNo.TabIndex = 20;
            this.customLabelSaleStoreNo.Text = "02030";
            this.customLabelSaleStoreNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelSaleAmtHeading
            // 
            this.customLabelSaleAmtHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelSaleAmtHeading.Location = new System.Drawing.Point(301, 0);
            this.customLabelSaleAmtHeading.Name = "customLabelSaleAmtHeading";
            this.customLabelSaleAmtHeading.Size = new System.Drawing.Size(99, 24);
            this.customLabelSaleAmtHeading.TabIndex = 9;
            this.customLabelSaleAmtHeading.Text = "Sale Amount:";
            this.customLabelSaleAmtHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelOrigDateHeading
            // 
            this.customLabelOrigDateHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelOrigDateHeading.Location = new System.Drawing.Point(8, 0);
            this.customLabelOrigDateHeading.Name = "customLabelOrigDateHeading";
            this.customLabelOrigDateHeading.Size = new System.Drawing.Size(141, 24);
            this.customLabelOrigDateHeading.TabIndex = 4;
            this.customLabelOrigDateHeading.Text = "Origination Date and Time:";
            this.customLabelOrigDateHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelTerminalHeading
            // 
            this.customLabelTerminalHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelTerminalHeading.Location = new System.Drawing.Point(628, 53);
            this.customLabelTerminalHeading.Name = "customLabelTerminalHeading";
            this.customLabelTerminalHeading.Size = new System.Drawing.Size(99, 27);
            this.customLabelTerminalHeading.TabIndex = 10;
            this.customLabelTerminalHeading.Text = "Terminal ID:";
            this.customLabelTerminalHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelTermailNo
            // 
            this.customLabelTermailNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelTermailNo.Location = new System.Drawing.Point(742, 53);
            this.customLabelTermailNo.Name = "customLabelTermailNo";
            this.customLabelTermailNo.Size = new System.Drawing.Size(90, 27);
            this.customLabelTermailNo.TabIndex = 14;
            this.customLabelTermailNo.Text = "1111";
            this.customLabelTermailNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelStatus
            // 
            this.customLabelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelStatus.Location = new System.Drawing.Point(421, 25);
            this.customLabelStatus.Name = "customLabelStatus";
            this.customLabelStatus.Size = new System.Drawing.Size(99, 27);
            this.customLabelStatus.TabIndex = 15;
            this.customLabelStatus.Text = "VO";
            this.customLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelReasonHeading
            // 
            this.customLabelReasonHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelReasonHeading.Location = new System.Drawing.Point(288, 53);
            this.customLabelReasonHeading.Name = "customLabelReasonHeading";
            this.customLabelReasonHeading.Size = new System.Drawing.Size(126, 27);
            this.customLabelReasonHeading.TabIndex = 12;
            this.customLabelReasonHeading.Text = "Reason:";
            this.customLabelReasonHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelReason
            // 
            this.customLabelReason.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelReason.Location = new System.Drawing.Point(421, 53);
            this.customLabelReason.Name = "customLabelReason";
            this.customLabelReason.Size = new System.Drawing.Size(186, 27);
            this.customLabelReason.TabIndex = 16;
            this.customLabelReason.Text = "Customer Changed Mind";
            this.customLabelReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelCommentHeading
            // 
            this.customLabelCommentHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelCommentHeading.Location = new System.Drawing.Point(288, 80);
            this.customLabelCommentHeading.Name = "customLabelCommentHeading";
            this.customLabelCommentHeading.Size = new System.Drawing.Size(126, 28);
            this.customLabelCommentHeading.TabIndex = 22;
            this.customLabelCommentHeading.Text = "Comment:";
            this.customLabelCommentHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelComment
            // 
            this.customLabelComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelComment.Location = new System.Drawing.Point(421, 80);
            this.customLabelComment.Name = "customLabelComment";
            this.customLabelComment.Size = new System.Drawing.Size(186, 27);
            this.customLabelComment.TabIndex = 23;
            this.customLabelComment.Text = "Customer Changed Mind";
            this.customLabelComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelCashDrawer
            // 
            this.customLabelCashDrawer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelCashDrawer.Location = new System.Drawing.Point(742, 24);
            this.customLabelCashDrawer.Name = "customLabelCashDrawer";
            this.customLabelCashDrawer.Size = new System.Drawing.Size(90, 28);
            this.customLabelCashDrawer.TabIndex = 8;
            this.customLabelCashDrawer.Text = "store#_xyz";
            this.customLabelCashDrawer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelDateTime
            // 
            this.customLabelDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelDateTime.Location = new System.Drawing.Point(161, 0);
            this.customLabelDateTime.Name = "customLabelDateTime";
            this.customLabelDateTime.Size = new System.Drawing.Size(120, 24);
            this.customLabelDateTime.TabIndex = 13;
            this.customLabelDateTime.Text = "10/10/2010 10:10 AM";
            this.customLabelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelCustID
            // 
            this.customLabelCustID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelCustID.Location = new System.Drawing.Point(161, 80);
            this.customLabelCustID.Name = "customLabelCustID";
            this.customLabelCustID.Size = new System.Drawing.Size(99, 27);
            this.customLabelCustID.TabIndex = 24;
            this.customLabelCustID.Text = "0000002";
            this.customLabelCustID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelSaleAmtWTax
            // 
            this.customLabelSaleAmtWTax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSaleAmtWTax.Location = new System.Drawing.Point(421, 0);
            this.customLabelSaleAmtWTax.Name = "customLabelSaleAmtWTax";
            this.customLabelSaleAmtWTax.Size = new System.Drawing.Size(68, 24);
            this.customLabelSaleAmtWTax.TabIndex = 25;
            this.customLabelSaleAmtWTax.Text = "$0.00";
            this.customLabelSaleAmtWTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelStatusHeading
            // 
            this.customLabelStatusHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelStatusHeading.Location = new System.Drawing.Point(301, 24);
            this.customLabelStatusHeading.Name = "customLabelStatusHeading";
            this.customLabelStatusHeading.Size = new System.Drawing.Size(99, 29);
            this.customLabelStatusHeading.TabIndex = 11;
            this.customLabelStatusHeading.Text = "Current Status:";
            this.customLabelStatusHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AllowUserToAddRows = false;
            this.dataGridViewItems.AllowUserToDeleteRows = false;
            this.dataGridViewItems.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
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
            this.dataGridViewItems.Location = new System.Drawing.Point(12, 184);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.ReadOnly = true;
            this.dataGridViewItems.RowHeadersVisible = false;
            this.dataGridViewItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewItems.Size = new System.Drawing.Size(839, 52);
            this.dataGridViewItems.TabIndex = 2;
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
            this.PH_CloseButton.Location = new System.Drawing.Point(732, 303);
            this.PH_CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.PH_CloseButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.Name = "PH_CloseButton";
            this.PH_CloseButton.Size = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.TabIndex = 157;
            this.PH_CloseButton.Text = "Close";
            this.PH_CloseButton.UseVisualStyleBackColor = false;
            // 
            // VoidSale_dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 362);
            this.Controls.Add(this.PH_CloseButton);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelHeading);
            this.Name = "VoidSale_dialog";
            this.Text = "Sale_dialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomLabel customLabelEmpNoHeading;
        private CustomLabel customLabelCustIDHeading;
        private CustomLabel customLabelMSRNoHeading;
        private CustomLabel customLabelOrigDateHeading;
        private CustomLabel customLabelOrigMSRNo;
        private CustomLabel customLabelEmpNo;
        private CustomLabel customLabelCashDrawer;
        private CustomLabel customLabelSaleAmtHeading;
        private CustomLabel customLabelTerminalHeading;
        private CustomLabel customLabelStatusHeading;
        private CustomLabel customLabelReasonHeading;
        private CustomLabel customLabelDateTime;
        private CustomLabel customLabelTermailNo;
        private CustomLabel customLabelStatus;
        private CustomLabel customLabelReason;
        private CustomLabel customLabelStoreNoHeading;
        private CustomLabel customLabelCashDrawerHeading;
        private CustomLabel customLabelSaleStoreNo;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.Button PH_CloseButton;
        private CustomLabel customLabelCommentHeading;
        private CustomLabel customLabelComment;
        private CustomLabel customLabelCustID;
        private CustomLabel customLabelSaleAmtWTax;
    }
}