using Common.Libraries.Forms.Components;

namespace Support.Forms.Customer.Products.ProductHistory
{
    partial class Layaway_dialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.PH_CloseButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelCashDrawer = new CustomLabel();
            this.customLabelLayawayNoHeading = new CustomLabel();
            this.customLabelLayawayNo = new CustomLabel();
            this.customLabelDateTimeHeading = new CustomLabel();
            this.customLabelDateTime = new CustomLabel();
            this.customLabelCustIDHeading = new CustomLabel();
            this.customLabelTotSaleHeading = new CustomLabel();
            this.customLabelSaleAmtWTax = new CustomLabel();
            this.customLabelShopNoHeading = new CustomLabel();
            this.customLabelTerminalIDHeading = new CustomLabel();
            this.customLabelCustID = new CustomLabel();
            this.customLabelEmpNoHeading = new CustomLabel();
            this.customLabelCashDrawerHeading = new CustomLabel();
            this.customLabelShopNumber = new CustomLabel();
            this.customLabelTerminalId = new CustomLabel();
            this.customLabelEmpNo = new CustomLabel();
            this.customLabelTenderData = new CustomLabel();
            this.customLabelTenderTypeHeading = new CustomLabel();
            this.labelHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PH_CloseButton
            // 
            this.PH_CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
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
            this.PH_CloseButton.Location = new System.Drawing.Point(739, 168);
            this.PH_CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.PH_CloseButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.Name = "PH_CloseButton";
            this.PH_CloseButton.Size = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.TabIndex = 160;
            this.PH_CloseButton.Text = "Close";
            this.PH_CloseButton.UseVisualStyleBackColor = false;
            this.PH_CloseButton.Click += new System.EventHandler(this.PH_CloseButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.30112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.69888F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawer, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelLayawayNoHeading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelLayawayNo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDateTimeHeading, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDateTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustIDHeading, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTotSaleHeading, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSaleAmtWTax, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelShopNoHeading, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTerminalIDHeading, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustID, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelEmpNoHeading, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawerHeading, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelShopNumber, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTerminalId, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelEmpNo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTenderData, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTenderTypeHeading, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(847, 100);
            this.tableLayoutPanel1.TabIndex = 158;
            this.tableLayoutPanel1.TabStop = true;
            this.tableLayoutPanel1.SizeChanged += new System.EventHandler(this.tableLayoutPanel1_SizeChanged);
            // 
            // customLabelCashDrawer
            // 
            this.customLabelCashDrawer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabelCashDrawer.Location = new System.Drawing.Point(713, 60);
            this.customLabelCashDrawer.Name = "customLabelCashDrawer";
            this.customLabelCashDrawer.Size = new System.Drawing.Size(126, 35);
            this.customLabelCashDrawer.TabIndex = 26;
            this.customLabelCashDrawer.Text = "store#_xyz";
            this.customLabelCashDrawer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelLayawayNoHeading
            // 
            this.customLabelLayawayNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelLayawayNoHeading.Location = new System.Drawing.Point(18, 30);
            this.customLabelLayawayNoHeading.Name = "customLabelLayawayNoHeading";
            this.customLabelLayawayNoHeading.Size = new System.Drawing.Size(99, 30);
            this.customLabelLayawayNoHeading.TabIndex = 1;
            this.customLabelLayawayNoHeading.Text = "Layaway #:";
            this.customLabelLayawayNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelLayawayNo
            // 
            this.customLabelLayawayNo.Location = new System.Drawing.Point(139, 30);
            this.customLabelLayawayNo.Name = "customLabelLayawayNo";
            this.customLabelLayawayNo.Size = new System.Drawing.Size(124, 26);
            this.customLabelLayawayNo.TabIndex = 6;
            this.customLabelLayawayNo.Text = "11111111";
            this.customLabelLayawayNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelDateTimeHeading
            // 
            this.customLabelDateTimeHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelDateTimeHeading.Location = new System.Drawing.Point(18, 0);
            this.customLabelDateTimeHeading.Name = "customLabelDateTimeHeading";
            this.customLabelDateTimeHeading.Size = new System.Drawing.Size(99, 30);
            this.customLabelDateTimeHeading.TabIndex = 9;
            this.customLabelDateTimeHeading.Text = "Date and Time:";
            this.customLabelDateTimeHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelDateTime
            // 
            this.customLabelDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelDateTime.Location = new System.Drawing.Point(139, 7);
            this.customLabelDateTime.Name = "customLabelDateTime";
            this.customLabelDateTime.Size = new System.Drawing.Size(124, 15);
            this.customLabelDateTime.TabIndex = 13;
            this.customLabelDateTime.Text = "10/10/2010 10:10 AM";
            this.customLabelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelCustIDHeading
            // 
            this.customLabelCustIDHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabelCustIDHeading.Location = new System.Drawing.Point(5, 60);
            this.customLabelCustIDHeading.Name = "customLabelCustIDHeading";
            this.customLabelCustIDHeading.Size = new System.Drawing.Size(126, 35);
            this.customLabelCustIDHeading.TabIndex = 12;
            this.customLabelCustIDHeading.Text = "Customer Identification:";
            this.customLabelCustIDHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelTotSaleHeading
            // 
            this.customLabelTotSaleHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelTotSaleHeading.Location = new System.Drawing.Point(278, 0);
            this.customLabelTotSaleHeading.Name = "customLabelTotSaleHeading";
            this.customLabelTotSaleHeading.Size = new System.Drawing.Size(135, 30);
            this.customLabelTotSaleHeading.TabIndex = 17;
            this.customLabelTotSaleHeading.Text = "Layaway Amount:";
            this.customLabelTotSaleHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelSaleAmtWTax
            // 
            this.customLabelSaleAmtWTax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSaleAmtWTax.Location = new System.Drawing.Point(428, 3);
            this.customLabelSaleAmtWTax.Name = "customLabelSaleAmtWTax";
            this.customLabelSaleAmtWTax.Size = new System.Drawing.Size(68, 24);
            this.customLabelSaleAmtWTax.TabIndex = 20;
            this.customLabelSaleAmtWTax.Text = "$0.00";
            this.customLabelSaleAmtWTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelShopNoHeading
            // 
            this.customLabelShopNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelShopNoHeading.Location = new System.Drawing.Point(587, 0);
            this.customLabelShopNoHeading.Name = "customLabelShopNoHeading";
            this.customLabelShopNoHeading.Size = new System.Drawing.Size(116, 30);
            this.customLabelShopNoHeading.TabIndex = 19;
            this.customLabelShopNoHeading.Text = "Shop Number:";
            this.customLabelShopNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelTerminalIDHeading
            // 
            this.customLabelTerminalIDHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelTerminalIDHeading.Location = new System.Drawing.Point(587, 30);
            this.customLabelTerminalIDHeading.Name = "customLabelTerminalIDHeading";
            this.customLabelTerminalIDHeading.Size = new System.Drawing.Size(116, 30);
            this.customLabelTerminalIDHeading.TabIndex = 11;
            this.customLabelTerminalIDHeading.Text = "Terminal ID:";
            this.customLabelTerminalIDHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCustID
            // 
            this.customLabelCustID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabelCustID.Location = new System.Drawing.Point(151, 60);
            this.customLabelCustID.Name = "customLabelCustID";
            this.customLabelCustID.Size = new System.Drawing.Size(99, 35);
            this.customLabelCustID.TabIndex = 16;
            this.customLabelCustID.Text = "TX DL 09876543";
            this.customLabelCustID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelEmpNoHeading
            // 
            this.customLabelEmpNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelEmpNoHeading.Location = new System.Drawing.Point(278, 30);
            this.customLabelEmpNoHeading.Name = "customLabelEmpNoHeading";
            this.customLabelEmpNoHeading.Size = new System.Drawing.Size(135, 30);
            this.customLabelEmpNoHeading.TabIndex = 23;
            this.customLabelEmpNoHeading.Text = "Employee Number:";
            this.customLabelEmpNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCashDrawerHeading
            // 
            this.customLabelCashDrawerHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabelCashDrawerHeading.Location = new System.Drawing.Point(587, 60);
            this.customLabelCashDrawerHeading.Name = "customLabelCashDrawerHeading";
            this.customLabelCashDrawerHeading.Size = new System.Drawing.Size(116, 35);
            this.customLabelCashDrawerHeading.TabIndex = 25;
            this.customLabelCashDrawerHeading.Text = "Cash Drawer:";
            this.customLabelCashDrawerHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelShopNumber
            // 
            this.customLabelShopNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelShopNumber.Location = new System.Drawing.Point(709, 3);
            this.customLabelShopNumber.Name = "customLabelShopNumber";
            this.customLabelShopNumber.Size = new System.Drawing.Size(67, 24);
            this.customLabelShopNumber.TabIndex = 22;
            this.customLabelShopNumber.Text = "02030";
            this.customLabelShopNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelTerminalId
            // 
            this.customLabelTerminalId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelTerminalId.Location = new System.Drawing.Point(709, 33);
            this.customLabelTerminalId.Name = "customLabelTerminalId";
            this.customLabelTerminalId.Size = new System.Drawing.Size(126, 24);
            this.customLabelTerminalId.TabIndex = 15;
            this.customLabelTerminalId.Text = "0000002";
            this.customLabelTerminalId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelEmpNo
            // 
            this.customLabelEmpNo.Location = new System.Drawing.Point(428, 30);
            this.customLabelEmpNo.Name = "customLabelEmpNo";
            this.customLabelEmpNo.Size = new System.Drawing.Size(152, 30);
            this.customLabelEmpNo.TabIndex = 24;
            this.customLabelEmpNo.Text = "123465";
            this.customLabelEmpNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelTenderData
            // 
            this.customLabelTenderData.AutoSize = true;
            this.customLabelTenderData.Location = new System.Drawing.Point(428, 60);
            this.customLabelTenderData.Name = "customLabelTenderData";
            this.customLabelTenderData.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.customLabelTenderData.Size = new System.Drawing.Size(68, 28);
            this.customLabelTenderData.TabIndex = 14;
            this.customLabelTenderData.Text = "Cash $50.00";
            this.customLabelTenderData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelTenderTypeHeading
            // 
            this.customLabelTenderTypeHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabelTenderTypeHeading.Location = new System.Drawing.Point(275, 60);
            this.customLabelTenderTypeHeading.Name = "customLabelTenderTypeHeading";
            this.customLabelTenderTypeHeading.Size = new System.Drawing.Size(141, 35);
            this.customLabelTenderTypeHeading.TabIndex = 10;
            this.customLabelTenderTypeHeading.Text = "Tender Types:";
            this.customLabelTenderTypeHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(15, 15);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(97, 16);
            this.labelHeading.TabIndex = 161;
            this.labelHeading.Text = "New Layaway";
            // 
            // Layaway_dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(864, 236);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.PH_CloseButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Layaway_dialog";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Layaway_dialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PH_CloseButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomLabel customLabelLayawayNoHeading;
        private CustomLabel customLabelLayawayNo;
        private CustomLabel customLabelTenderTypeHeading;
        private CustomLabel customLabelTerminalIDHeading;
        private CustomLabel customLabelCustIDHeading;
        private CustomLabel customLabelTenderData;
        private CustomLabel customLabelDateTime;
        private CustomLabel customLabelTerminalId;
        private CustomLabel customLabelCustID;
        private CustomLabel customLabelShopNoHeading;
        private CustomLabel customLabelShopNumber;
        private CustomLabel customLabelDateTimeHeading;
        private System.Windows.Forms.Label labelHeading;
        private CustomLabel customLabelTotSaleHeading;
        private CustomLabel customLabelSaleAmtWTax;
        private CustomLabel customLabelEmpNoHeading;
        private CustomLabel customLabelEmpNo;
        private CustomLabel customLabelCashDrawer;
        private CustomLabel customLabelCashDrawerHeading;

    }
}
