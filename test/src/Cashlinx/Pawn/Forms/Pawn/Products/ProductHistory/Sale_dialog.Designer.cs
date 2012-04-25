using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Products.ProductHistory
{
    partial class Sale_dialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelMSRNo = new CustomLabel();
            this.customLabelMSRNoHeading = new CustomLabel();
            this.customLabelDateTime = new CustomLabel();
            this.customLabelTotSaleHeading = new CustomLabel();
            this.customLabelSalesTaxHeading = new CustomLabel();
            this.customLabelShopNoHeading = new CustomLabel();
            this.customLabelSaleAmtWTax = new CustomLabel();
            this.customLabelSalesTaxAmount = new CustomLabel();
            this.customLabelDateTimeHeading = new CustomLabel();
            this.customLabelShopNumber = new CustomLabel();
            this.customLabelTerminalIDHeading = new CustomLabel();
            this.customLabelTenderTypeHeading = new CustomLabel();
            this.customLabelCustIDHeading = new CustomLabel();
            this.customLabelTenderData = new CustomLabel();
            this.customLabelCustID = new CustomLabel();
            this.customLabelTerminalId = new CustomLabel();
            this.customLabelCashDrawerHeading = new CustomLabel();
            this.customLabelEmpNoHeading = new CustomLabel();
            this.customLabelLayawayNoHeading = new CustomLabel();
            this.customLabelCashDrawer = new CustomLabel();
            this.customLabelEmpNo = new CustomLabel();
            this.customLabelLayawayNo = new CustomLabel();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_CloseButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.labelHeading.Size = new System.Drawing.Size(76, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Retail Sale";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.08046F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.91954F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelMSRNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelMSRNoHeading, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDateTime, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTotSaleHeading, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSalesTaxHeading, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelShopNoHeading, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSaleAmtWTax, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSalesTaxAmount, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDateTimeHeading, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelShopNumber, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTerminalIDHeading, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTenderTypeHeading, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustIDHeading, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCustID, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTerminalId, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawerHeading, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelEmpNoHeading, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelLayawayNoHeading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCashDrawer, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelEmpNo, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelLayawayNo, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 86);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(845, 91);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.SizeChanged += new System.EventHandler(this.tableLayoutPanel1_SizeChanged);
            // 
            // customLabelMSRNo
            // 
            this.customLabelMSRNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customLabelMSRNo.Location = new System.Drawing.Point(108, 0);
            this.customLabelMSRNo.Name = "customLabelMSRNo";
            this.customLabelMSRNo.Size = new System.Drawing.Size(157, 21);
            this.customLabelMSRNo.TabIndex = 5;
            this.customLabelMSRNo.Text = "123456";
            this.customLabelMSRNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelMSRNoHeading
            // 
            this.customLabelMSRNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelMSRNoHeading.Location = new System.Drawing.Point(3, 0);
            this.customLabelMSRNoHeading.Name = "customLabelMSRNoHeading";
            this.customLabelMSRNoHeading.Size = new System.Drawing.Size(99, 21);
            this.customLabelMSRNoHeading.TabIndex = 4;
            this.customLabelMSRNoHeading.Text = "MSR Number:";
            this.customLabelMSRNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelDateTime
            // 
            this.customLabelDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelDateTime.Location = new System.Drawing.Point(433, 0);
            this.customLabelDateTime.Name = "customLabelDateTime";
            this.customLabelDateTime.Size = new System.Drawing.Size(140, 21);
            this.customLabelDateTime.TabIndex = 13;
            this.customLabelDateTime.Text = "10/10/2010 10:10 AM";
            this.customLabelDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelTotSaleHeading
            // 
            this.customLabelTotSaleHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelTotSaleHeading.Location = new System.Drawing.Point(625, 0);
            this.customLabelTotSaleHeading.Name = "customLabelTotSaleHeading";
            this.customLabelTotSaleHeading.Size = new System.Drawing.Size(135, 21);
            this.customLabelTotSaleHeading.TabIndex = 17;
            this.customLabelTotSaleHeading.Text = "Total Sale Amount w/Tax:";
            this.customLabelTotSaleHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelSalesTaxHeading
            // 
            this.customLabelSalesTaxHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelSalesTaxHeading.Location = new System.Drawing.Point(625, 21);
            this.customLabelSalesTaxHeading.Name = "customLabelSalesTaxHeading";
            this.customLabelSalesTaxHeading.Size = new System.Drawing.Size(135, 20);
            this.customLabelSalesTaxHeading.TabIndex = 18;
            this.customLabelSalesTaxHeading.Text = "Sales Tax Amount:";
            this.customLabelSalesTaxHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelShopNoHeading
            // 
            this.customLabelShopNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelShopNoHeading.Location = new System.Drawing.Point(625, 41);
            this.customLabelShopNoHeading.Name = "customLabelShopNoHeading";
            this.customLabelShopNoHeading.Size = new System.Drawing.Size(135, 23);
            this.customLabelShopNoHeading.TabIndex = 19;
            this.customLabelShopNoHeading.Text = "Shop Number:";
            this.customLabelShopNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelSaleAmtWTax
            // 
            this.customLabelSaleAmtWTax.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSaleAmtWTax.Location = new System.Drawing.Point(766, 0);
            this.customLabelSaleAmtWTax.Name = "customLabelSaleAmtWTax";
            this.customLabelSaleAmtWTax.Size = new System.Drawing.Size(68, 21);
            this.customLabelSaleAmtWTax.TabIndex = 20;
            this.customLabelSaleAmtWTax.Text = "$0.00";
            this.customLabelSaleAmtWTax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelSalesTaxAmount
            // 
            this.customLabelSalesTaxAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSalesTaxAmount.Location = new System.Drawing.Point(766, 21);
            this.customLabelSalesTaxAmount.Name = "customLabelSalesTaxAmount";
            this.customLabelSalesTaxAmount.Size = new System.Drawing.Size(67, 20);
            this.customLabelSalesTaxAmount.TabIndex = 21;
            this.customLabelSalesTaxAmount.Text = "$0.00";
            this.customLabelSalesTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelDateTimeHeading
            // 
            this.customLabelDateTimeHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelDateTimeHeading.Location = new System.Drawing.Point(285, 0);
            this.customLabelDateTimeHeading.Name = "customLabelDateTimeHeading";
            this.customLabelDateTimeHeading.Size = new System.Drawing.Size(128, 21);
            this.customLabelDateTimeHeading.TabIndex = 9;
            this.customLabelDateTimeHeading.Text = "Date and Time:";
            this.customLabelDateTimeHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelShopNumber
            // 
            this.customLabelShopNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelShopNumber.Location = new System.Drawing.Point(766, 41);
            this.customLabelShopNumber.Name = "customLabelShopNumber";
            this.customLabelShopNumber.Size = new System.Drawing.Size(67, 23);
            this.customLabelShopNumber.TabIndex = 22;
            this.customLabelShopNumber.Text = "02030";
            this.customLabelShopNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelTerminalIDHeading
            // 
            this.customLabelTerminalIDHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabelTerminalIDHeading.Location = new System.Drawing.Point(643, 64);
            this.customLabelTerminalIDHeading.Name = "customLabelTerminalIDHeading";
            this.customLabelTerminalIDHeading.Size = new System.Drawing.Size(99, 20);
            this.customLabelTerminalIDHeading.TabIndex = 11;
            this.customLabelTerminalIDHeading.Text = "Terminal ID:";
            this.customLabelTerminalIDHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.customLabelTerminalIDHeading.Visible = false;
            // 
            // customLabelTenderTypeHeading
            // 
            this.customLabelTenderTypeHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelTenderTypeHeading.Location = new System.Drawing.Point(288, 41);
            this.customLabelTenderTypeHeading.Name = "customLabelTenderTypeHeading";
            this.customLabelTenderTypeHeading.Size = new System.Drawing.Size(121, 23);
            this.customLabelTenderTypeHeading.TabIndex = 10;
            this.customLabelTenderTypeHeading.Text = "Tender Types:";
            this.customLabelTenderTypeHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCustIDHeading
            // 
            this.customLabelCustIDHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelCustIDHeading.Location = new System.Drawing.Point(285, 21);
            this.customLabelCustIDHeading.Name = "customLabelCustIDHeading";
            this.customLabelCustIDHeading.Size = new System.Drawing.Size(127, 20);
            this.customLabelCustIDHeading.TabIndex = 12;
            this.customLabelCustIDHeading.Text = "Customer Identification:";
            this.customLabelCustIDHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelTenderData
            // 
            this.customLabelTenderData.AutoSize = true;
            this.customLabelTenderData.Location = new System.Drawing.Point(0, 0);
            this.customLabelTenderData.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.customLabelTenderData.Name = "customLabelTenderData";
            this.customLabelTenderData.Size = new System.Drawing.Size(68, 13);
            this.customLabelTenderData.TabIndex = 14;
            this.customLabelTenderData.Text = "Cash $50.00";
            this.customLabelTenderData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelCustID
            // 
            this.customLabelCustID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelCustID.Location = new System.Drawing.Point(433, 21);
            this.customLabelCustID.Name = "customLabelCustID";
            this.customLabelCustID.Size = new System.Drawing.Size(99, 20);
            this.customLabelCustID.TabIndex = 16;
            this.customLabelCustID.Text = "TX DL 09876543";
            this.customLabelCustID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelTerminalId
            // 
            this.customLabelTerminalId.Location = new System.Drawing.Point(766, 64);
            this.customLabelTerminalId.Name = "customLabelTerminalId";
            this.customLabelTerminalId.Size = new System.Drawing.Size(74, 20);
            this.customLabelTerminalId.TabIndex = 15;
            this.customLabelTerminalId.Text = "0000002";
            this.customLabelTerminalId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customLabelTerminalId.Visible = false;
            // 
            // customLabelCashDrawerHeading
            // 
            this.customLabelCashDrawerHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelCashDrawerHeading.Location = new System.Drawing.Point(3, 64);
            this.customLabelCashDrawerHeading.Name = "customLabelCashDrawerHeading";
            this.customLabelCashDrawerHeading.Size = new System.Drawing.Size(99, 27);
            this.customLabelCashDrawerHeading.TabIndex = 3;
            this.customLabelCashDrawerHeading.Text = "Cash Drawer:";
            this.customLabelCashDrawerHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelEmpNoHeading
            // 
            this.customLabelEmpNoHeading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.customLabelEmpNoHeading.Location = new System.Drawing.Point(3, 41);
            this.customLabelEmpNoHeading.Name = "customLabelEmpNoHeading";
            this.customLabelEmpNoHeading.Size = new System.Drawing.Size(99, 23);
            this.customLabelEmpNoHeading.TabIndex = 2;
            this.customLabelEmpNoHeading.Text = "Employee Number:";
            this.customLabelEmpNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelLayawayNoHeading
            // 
            this.customLabelLayawayNoHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.customLabelLayawayNoHeading.Location = new System.Drawing.Point(3, 21);
            this.customLabelLayawayNoHeading.Name = "customLabelLayawayNoHeading";
            this.customLabelLayawayNoHeading.Size = new System.Drawing.Size(99, 20);
            this.customLabelLayawayNoHeading.TabIndex = 1;
            this.customLabelLayawayNoHeading.Text = "Layaway #:";
            this.customLabelLayawayNoHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabelCashDrawer
            // 
            this.customLabelCashDrawer.Location = new System.Drawing.Point(108, 64);
            this.customLabelCashDrawer.Name = "customLabelCashDrawer";
            this.customLabelCashDrawer.Size = new System.Drawing.Size(157, 27);
            this.customLabelCashDrawer.TabIndex = 8;
            this.customLabelCashDrawer.Text = "store#_xyz";
            this.customLabelCashDrawer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelEmpNo
            // 
            this.customLabelEmpNo.Location = new System.Drawing.Point(108, 41);
            this.customLabelEmpNo.Name = "customLabelEmpNo";
            this.customLabelEmpNo.Size = new System.Drawing.Size(157, 20);
            this.customLabelEmpNo.TabIndex = 7;
            this.customLabelEmpNo.Text = "123465";
            this.customLabelEmpNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabelLayawayNo
            // 
            this.customLabelLayawayNo.Location = new System.Drawing.Point(108, 21);
            this.customLabelLayawayNo.Name = "customLabelLayawayNo";
            this.customLabelLayawayNo.Size = new System.Drawing.Size(157, 20);
            this.customLabelLayawayNo.TabIndex = 6;
            this.customLabelLayawayNo.Text = "11111111";
            this.customLabelLayawayNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AllowUserToAddRows = false;
            this.dataGridViewItems.AllowUserToDeleteRows = false;
            this.dataGridViewItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icn,
            this.description,
            this.status,
            this.amount});
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewItems.DefaultCellStyle = dataGridViewCellStyle27;
            this.dataGridViewItems.Location = new System.Drawing.Point(12, 187);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.ReadOnly = true;
            this.dataGridViewItems.RowHeadersVisible = false;
            this.dataGridViewItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewItems.Size = new System.Drawing.Size(842, 105);
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
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.description.DefaultCellStyle = dataGridViewCellStyle26;
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 370;
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
            this.PH_CloseButton.Location = new System.Drawing.Point(734, 339);
            this.PH_CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.PH_CloseButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.Name = "PH_CloseButton";
            this.PH_CloseButton.Size = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.TabIndex = 157;
            this.PH_CloseButton.Text = "Close";
            this.PH_CloseButton.UseVisualStyleBackColor = false;
            this.PH_CloseButton.Click += new System.EventHandler(this.PH_CloseButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.customLabelTenderData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(433, 44);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(186, 44);
            this.panel1.TabIndex = 158;
            // 
            // Sale_dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 408);
            this.Controls.Add(this.PH_CloseButton);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelHeading);
            this.Name = "Sale_dialog";
            this.Text = "Sale_dialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomLabel customLabelEmpNoHeading;
        private CustomLabel customLabelCashDrawerHeading;
        private CustomLabel customLabelLayawayNoHeading;
        private CustomLabel customLabelMSRNo;
        private CustomLabel customLabelMSRNoHeading;
        private CustomLabel customLabelLayawayNo;
        private CustomLabel customLabelEmpNo;
        private CustomLabel customLabelCashDrawer;
        private CustomLabel customLabelDateTimeHeading;
        private CustomLabel customLabelTenderTypeHeading;
        private CustomLabel customLabelTerminalIDHeading;
        private CustomLabel customLabelCustIDHeading;
        private CustomLabel customLabelDateTime;
        private CustomLabel customLabelTenderData;
        private CustomLabel customLabelTerminalId;
        private CustomLabel customLabelCustID;
        private CustomLabel customLabelTotSaleHeading;
        private CustomLabel customLabelSalesTaxHeading;
        private CustomLabel customLabelShopNoHeading;
        private CustomLabel customLabelSaleAmtWTax;
        private CustomLabel customLabelSalesTaxAmount;
        private CustomLabel customLabelShopNumber;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.Button PH_CloseButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.Panel panel1;
    }
}
