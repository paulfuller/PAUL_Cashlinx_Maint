using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls
{
    partial class RetailCost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetailCost));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelRetailItems = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelTotals = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.customButtonOutTheDoor = new Common.Libraries.Forms.Components.CustomButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelLayawayServiceFee = new System.Windows.Forms.Panel();
            this.lblLayawayServiceFee = new System.Windows.Forms.Label();
            this.txtLayawayServiceFee = new System.Windows.Forms.TextBox();
            this.panelShippingAndHandling = new System.Windows.Forms.Panel();
            this.lblShippingAndHandling = new System.Windows.Forms.Label();
            this.txtShippingAndHandling = new System.Windows.Forms.TextBox();
            this.panelBackgroundCheckFee = new System.Windows.Forms.Panel();
            this.lblBackgroundCheckFee = new System.Windows.Forms.Label();
            this.txtBackgroundCheckFee = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtRetailTotal = new System.Windows.Forms.TextBox();
            this.lblRetailTotal = new System.Windows.Forms.Label();
            this.tableLayoutPanelTotalFeeData = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxCouponAmt = new System.Windows.Forms.TextBox();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.customButtonCoupon = new Common.Libraries.Forms.Components.CustomButton();
            this.label11 = new System.Windows.Forms.Label();
            this.labTaxRate = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.lblGunMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelRetailItems.SuspendLayout();
            this.panelTotals.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelLayawayServiceFee.SuspendLayout();
            this.panelShippingAndHandling.SuspendLayout();
            this.panelBackgroundCheckFee.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanelTotalFeeData.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Selected";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(908, 18);
            this.panel1.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Window;
            this.label10.Location = new System.Drawing.Point(776, 2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Coupon";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Window;
            this.label9.Location = new System.Drawing.Point(835, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Total";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Window;
            this.label8.Location = new System.Drawing.Point(738, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Qty.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(646, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Negotiated $";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Window;
            this.label6.Location = new System.Drawing.Point(562, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Discount(%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(483, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Retail Price";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(273, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Merchandise Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(177, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ICN#";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(70, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sale Type";
            // 
            // panelRetailItems
            // 
            this.panelRetailItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRetailItems.AutoScroll = true;
            this.panelRetailItems.Controls.Add(this.tableLayoutPanel1);
            this.panelRetailItems.Location = new System.Drawing.Point(0, 53);
            this.panelRetailItems.Name = "panelRetailItems";
            this.panelRetailItems.Size = new System.Drawing.Size(908, 150);
            this.panelRetailItems.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelTotals
            // 
            this.panelTotals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotals.Controls.Add(this.tableLayoutPanel2);
            this.panelTotals.Controls.Add(this.tableLayoutPanelTotalFeeData);
            this.panelTotals.Location = new System.Drawing.Point(1, 207);
            this.panelTotals.Name = "panelTotals";
            this.panelTotals.Size = new System.Drawing.Size(904, 225);
            this.panelTotals.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.customButtonOutTheDoor, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(463, 91);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(405, 132);
            this.tableLayoutPanel2.TabIndex = 169;
            // 
            // customButtonOutTheDoor
            // 
            this.customButtonOutTheDoor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customButtonOutTheDoor.BackColor = System.Drawing.Color.Transparent;
            this.customButtonOutTheDoor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonOutTheDoor.BackgroundImage")));
            this.customButtonOutTheDoor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonOutTheDoor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonOutTheDoor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonOutTheDoor.FlatAppearance.BorderSize = 0;
            this.customButtonOutTheDoor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonOutTheDoor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonOutTheDoor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonOutTheDoor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonOutTheDoor.ForeColor = System.Drawing.Color.White;
            this.customButtonOutTheDoor.Location = new System.Drawing.Point(0, 41);
            this.customButtonOutTheDoor.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonOutTheDoor.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonOutTheDoor.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonOutTheDoor.Name = "customButtonOutTheDoor";
            this.customButtonOutTheDoor.Size = new System.Drawing.Size(100, 50);
            this.customButtonOutTheDoor.TabIndex = 155;
            this.customButtonOutTheDoor.Text = "Out The Door";
            this.customButtonOutTheDoor.UseVisualStyleBackColor = false;
            this.customButtonOutTheDoor.Click += new System.EventHandler(this.customButtonOutTheDoor_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panelLayawayServiceFee);
            this.flowLayoutPanel1.Controls.Add(this.panelShippingAndHandling);
            this.flowLayoutPanel1.Controls.Add(this.panelBackgroundCheckFee);
            this.flowLayoutPanel1.Controls.Add(this.panel5);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(103, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(287, 125);
            this.flowLayoutPanel1.TabIndex = 168;
            // 
            // panelLayawayServiceFee
            // 
            this.panelLayawayServiceFee.Controls.Add(this.lblLayawayServiceFee);
            this.panelLayawayServiceFee.Controls.Add(this.txtLayawayServiceFee);
            this.panelLayawayServiceFee.Location = new System.Drawing.Point(3, 3);
            this.panelLayawayServiceFee.Name = "panelLayawayServiceFee";
            this.panelLayawayServiceFee.Size = new System.Drawing.Size(284, 24);
            this.panelLayawayServiceFee.TabIndex = 165;
            // 
            // lblLayawayServiceFee
            // 
            this.lblLayawayServiceFee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLayawayServiceFee.AutoSize = true;
            this.lblLayawayServiceFee.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayawayServiceFee.Location = new System.Drawing.Point(43, 5);
            this.lblLayawayServiceFee.Name = "lblLayawayServiceFee";
            this.lblLayawayServiceFee.Size = new System.Drawing.Size(142, 13);
            this.lblLayawayServiceFee.TabIndex = 162;
            this.lblLayawayServiceFee.Text = "Layaway Service Fee: $";
            this.lblLayawayServiceFee.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLayawayServiceFee
            // 
            this.txtLayawayServiceFee.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLayawayServiceFee.Enabled = false;
            this.txtLayawayServiceFee.Location = new System.Drawing.Point(194, 2);
            this.txtLayawayServiceFee.Name = "txtLayawayServiceFee";
            this.txtLayawayServiceFee.Size = new System.Drawing.Size(84, 20);
            this.txtLayawayServiceFee.TabIndex = 150;
            this.txtLayawayServiceFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelShippingAndHandling
            // 
            this.panelShippingAndHandling.Controls.Add(this.lblShippingAndHandling);
            this.panelShippingAndHandling.Controls.Add(this.txtShippingAndHandling);
            this.panelShippingAndHandling.Location = new System.Drawing.Point(3, 33);
            this.panelShippingAndHandling.Name = "panelShippingAndHandling";
            this.panelShippingAndHandling.Size = new System.Drawing.Size(284, 23);
            this.panelShippingAndHandling.TabIndex = 166;
            // 
            // lblShippingAndHandling
            // 
            this.lblShippingAndHandling.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblShippingAndHandling.AutoSize = true;
            this.lblShippingAndHandling.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShippingAndHandling.Location = new System.Drawing.Point(37, 3);
            this.lblShippingAndHandling.Name = "lblShippingAndHandling";
            this.lblShippingAndHandling.Size = new System.Drawing.Size(149, 13);
            this.lblShippingAndHandling.TabIndex = 2;
            this.lblShippingAndHandling.Text = "Shipping and Handling: $";
            this.lblShippingAndHandling.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtShippingAndHandling
            // 
            this.txtShippingAndHandling.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtShippingAndHandling.Location = new System.Drawing.Point(194, 0);
            this.txtShippingAndHandling.Name = "txtShippingAndHandling";
            this.txtShippingAndHandling.Size = new System.Drawing.Size(84, 20);
            this.txtShippingAndHandling.TabIndex = 149;
            this.txtShippingAndHandling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShippingAndHandling.Leave += new System.EventHandler(this.txtShippingAndHandling_Leave);
            // 
            // panelBackgroundCheckFee
            // 
            this.panelBackgroundCheckFee.Controls.Add(this.lblBackgroundCheckFee);
            this.panelBackgroundCheckFee.Controls.Add(this.txtBackgroundCheckFee);
            this.panelBackgroundCheckFee.Location = new System.Drawing.Point(3, 62);
            this.panelBackgroundCheckFee.Name = "panelBackgroundCheckFee";
            this.panelBackgroundCheckFee.Size = new System.Drawing.Size(284, 26);
            this.panelBackgroundCheckFee.TabIndex = 164;
            // 
            // lblBackgroundCheckFee
            // 
            this.lblBackgroundCheckFee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBackgroundCheckFee.AutoSize = true;
            this.lblBackgroundCheckFee.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackgroundCheckFee.Location = new System.Drawing.Point(37, 6);
            this.lblBackgroundCheckFee.Name = "lblBackgroundCheckFee";
            this.lblBackgroundCheckFee.Size = new System.Drawing.Size(151, 13);
            this.lblBackgroundCheckFee.TabIndex = 5;
            this.lblBackgroundCheckFee.Text = "Background Check Fee: $";
            this.lblBackgroundCheckFee.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBackgroundCheckFee
            // 
            this.txtBackgroundCheckFee.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBackgroundCheckFee.Location = new System.Drawing.Point(194, 3);
            this.txtBackgroundCheckFee.Name = "txtBackgroundCheckFee";
            this.txtBackgroundCheckFee.Size = new System.Drawing.Size(84, 20);
            this.txtBackgroundCheckFee.TabIndex = 150;
            this.txtBackgroundCheckFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBackgroundCheckFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBackgroundCheckFee_KeyDown);
            this.txtBackgroundCheckFee.Leave += new System.EventHandler(this.txtBackgroundCheckFee_Leave);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtRetailTotal);
            this.panel5.Controls.Add(this.lblRetailTotal);
            this.panel5.Location = new System.Drawing.Point(3, 94);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(279, 31);
            this.panel5.TabIndex = 167;
            // 
            // txtRetailTotal
            // 
            this.txtRetailTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtRetailTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRetailTotal.Enabled = false;
            this.txtRetailTotal.Location = new System.Drawing.Point(191, 3);
            this.txtRetailTotal.Name = "txtRetailTotal";
            this.txtRetailTotal.Size = new System.Drawing.Size(84, 20);
            this.txtRetailTotal.TabIndex = 152;
            this.txtRetailTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRetailTotal
            // 
            this.lblRetailTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRetailTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetailTotal.Location = new System.Drawing.Point(34, 5);
            this.lblRetailTotal.Name = "lblRetailTotal";
            this.lblRetailTotal.Size = new System.Drawing.Size(151, 13);
            this.lblRetailTotal.TabIndex = 147;
            this.lblRetailTotal.Text = "Retail Total: $";
            this.lblRetailTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tableLayoutPanelTotalFeeData
            // 
            this.tableLayoutPanelTotalFeeData.ColumnCount = 3;
            this.tableLayoutPanelTotalFeeData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.9003F));
            this.tableLayoutPanelTotalFeeData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.0997F));
            this.tableLayoutPanelTotalFeeData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanelTotalFeeData.Controls.Add(this.textBoxCouponAmt, 2, 1);
            this.tableLayoutPanelTotalFeeData.Controls.Add(this.txtSubTotal, 2, 0);
            this.tableLayoutPanelTotalFeeData.Controls.Add(this.customButtonCoupon, 0, 0);
            this.tableLayoutPanelTotalFeeData.Controls.Add(this.label11, 1, 1);
            this.tableLayoutPanelTotalFeeData.Controls.Add(this.labTaxRate, 1, 2);
            this.tableLayoutPanelTotalFeeData.Controls.Add(this.lblSubTotal, 1, 0);
            this.tableLayoutPanelTotalFeeData.Controls.Add(this.txtTaxAmount, 2, 2);
            this.tableLayoutPanelTotalFeeData.Location = new System.Drawing.Point(428, 8);
            this.tableLayoutPanelTotalFeeData.Name = "tableLayoutPanelTotalFeeData";
            this.tableLayoutPanelTotalFeeData.RowCount = 3;
            this.tableLayoutPanelTotalFeeData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTotalFeeData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTotalFeeData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTotalFeeData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelTotalFeeData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelTotalFeeData.Size = new System.Drawing.Size(423, 83);
            this.tableLayoutPanelTotalFeeData.TabIndex = 163;
            // 
            // textBoxCouponAmt
            // 
            this.textBoxCouponAmt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBoxCouponAmt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxCouponAmt.Enabled = false;
            this.textBoxCouponAmt.Location = new System.Drawing.Point(312, 29);
            this.textBoxCouponAmt.Name = "textBoxCouponAmt";
            this.textBoxCouponAmt.Size = new System.Drawing.Size(85, 20);
            this.textBoxCouponAmt.TabIndex = 150;
            this.textBoxCouponAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSubTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSubTotal.Enabled = false;
            this.txtSubTotal.Location = new System.Drawing.Point(312, 3);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Size = new System.Drawing.Size(85, 20);
            this.txtSubTotal.TabIndex = 150;
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // customButtonCoupon
            // 
            this.customButtonCoupon.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customButtonCoupon.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCoupon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCoupon.BackgroundImage")));
            this.customButtonCoupon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCoupon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCoupon.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCoupon.FlatAppearance.BorderSize = 0;
            this.customButtonCoupon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCoupon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCoupon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCoupon.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCoupon.ForeColor = System.Drawing.Color.White;
            this.customButtonCoupon.Location = new System.Drawing.Point(32, 1);
            this.customButtonCoupon.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCoupon.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCoupon.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCoupon.Name = "customButtonCoupon";
            this.tableLayoutPanelTotalFeeData.SetRowSpan(this.customButtonCoupon, 2);
            this.customButtonCoupon.Size = new System.Drawing.Size(100, 50);
            this.customButtonCoupon.TabIndex = 156;
            this.customButtonCoupon.Text = "Coupon";
            this.customButtonCoupon.UseVisualStyleBackColor = false;
            this.customButtonCoupon.Click += new System.EventHandler(this.customButtonCoupon_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(138, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 26);
            this.label11.TabIndex = 1;
            this.label11.Text = "Transaction Coupon Amount: $";
            // 
            // labTaxRate
            // 
            this.labTaxRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTaxRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTaxRate.Location = new System.Drawing.Point(155, 61);
            this.labTaxRate.Name = "labTaxRate";
            this.labTaxRate.Size = new System.Drawing.Size(151, 13);
            this.labTaxRate.TabIndex = 4;
            this.labTaxRate.Text = "Estimated Tax 8.25: %";
            this.labTaxRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(231, 6);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(75, 13);
            this.lblSubTotal.TabIndex = 1;
            this.lblSubTotal.Text = "Sub Total: $";
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTaxAmount.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTaxAmount.Enabled = false;
            this.txtTaxAmount.Location = new System.Drawing.Point(312, 57);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.Size = new System.Drawing.Size(84, 20);
            this.txtTaxAmount.TabIndex = 151;
            this.txtTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGunMessage
            // 
            this.lblGunMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGunMessage.ForeColor = System.Drawing.Color.Red;
            this.lblGunMessage.Location = new System.Drawing.Point(115, 9);
            this.lblGunMessage.Name = "lblGunMessage";
            this.lblGunMessage.Size = new System.Drawing.Size(757, 17);
            this.lblGunMessage.TabIndex = 4;
            this.lblGunMessage.Text = "Gun Message here.";
            this.lblGunMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGunMessage.Visible = false;
            // 
            // RetailCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.lblGunMessage);
            this.Controls.Add(this.panelTotals);
            this.Controls.Add(this.panelRetailItems);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "RetailCost";
            this.Size = new System.Drawing.Size(908, 485);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RetailCost_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelRetailItems.ResumeLayout(false);
            this.panelRetailItems.PerformLayout();
            this.panelTotals.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelLayawayServiceFee.ResumeLayout(false);
            this.panelLayawayServiceFee.PerformLayout();
            this.panelShippingAndHandling.ResumeLayout(false);
            this.panelShippingAndHandling.PerformLayout();
            this.panelBackgroundCheckFee.ResumeLayout(false);
            this.panelBackgroundCheckFee.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanelTotalFeeData.ResumeLayout(false);
            this.tableLayoutPanelTotalFeeData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelRetailItems;
        private System.Windows.Forms.Panel panelTotals;
        private System.Windows.Forms.Label lblBackgroundCheckFee;
        private System.Windows.Forms.Label lblShippingAndHandling;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblRetailTotal;
        private System.Windows.Forms.TextBox txtShippingAndHandling;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButtonOutTheDoor;
        private System.Windows.Forms.TextBox txtBackgroundCheckFee;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.TextBox txtRetailTotal;
        private System.Windows.Forms.Label lblGunMessage;
        private System.Windows.Forms.TextBox txtLayawayServiceFee;
        private System.Windows.Forms.Label lblLayawayServiceFee;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxCouponAmt;
        private CustomButton customButtonCoupon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTotalFeeData;
        private System.Windows.Forms.Panel panelLayawayServiceFee;
        private System.Windows.Forms.Panel panelBackgroundCheckFee;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panelShippingAndHandling;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label labTaxRate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
