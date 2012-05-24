using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Inquiry.Retail
{
    partial class RetailDetails
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label22;
            System.Windows.Forms.Label label23;
            System.Windows.Forms.Label label25;
            System.Windows.Forms.Label label30;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label20;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetailDetails));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.windowHeading_lb = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTenderTypes = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cust_id = new System.Windows.Forms.Label();
            this.cust_name = new System.Windows.Forms.Label();
            this.cust_since = new System.Windows.Forms.Label();
            this.cust_dob = new System.Windows.Forms.Label();
            this.cust_no = new System.Windows.Forms.Label();
            this.status_lbl = new System.Windows.Forms.Label();
            this.shop_number_lbl = new System.Windows.Forms.Label();
            this.total_sale_amount_lbl = new System.Windows.Forms.Label();
            this.sales_tax_amount_lbl = new System.Windows.Forms.Label();
            this.terminal_id_lbl = new System.Windows.Forms.Label();
            this.cash_drawer_lbl = new System.Windows.Forms.Label();
            this.user_id_lbl = new System.Windows.Forms.Label();
            this.msr_lbl = new System.Windows.Forms.Label();
            this.date_time_lbl = new System.Windows.Forms.Label();
            this.layaway_lbl = new System.Windows.Forms.Label();
            this.pageInd = new CustomLabel();
            this.prevPage = new CustomButtonTiny();
            this.ItemsList_dg = new CustomDataGridView();
            this.Select_btn = new System.Windows.Forms.DataGridViewImageColumn();
            this.ICN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_loan_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstPage = new CustomButtonTiny();
            this.lastPage = new CustomButtonTiny();
            this.Refine_btn = new CustomButton();
            this.nextPage = new CustomButtonTiny();
            this.Print_btn = new CustomButton();
            this.Cancel_btn = new CustomButton();
            this.Back_btn = new CustomButton();
            this.History_dg = new CustomDataGridView();
            this.TransactionNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptNumber = new System.Windows.Forms.DataGridViewLinkColumn();
            this.EventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TranAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            label30 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsList_dg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.History_dg)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(30, 13);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 13);
            label1.TabIndex = 0;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(320, 13);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(68, 13);
            label2.TabIndex = 1;
            label2.Text = "Customer #:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(35, 31);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(32, 13);
            label3.TabIndex = 2;
            label3.Text = "DOB:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(578, 13);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(85, 13);
            label4.TabIndex = 3;
            label4.Text = "Customer Since:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(81, 104);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(47, 13);
            label5.TabIndex = 5;
            label5.Text = "User ID:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(279, 66);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(80, 13);
            label6.TabIndex = 6;
            label6.Text = "Date and Time:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(566, 85);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(97, 13);
            label7.TabIndex = 7;
            label7.Text = "Sales Tax Amount:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(63, 85);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(65, 13);
            label10.TabIndex = 10;
            label10.Text = "Layaway #:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(53, 123);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(73, 13);
            label11.TabIndex = 11;
            label11.Text = "Cash Drawer:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(85, 66);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(43, 13);
            label16.TabIndex = 16;
            label16.Text = "MSR #:";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(532, 66);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(131, 13);
            label22.TabIndex = 22;
            label22.Text = "Total Sale Amount w/Tax:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(587, 104);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(75, 13);
            label23.TabIndex = 23;
            label23.Text = "Shop Number:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new System.Drawing.Point(593, 123);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(42, 13);
            label25.TabIndex = 24;
            label25.Text = "Status:";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new System.Drawing.Point(265, 30);
            label30.Name = "label30";
            label30.Size = new System.Drawing.Size(123, 13);
            label30.TabIndex = 30;
            label30.Text = "Customer Identification:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(282, 104);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(77, 13);
            label15.TabIndex = 33;
            label15.Text = "Tender Types:";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(294, 85);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(65, 13);
            label20.TabIndex = 34;
            label20.Text = "Terminal ID:";
            // 
            // windowHeading_lb
            // 
            this.windowHeading_lb.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.windowHeading_lb.AutoSize = true;
            this.windowHeading_lb.BackColor = System.Drawing.Color.Transparent;
            this.windowHeading_lb.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowHeading_lb.ForeColor = System.Drawing.Color.White;
            this.windowHeading_lb.Location = new System.Drawing.Point(304, 41);
            this.windowHeading_lb.Name = "windowHeading_lb";
            this.windowHeading_lb.Size = new System.Drawing.Size(201, 19);
            this.windowHeading_lb.TabIndex = 28;
            this.windowHeading_lb.Text = "Retail Sale Inquiry - Details";
            this.windowHeading_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.txtTenderTypes);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.cust_id);
            this.panel1.Controls.Add(this.cust_name);
            this.panel1.Controls.Add(this.cust_since);
            this.panel1.Controls.Add(this.cust_dob);
            this.panel1.Controls.Add(this.cust_no);
            this.panel1.Controls.Add(this.status_lbl);
            this.panel1.Controls.Add(this.shop_number_lbl);
            this.panel1.Controls.Add(this.total_sale_amount_lbl);
            this.panel1.Controls.Add(this.sales_tax_amount_lbl);
            this.panel1.Controls.Add(this.terminal_id_lbl);
            this.panel1.Controls.Add(this.cash_drawer_lbl);
            this.panel1.Controls.Add(this.user_id_lbl);
            this.panel1.Controls.Add(this.msr_lbl);
            this.panel1.Controls.Add(this.date_time_lbl);
            this.panel1.Controls.Add(this.layaway_lbl);
            this.panel1.Controls.Add(label20);
            this.panel1.Controls.Add(label15);
            this.panel1.Controls.Add(label30);
            this.panel1.Controls.Add(label25);
            this.panel1.Controls.Add(label23);
            this.panel1.Controls.Add(label22);
            this.panel1.Controls.Add(label16);
            this.panel1.Controls.Add(label11);
            this.panel1.Controls.Add(label10);
            this.panel1.Controls.Add(label7);
            this.panel1.Controls.Add(label6);
            this.panel1.Controls.Add(label5);
            this.panel1.Controls.Add(label4);
            this.panel1.Controls.Add(label3);
            this.panel1.Controls.Add(label2);
            this.panel1.Controls.Add(label1);
            this.panel1.Location = new System.Drawing.Point(13, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 148);
            this.panel1.TabIndex = 30;
            // 
            // txtTenderTypes
            // 
            this.txtTenderTypes.Location = new System.Drawing.Point(373, 104);
            this.txtTenderTypes.Multiline = true;
            this.txtTenderTypes.Name = "txtTenderTypes";
            this.txtTenderTypes.ReadOnly = true;
            this.txtTenderTypes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTenderTypes.Size = new System.Drawing.Size(151, 41);
            this.txtTenderTypes.TabIndex = 88;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(20, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(765, 2);
            this.groupBox2.TabIndex = 87;
            this.groupBox2.TabStop = false;
            // 
            // cust_id
            // 
            this.cust_id.AutoSize = true;
            this.cust_id.Location = new System.Drawing.Point(389, 30);
            this.cust_id.Name = "cust_id";
            this.cust_id.Size = new System.Drawing.Size(37, 13);
            this.cust_id.TabIndex = 68;
            this.cust_id.Text = "12345";
            // 
            // cust_name
            // 
            this.cust_name.AutoSize = true;
            this.cust_name.Location = new System.Drawing.Point(73, 13);
            this.cust_name.Name = "cust_name";
            this.cust_name.Size = new System.Drawing.Size(80, 13);
            this.cust_name.TabIndex = 67;
            this.cust_name.Text = "customer name";
            // 
            // cust_since
            // 
            this.cust_since.AutoSize = true;
            this.cust_since.Location = new System.Drawing.Point(669, 13);
            this.cust_since.Name = "cust_since";
            this.cust_since.Size = new System.Drawing.Size(63, 13);
            this.cust_since.TabIndex = 66;
            this.cust_since.Text = "01/01/1001";
            // 
            // cust_dob
            // 
            this.cust_dob.AutoSize = true;
            this.cust_dob.Location = new System.Drawing.Point(73, 31);
            this.cust_dob.Name = "cust_dob";
            this.cust_dob.Size = new System.Drawing.Size(63, 13);
            this.cust_dob.TabIndex = 65;
            this.cust_dob.Text = "01/01/1001";
            // 
            // cust_no
            // 
            this.cust_no.AutoSize = true;
            this.cust_no.Location = new System.Drawing.Point(389, 13);
            this.cust_no.Name = "cust_no";
            this.cust_no.Size = new System.Drawing.Size(103, 13);
            this.cust_no.TabIndex = 64;
            this.cust_no.Text = "1-000000-000000-0";
            // 
            // status_lbl
            // 
            this.status_lbl.AutoSize = true;
            this.status_lbl.Location = new System.Drawing.Point(669, 123);
            this.status_lbl.Name = "status_lbl";
            this.status_lbl.Size = new System.Drawing.Size(41, 13);
            this.status_lbl.TabIndex = 54;
            this.status_lbl.Text = "$00.00";
            // 
            // shop_number_lbl
            // 
            this.shop_number_lbl.AutoSize = true;
            this.shop_number_lbl.Location = new System.Drawing.Point(669, 104);
            this.shop_number_lbl.Name = "shop_number_lbl";
            this.shop_number_lbl.Size = new System.Drawing.Size(37, 13);
            this.shop_number_lbl.TabIndex = 53;
            this.shop_number_lbl.Text = "12345";
            // 
            // total_sale_amount_lbl
            // 
            this.total_sale_amount_lbl.AutoSize = true;
            this.total_sale_amount_lbl.Location = new System.Drawing.Point(668, 66);
            this.total_sale_amount_lbl.Name = "total_sale_amount_lbl";
            this.total_sale_amount_lbl.Size = new System.Drawing.Size(41, 13);
            this.total_sale_amount_lbl.TabIndex = 52;
            this.total_sale_amount_lbl.Text = "$00.00";
            // 
            // sales_tax_amount_lbl
            // 
            this.sales_tax_amount_lbl.AutoSize = true;
            this.sales_tax_amount_lbl.Location = new System.Drawing.Point(668, 85);
            this.sales_tax_amount_lbl.Name = "sales_tax_amount_lbl";
            this.sales_tax_amount_lbl.Size = new System.Drawing.Size(41, 13);
            this.sales_tax_amount_lbl.TabIndex = 51;
            this.sales_tax_amount_lbl.Text = "$00.00";
            // 
            // terminal_id_lbl
            // 
            this.terminal_id_lbl.AutoSize = true;
            this.terminal_id_lbl.Location = new System.Drawing.Point(370, 85);
            this.terminal_id_lbl.Name = "terminal_id_lbl";
            this.terminal_id_lbl.Size = new System.Drawing.Size(37, 13);
            this.terminal_id_lbl.TabIndex = 49;
            this.terminal_id_lbl.Text = "12345";
            // 
            // cash_drawer_lbl
            // 
            this.cash_drawer_lbl.AutoSize = true;
            this.cash_drawer_lbl.Location = new System.Drawing.Point(134, 123);
            this.cash_drawer_lbl.Name = "cash_drawer_lbl";
            this.cash_drawer_lbl.Size = new System.Drawing.Size(37, 13);
            this.cash_drawer_lbl.TabIndex = 46;
            this.cash_drawer_lbl.Text = "12345";
            // 
            // user_id_lbl
            // 
            this.user_id_lbl.AutoSize = true;
            this.user_id_lbl.Location = new System.Drawing.Point(134, 104);
            this.user_id_lbl.Name = "user_id_lbl";
            this.user_id_lbl.Size = new System.Drawing.Size(75, 13);
            this.user_id_lbl.TabIndex = 44;
            this.user_id_lbl.Text = "ticket_number";
            // 
            // msr_lbl
            // 
            this.msr_lbl.AutoSize = true;
            this.msr_lbl.Location = new System.Drawing.Point(134, 66);
            this.msr_lbl.Name = "msr_lbl";
            this.msr_lbl.Size = new System.Drawing.Size(37, 13);
            this.msr_lbl.TabIndex = 42;
            this.msr_lbl.Text = "12345";
            // 
            // date_time_lbl
            // 
            this.date_time_lbl.AutoSize = true;
            this.date_time_lbl.Location = new System.Drawing.Point(370, 66);
            this.date_time_lbl.Name = "date_time_lbl";
            this.date_time_lbl.Size = new System.Drawing.Size(112, 13);
            this.date_time_lbl.TabIndex = 41;
            this.date_time_lbl.Text = "01/01/1001 00:00 AM";
            // 
            // layaway_lbl
            // 
            this.layaway_lbl.AutoSize = true;
            this.layaway_lbl.Location = new System.Drawing.Point(134, 85);
            this.layaway_lbl.Name = "layaway_lbl";
            this.layaway_lbl.Size = new System.Drawing.Size(75, 13);
            this.layaway_lbl.TabIndex = 40;
            this.layaway_lbl.Text = "ticket_number";
            // 
            // pageInd
            // 
            this.pageInd.AutoSize = true;
            this.pageInd.BackColor = System.Drawing.Color.Transparent;
            this.pageInd.Location = new System.Drawing.Point(563, 247);
            this.pageInd.Name = "pageInd";
            this.pageInd.Size = new System.Drawing.Size(82, 13);
            this.pageInd.TabIndex = 20;
            this.pageInd.Text = "Page {0} of {1}";
            // 
            // prevPage
            // 
            this.prevPage.BackColor = System.Drawing.Color.Transparent;
            this.prevPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("prevPage.BackgroundImage")));
            this.prevPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.prevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevPage.Enabled = false;
            this.prevPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.prevPage.FlatAppearance.BorderSize = 0;
            this.prevPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.prevPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.prevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevPage.ForeColor = System.Drawing.Color.White;
            this.prevPage.Location = new System.Drawing.Point(485, 238);
            this.prevPage.Margin = new System.Windows.Forms.Padding(0);
            this.prevPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.prevPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.prevPage.Name = "prevPage";
            this.prevPage.Size = new System.Drawing.Size(75, 35);
            this.prevPage.TabIndex = 19;
            this.prevPage.Text = "<   ";
            this.prevPage.UseVisualStyleBackColor = false;
            this.prevPage.Click += new System.EventHandler(this.prevPage_Click);
            // 
            // ItemsList_dg
            // 
            this.ItemsList_dg.AllowUserToAddRows = false;
            this.ItemsList_dg.AllowUserToDeleteRows = false;
            this.ItemsList_dg.AllowUserToResizeColumns = false;
            this.ItemsList_dg.AllowUserToResizeRows = false;
            this.ItemsList_dg.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ItemsList_dg.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.ItemsList_dg.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsList_dg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemsList_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsList_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select_btn,
            this.ICN,
            this.Status,
            this.item_desc,
            this.item_loan_amt});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsList_dg.DefaultCellStyle = dataGridViewCellStyle6;
            this.ItemsList_dg.GridColor = System.Drawing.Color.LightGray;
            this.ItemsList_dg.Location = new System.Drawing.Point(13, 273);
            this.ItemsList_dg.Margin = new System.Windows.Forms.Padding(0);
            this.ItemsList_dg.MultiSelect = false;
            this.ItemsList_dg.Name = "ItemsList_dg";
            this.ItemsList_dg.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsList_dg.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.ItemsList_dg.RowHeadersVisible = false;
            this.ItemsList_dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ItemsList_dg.Size = new System.Drawing.Size(803, 121);
            this.ItemsList_dg.TabIndex = 29;
            this.ItemsList_dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsList_dg_CellContentClick);
            // 
            // Select_btn
            // 
            this.Select_btn.FillWeight = 50F;
            this.Select_btn.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.Select_btn.Image = global::Common.Properties.Resources.blueglossy_select2;
            this.Select_btn.MinimumWidth = 55;
            this.Select_btn.Name = "Select_btn";
            this.Select_btn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Select_btn.Width = 55;
            // 
            // ICN
            // 
            this.ICN.DataPropertyName = "ICN";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ICN.DefaultCellStyle = dataGridViewCellStyle2;
            this.ICN.HeaderText = "ICN";
            this.ICN.Name = "ICN";
            this.ICN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ICN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ICN.Width = 130;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "STATUS_CD";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle3;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 75;
            // 
            // item_desc
            // 
            this.item_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_desc.DataPropertyName = "MD_DESC";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.item_desc.DefaultCellStyle = dataGridViewCellStyle4;
            this.item_desc.HeaderText = "Merchandise Description";
            this.item_desc.Name = "item_desc";
            this.item_desc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item_desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item_loan_amt
            // 
            this.item_loan_amt.DataPropertyName = "ITEM_AMT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.item_loan_amt.DefaultCellStyle = dataGridViewCellStyle5;
            this.item_loan_amt.HeaderText = "Item Amount";
            this.item_loan_amt.Name = "item_loan_amt";
            this.item_loan_amt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item_loan_amt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // firstPage
            // 
            this.firstPage.BackColor = System.Drawing.Color.Transparent;
            this.firstPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("firstPage.BackgroundImage")));
            this.firstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.firstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.firstPage.Enabled = false;
            this.firstPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.firstPage.FlatAppearance.BorderSize = 0;
            this.firstPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.firstPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.firstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.firstPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstPage.ForeColor = System.Drawing.Color.White;
            this.firstPage.Location = new System.Drawing.Point(427, 238);
            this.firstPage.Margin = new System.Windows.Forms.Padding(0);
            this.firstPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.firstPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.firstPage.Name = "firstPage";
            this.firstPage.Size = new System.Drawing.Size(75, 35);
            this.firstPage.TabIndex = 22;
            this.firstPage.Text = "<<   ";
            this.firstPage.UseVisualStyleBackColor = false;
            this.firstPage.Click += new System.EventHandler(this.firstPage_Click);
            // 
            // lastPage
            // 
            this.lastPage.BackColor = System.Drawing.Color.Transparent;
            this.lastPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lastPage.BackgroundImage")));
            this.lastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lastPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.lastPage.FlatAppearance.BorderSize = 0;
            this.lastPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lastPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.lastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lastPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastPage.ForeColor = System.Drawing.Color.White;
            this.lastPage.Location = new System.Drawing.Point(724, 238);
            this.lastPage.Margin = new System.Windows.Forms.Padding(0);
            this.lastPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.lastPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.lastPage.Name = "lastPage";
            this.lastPage.Size = new System.Drawing.Size(75, 35);
            this.lastPage.TabIndex = 21;
            this.lastPage.Text = ">>   ";
            this.lastPage.UseVisualStyleBackColor = false;
            this.lastPage.Click += new System.EventHandler(this.lastPage_Click);
            // 
            // Refine_btn
            // 
            this.Refine_btn.BackColor = System.Drawing.Color.Transparent;
            this.Refine_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Refine_btn.BackgroundImage")));
            this.Refine_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Refine_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Refine_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Refine_btn.FlatAppearance.BorderSize = 0;
            this.Refine_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Refine_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Refine_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Refine_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Refine_btn.ForeColor = System.Drawing.Color.White;
            this.Refine_btn.Location = new System.Drawing.Point(116, 551);
            this.Refine_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Refine_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Refine_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Refine_btn.Name = "Refine_btn";
            this.Refine_btn.Size = new System.Drawing.Size(100, 50);
            this.Refine_btn.TabIndex = 27;
            this.Refine_btn.Text = "Refine Search";
            this.Refine_btn.UseVisualStyleBackColor = false;
            this.Refine_btn.Click += new System.EventHandler(this.Refine_btn_Click);
            // 
            // nextPage
            // 
            this.nextPage.BackColor = System.Drawing.Color.Transparent;
            this.nextPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextPage.BackgroundImage")));
            this.nextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nextPage.FlatAppearance.BorderSize = 0;
            this.nextPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.nextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.nextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextPage.ForeColor = System.Drawing.Color.White;
            this.nextPage.Location = new System.Drawing.Point(661, 238);
            this.nextPage.Margin = new System.Windows.Forms.Padding(0);
            this.nextPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.nextPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.nextPage.Name = "nextPage";
            this.nextPage.Size = new System.Drawing.Size(75, 35);
            this.nextPage.TabIndex = 18;
            this.nextPage.Text = ">   ";
            this.nextPage.UseVisualStyleBackColor = false;
            this.nextPage.Click += new System.EventHandler(this.nextPage_Click);
            // 
            // Print_btn
            // 
            this.Print_btn.BackColor = System.Drawing.Color.Transparent;
            this.Print_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Print_btn.BackgroundImage")));
            this.Print_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Print_btn.FlatAppearance.BorderSize = 0;
            this.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Print_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Print_btn.ForeColor = System.Drawing.Color.White;
            this.Print_btn.Location = new System.Drawing.Point(616, 551);
            this.Print_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Print_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.Name = "Print_btn";
            this.Print_btn.Size = new System.Drawing.Size(100, 50);
            this.Print_btn.TabIndex = 24;
            this.Print_btn.Text = "Print";
            this.Print_btn.UseVisualStyleBackColor = false;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.BackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel_btn.BackgroundImage")));
            this.Cancel_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Cancel_btn.FlatAppearance.BorderSize = 0;
            this.Cancel_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_btn.ForeColor = System.Drawing.Color.White;
            this.Cancel_btn.Location = new System.Drawing.Point(716, 551);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 23;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // Back_btn
            // 
            this.Back_btn.BackColor = System.Drawing.Color.Transparent;
            this.Back_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Back_btn.BackgroundImage")));
            this.Back_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Back_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Back_btn.FlatAppearance.BorderSize = 0;
            this.Back_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Back_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Back_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back_btn.ForeColor = System.Drawing.Color.White;
            this.Back_btn.Location = new System.Drawing.Point(16, 551);
            this.Back_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Back_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Back_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(100, 50);
            this.Back_btn.TabIndex = 21;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = false;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // History_dg
            // 
            this.History_dg.AllowUserToAddRows = false;
            this.History_dg.AllowUserToDeleteRows = false;
            this.History_dg.AllowUserToResizeColumns = false;
            this.History_dg.AllowUserToResizeRows = false;
            this.History_dg.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.History_dg.CausesValidation = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.History_dg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.History_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.History_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TransactionNumber,
            this.ReceiptNumber,
            this.EventType,
            this.DateTime,
            this.TranAmount,
            this.SalesTax,
            this.TotalAmount,
            this.UserID});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.History_dg.DefaultCellStyle = dataGridViewCellStyle9;
            this.History_dg.GridColor = System.Drawing.Color.LightGray;
            this.History_dg.Location = new System.Drawing.Point(13, 411);
            this.History_dg.Margin = new System.Windows.Forms.Padding(0);
            this.History_dg.Name = "History_dg";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.History_dg.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.History_dg.RowHeadersVisible = false;
            this.History_dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.History_dg.Size = new System.Drawing.Size(803, 140);
            this.History_dg.TabIndex = 31;
            this.History_dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.History_dg_CellContentClick);
            // 
            // TransactionNumber
            // 
            this.TransactionNumber.DataPropertyName = "RECEIPTDETAIL_NUMBER";
            this.TransactionNumber.HeaderText = "Tran #";
            this.TransactionNumber.Name = "TransactionNumber";
            this.TransactionNumber.ReadOnly = true;
            this.TransactionNumber.Width = 75;
            // 
            // ReceiptNumber
            // 
            this.ReceiptNumber.DataPropertyName = "RECEIPT_NUMBER";
            this.ReceiptNumber.HeaderText = "Receipt #";
            this.ReceiptNumber.Name = "ReceiptNumber";
            this.ReceiptNumber.ReadOnly = true;
            this.ReceiptNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ReceiptNumber.Width = 75;
            // 
            // EventType
            // 
            this.EventType.DataPropertyName = "REF_EVENT";
            this.EventType.HeaderText = "Event Type";
            this.EventType.Name = "EventType";
            this.EventType.ReadOnly = true;
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "REF_TIME";
            this.DateTime.HeaderText = "Date / Time";
            this.DateTime.Name = "DateTime";
            this.DateTime.ReadOnly = true;
            this.DateTime.Width = 150;
            // 
            // TranAmount
            // 
            this.TranAmount.DataPropertyName = "AMOUNT";
            this.TranAmount.HeaderText = "Tran Amount";
            this.TranAmount.Name = "TranAmount";
            this.TranAmount.ReadOnly = true;
            // 
            // SalesTax
            // 
            this.SalesTax.DataPropertyName = "SALES_TAX";
            this.SalesTax.HeaderText = "Sales Tax";
            this.SalesTax.Name = "SalesTax";
            this.SalesTax.ReadOnly = true;
            // 
            // TotalAmount
            // 
            this.TotalAmount.DataPropertyName = "TOTAL_AMOUNT";
            this.TotalAmount.HeaderText = "Total Amount";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "ENT_ID";
            this.UserID.HeaderText = "User ID";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(13, 398);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "History:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(13, 260);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Merchandise:";
            // 
            // RetailDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(860, 620);
            this.ControlBox = false;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.History_dg);
            this.Controls.Add(this.pageInd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.prevPage);
            this.Controls.Add(this.ItemsList_dg);
            this.Controls.Add(this.firstPage);
            this.Controls.Add(this.windowHeading_lb);
            this.Controls.Add(this.lastPage);
            this.Controls.Add(this.Refine_btn);
            this.Controls.Add(this.nextPage);
            this.Controls.Add(this.Print_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Back_btn);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RetailDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoanDetails";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsList_dg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.History_dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton Back_btn;
        private CustomButton Cancel_btn;
        private CustomButton Print_btn;
        private CustomButton Refine_btn;
        private System.Windows.Forms.Label windowHeading_lb;
        private CustomDataGridView ItemsList_dg;
        private System.Windows.Forms.Panel panel1;
        private CustomLabel pageInd;
        private CustomButtonTiny prevPage;
        private CustomButtonTiny firstPage;
        private CustomButtonTiny lastPage;
        private CustomButtonTiny nextPage;
        private System.Windows.Forms.Label layaway_lbl;
        private System.Windows.Forms.Label cust_id;
        private System.Windows.Forms.Label cust_name;
        private System.Windows.Forms.Label cust_since;
        private System.Windows.Forms.Label cust_dob;
        private System.Windows.Forms.Label cust_no;
        private System.Windows.Forms.Label status_lbl;
        private System.Windows.Forms.Label shop_number_lbl;
        private System.Windows.Forms.Label total_sale_amount_lbl;
        private System.Windows.Forms.Label sales_tax_amount_lbl;
        private System.Windows.Forms.Label terminal_id_lbl;
        private System.Windows.Forms.Label cash_drawer_lbl;
        private System.Windows.Forms.Label user_id_lbl;
        private System.Windows.Forms.Label msr_lbl;
        private System.Windows.Forms.Label date_time_lbl;
        private System.Windows.Forms.GroupBox groupBox2;
        private CustomDataGridView History_dg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewImageColumn Select_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_loan_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionNumber;
        private System.Windows.Forms.DataGridViewLinkColumn ReceiptNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.TextBox txtTenderTypes;
    }
}