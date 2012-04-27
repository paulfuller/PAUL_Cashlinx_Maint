using CashlinxDesktop.UserControls;
using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    partial class InventoryInquirySearch
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
            System.Windows.Forms.Label labelReportType;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.Label label19;
            System.Windows.Forms.Label label21;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryInquirySearch));
            this.labelHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.highRetailAmt_tb = new System.Windows.Forms.TextBox();
            this.lowRetailAmt_tb = new System.Windows.Forms.TextBox();
            this.highCost_tb = new System.Windows.Forms.TextBox();
            this.lowCost_tb = new System.Windows.Forms.TextBox();
            this.sortDir_cb = new System.Windows.Forms.ComboBox();
            this.sortBy_cb = new System.Windows.Forms.ComboBox();
            this.Clear_btn = new CustomButton();
            this.Find_btn = new CustomButton();
            this.Cancel_btn = new CustomButton();
            this.descr_tb = new System.Windows.Forms.TextBox();
            this.manuf_tb = new System.Windows.Forms.TextBox();
            this.model_tb = new System.Windows.Forms.TextBox();
            this.serialNr_tb = new System.Windows.Forms.TextBox();
            this.aisle_tb = new System.Windows.Forms.TextBox();
            this.shelf_tb = new System.Windows.Forms.TextBox();
            this.loctn_tb = new System.Windows.Forms.TextBox();
            this.rfb_tb = new System.Windows.Forms.TextBox();
            this.gunNbr_tb = new System.Windows.Forms.TextBox();
            this.ICN_shop_tb = new System.Windows.Forms.TextBox();
            this.ICN_year_tb = new System.Windows.Forms.TextBox();
            this.ICN_doc_tb = new System.Windows.Forms.TextBox();
            this.ICN_doc_type_tb = new System.Windows.Forms.TextBox();
            this.ICN_item_tb = new System.Windows.Forms.TextBox();
            this.CategoriesBtn = new CustomButton();
            this.invAge_tb = new System.Windows.Forms.TextBox();
            this.ICN_sub_item_tb = new System.Windows.Forms.TextBox();
            this.status_cb = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.Doc_nr_tb = new System.Windows.Forms.TextBox();
            this.fieldCost_tb = new System.Windows.Forms.TextBox();
            this.dateCalendarStart = new DateCalendar();
            this.dateCalendarEnd = new DateCalendar();
            labelReportType = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelReportType
            // 
            labelReportType.AutoSize = true;
            labelReportType.BackColor = System.Drawing.Color.Transparent;
            labelReportType.Font = new System.Drawing.Font("Tahoma", 11F);
            labelReportType.Location = new System.Drawing.Point(566, 176);
            labelReportType.Name = "labelReportType";
            labelReportType.Size = new System.Drawing.Size(31, 18);
            labelReportType.TabIndex = 15;
            labelReportType.Text = "To:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Font = new System.Drawing.Font("Tahoma", 11F);
            label9.Location = new System.Drawing.Point(72, 236);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(82, 18);
            label9.TabIndex = 47;
            label9.Text = "Retail Price:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Tahoma", 11F);
            label8.Location = new System.Drawing.Point(280, 236);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(31, 18);
            label8.TabIndex = 46;
            label8.Text = "To:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Tahoma", 11F);
            label10.Location = new System.Drawing.Point(75, 259);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(79, 18);
            label10.TabIndex = 51;
            label10.Text = "Item Cost:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = System.Drawing.Color.Transparent;
            label11.Font = new System.Drawing.Font("Tahoma", 11F);
            label11.Location = new System.Drawing.Point(280, 259);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(31, 18);
            label11.TabIndex = 50;
            label11.Text = "To:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.Color.Transparent;
            label12.Font = new System.Drawing.Font("Tahoma", 11F);
            label12.Location = new System.Drawing.Point(239, 464);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(61, 18);
            label12.TabIndex = 57;
            label12.Text = "Sort By:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = System.Drawing.Color.Transparent;
            label13.Font = new System.Drawing.Font("Tahoma", 11F);
            label13.Location = new System.Drawing.Point(71, 305);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(83, 18);
            label13.TabIndex = 60;
            label13.Text = "Description:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = System.Drawing.Color.Transparent;
            label14.Font = new System.Drawing.Font("Tahoma", 11F);
            label14.Location = new System.Drawing.Point(54, 328);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(100, 18);
            label14.TabIndex = 62;
            label14.Text = "Manufacturer:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = System.Drawing.Color.Transparent;
            label15.Font = new System.Drawing.Font("Tahoma", 11F);
            label15.Location = new System.Drawing.Point(103, 352);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(51, 18);
            label15.TabIndex = 64;
            label15.Text = "Model:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = System.Drawing.Color.Transparent;
            label16.Font = new System.Drawing.Font("Tahoma", 11F);
            label16.Location = new System.Drawing.Point(51, 374);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(103, 18);
            label16.TabIndex = 66;
            label16.Text = "Serial Number:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = System.Drawing.Color.Transparent;
            label17.Font = new System.Drawing.Font("Tahoma", 11F);
            label17.Location = new System.Drawing.Point(116, 417);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(41, 18);
            label17.TabIndex = 68;
            label17.Text = "Aisle:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = System.Drawing.Color.Transparent;
            label18.Font = new System.Drawing.Font("Tahoma", 11F);
            label18.Location = new System.Drawing.Point(269, 417);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(44, 18);
            label18.TabIndex = 70;
            label18.Text = "Shelf:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.BackColor = System.Drawing.Color.Transparent;
            label19.Font = new System.Drawing.Font("Tahoma", 11F);
            label19.Location = new System.Drawing.Point(430, 417);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(50, 18);
            label19.TabIndex = 72;
            label19.Text = "Other:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = System.Drawing.Color.Transparent;
            label21.Font = new System.Drawing.Font("Tahoma", 11F);
            label21.Location = new System.Drawing.Point(522, 236);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(75, 18);
            label21.TabIndex = 88;
            label21.Text = "Field Cost:";
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(431, 43);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(133, 19);
            this.labelHeading.TabIndex = 2;
            this.labelHeading.Text = "Inventory Inquiry";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "ICN:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(360, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "RFB: #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(526, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Merchandise Gun Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(39, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Category:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(54, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Status:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(236, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Status Date: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Inventory Age Over:";
            // 
            // highRetailAmt_tb
            // 
            this.highRetailAmt_tb.Location = new System.Drawing.Point(328, 236);
            this.highRetailAmt_tb.Name = "highRetailAmt_tb";
            this.highRetailAmt_tb.Size = new System.Drawing.Size(87, 20);
            this.highRetailAmt_tb.TabIndex = 15;
            // 
            // lowRetailAmt_tb
            // 
            this.lowRetailAmt_tb.Location = new System.Drawing.Point(167, 236);
            this.lowRetailAmt_tb.Name = "lowRetailAmt_tb";
            this.lowRetailAmt_tb.Size = new System.Drawing.Size(87, 20);
            this.lowRetailAmt_tb.TabIndex = 14;
            // 
            // highCost_tb
            // 
            this.highCost_tb.Location = new System.Drawing.Point(328, 259);
            this.highCost_tb.Name = "highCost_tb";
            this.highCost_tb.Size = new System.Drawing.Size(87, 20);
            this.highCost_tb.TabIndex = 18;
            // 
            // lowCost_tb
            // 
            this.lowCost_tb.Location = new System.Drawing.Point(167, 259);
            this.lowCost_tb.Name = "lowCost_tb";
            this.lowCost_tb.Size = new System.Drawing.Size(87, 20);
            this.lowCost_tb.TabIndex = 17;
            // 
            // sortDir_cb
            // 
            this.sortDir_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sortDir_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortDir_cb.ForeColor = System.Drawing.Color.Black;
            this.sortDir_cb.FormattingEnabled = true;
            this.sortDir_cb.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.sortDir_cb.Location = new System.Drawing.Point(477, 465);
            this.sortDir_cb.Name = "sortDir_cb";
            this.sortDir_cb.Size = new System.Drawing.Size(87, 21);
            this.sortDir_cb.TabIndex = 27;
            // 
            // sortBy_cb
            // 
            this.sortBy_cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.sortBy_cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.sortBy_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sortBy_cb.Cursor = System.Windows.Forms.Cursors.Default;
            this.sortBy_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortBy_cb.ForeColor = System.Drawing.Color.Black;
            this.sortBy_cb.FormattingEnabled = true;
            this.sortBy_cb.Location = new System.Drawing.Point(316, 465);
            this.sortBy_cb.Name = "sortBy_cb";
            this.sortBy_cb.Size = new System.Drawing.Size(144, 21);
            this.sortBy_cb.TabIndex = 26;
            // 
            // Clear_btn
            // 
            this.Clear_btn.BackColor = System.Drawing.Color.Transparent;
            this.Clear_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Clear_btn.BackgroundImage")));
            this.Clear_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Clear_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Clear_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Clear_btn.FlatAppearance.BorderSize = 0;
            this.Clear_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Clear_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Clear_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clear_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clear_btn.ForeColor = System.Drawing.Color.White;
            this.Clear_btn.Location = new System.Drawing.Point(731, 506);
            this.Clear_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Clear_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(100, 50);
            this.Clear_btn.TabIndex = 56;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = false;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // Find_btn
            // 
            this.Find_btn.BackColor = System.Drawing.Color.Transparent;
            this.Find_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Find_btn.BackgroundImage")));
            this.Find_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Find_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Find_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Find_btn.FlatAppearance.BorderSize = 0;
            this.Find_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Find_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Find_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Find_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Find_btn.ForeColor = System.Drawing.Color.White;
            this.Find_btn.Location = new System.Drawing.Point(852, 506);
            this.Find_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Find_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.Name = "Find_btn";
            this.Find_btn.Size = new System.Drawing.Size(100, 50);
            this.Find_btn.TabIndex = 28;
            this.Find_btn.Text = "Find";
            this.Find_btn.UseVisualStyleBackColor = false;
            this.Find_btn.Click += new System.EventHandler(this.Find_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.BackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel_btn.BackgroundImage")));
            this.Cancel_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Cancel_btn.FlatAppearance.BorderSize = 0;
            this.Cancel_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_btn.ForeColor = System.Drawing.Color.White;
            this.Cancel_btn.Location = new System.Drawing.Point(39, 506);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 54;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // descr_tb
            // 
            this.descr_tb.Location = new System.Drawing.Point(167, 306);
            this.descr_tb.Name = "descr_tb";
            this.descr_tb.Size = new System.Drawing.Size(592, 20);
            this.descr_tb.TabIndex = 19;
            // 
            // manuf_tb
            // 
            this.manuf_tb.Location = new System.Drawing.Point(167, 329);
            this.manuf_tb.MaxLength = 30;
            this.manuf_tb.Name = "manuf_tb";
            this.manuf_tb.Size = new System.Drawing.Size(265, 20);
            this.manuf_tb.TabIndex = 20;
            // 
            // model_tb
            // 
            this.model_tb.Location = new System.Drawing.Point(167, 352);
            this.model_tb.MaxLength = 30;
            this.model_tb.Name = "model_tb";
            this.model_tb.Size = new System.Drawing.Size(265, 20);
            this.model_tb.TabIndex = 21;
            // 
            // serialNr_tb
            // 
            this.serialNr_tb.Location = new System.Drawing.Point(167, 375);
            this.serialNr_tb.MaxLength = 30;
            this.serialNr_tb.Name = "serialNr_tb";
            this.serialNr_tb.Size = new System.Drawing.Size(265, 20);
            this.serialNr_tb.TabIndex = 22;
            // 
            // aisle_tb
            // 
            this.aisle_tb.Location = new System.Drawing.Point(162, 417);
            this.aisle_tb.MaxLength = 4;
            this.aisle_tb.Name = "aisle_tb";
            this.aisle_tb.Size = new System.Drawing.Size(72, 20);
            this.aisle_tb.TabIndex = 23;
            // 
            // shelf_tb
            // 
            this.shelf_tb.Location = new System.Drawing.Point(322, 417);
            this.shelf_tb.MaxLength = 4;
            this.shelf_tb.Name = "shelf_tb";
            this.shelf_tb.Size = new System.Drawing.Size(72, 20);
            this.shelf_tb.TabIndex = 24;
            // 
            // loctn_tb
            // 
            this.loctn_tb.Location = new System.Drawing.Point(487, 417);
            this.loctn_tb.MaxLength = 50;
            this.loctn_tb.Name = "loctn_tb";
            this.loctn_tb.Size = new System.Drawing.Size(272, 20);
            this.loctn_tb.TabIndex = 25;
            // 
            // rfb_tb
            // 
            this.rfb_tb.Location = new System.Drawing.Point(420, 93);
            this.rfb_tb.MaxLength = 4;
            this.rfb_tb.Name = "rfb_tb";
            this.rfb_tb.Size = new System.Drawing.Size(72, 20);
            this.rfb_tb.TabIndex = 7;
            // 
            // gunNbr_tb
            // 
            this.gunNbr_tb.Location = new System.Drawing.Point(714, 94);
            this.gunNbr_tb.MaxLength = 9;
            this.gunNbr_tb.Name = "gunNbr_tb";
            this.gunNbr_tb.Size = new System.Drawing.Size(72, 20);
            this.gunNbr_tb.TabIndex = 8;
            // 
            // ICN_shop_tb
            // 
            this.ICN_shop_tb.Location = new System.Drawing.Point(110, 95);
            this.ICN_shop_tb.MaxLength = 5;
            this.ICN_shop_tb.Name = "ICN_shop_tb";
            this.ICN_shop_tb.Size = new System.Drawing.Size(37, 20);
            this.ICN_shop_tb.TabIndex = 1;
            // 
            // ICN_year_tb
            // 
            this.ICN_year_tb.Location = new System.Drawing.Point(155, 95);
            this.ICN_year_tb.MaxLength = 1;
            this.ICN_year_tb.Name = "ICN_year_tb";
            this.ICN_year_tb.Size = new System.Drawing.Size(19, 20);
            this.ICN_year_tb.TabIndex = 2;
            // 
            // ICN_doc_tb
            // 
            this.ICN_doc_tb.Location = new System.Drawing.Point(180, 95);
            this.ICN_doc_tb.MaxLength = 6;
            this.ICN_doc_tb.Name = "ICN_doc_tb";
            this.ICN_doc_tb.Size = new System.Drawing.Size(47, 20);
            this.ICN_doc_tb.TabIndex = 3;
            this.ICN_doc_tb.Leave += new System.EventHandler(this.ICN_doc_tb_Leave);
            // 
            // ICN_doc_type_tb
            // 
            this.ICN_doc_type_tb.Location = new System.Drawing.Point(235, 95);
            this.ICN_doc_type_tb.MaxLength = 1;
            this.ICN_doc_type_tb.Name = "ICN_doc_type_tb";
            this.ICN_doc_type_tb.Size = new System.Drawing.Size(19, 20);
            this.ICN_doc_type_tb.TabIndex = 4;
            // 
            // ICN_item_tb
            // 
            this.ICN_item_tb.Location = new System.Drawing.Point(261, 95);
            this.ICN_item_tb.MaxLength = 3;
            this.ICN_item_tb.Name = "ICN_item_tb";
            this.ICN_item_tb.Size = new System.Drawing.Size(27, 20);
            this.ICN_item_tb.TabIndex = 5;
            // 
            // CategoriesBtn
            // 
            this.CategoriesBtn.BackColor = System.Drawing.Color.Transparent;
            this.CategoriesBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CategoriesBtn.BackgroundImage")));
            this.CategoriesBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CategoriesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CategoriesBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CategoriesBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CategoriesBtn.FlatAppearance.BorderSize = 0;
            this.CategoriesBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CategoriesBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CategoriesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoriesBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoriesBtn.ForeColor = System.Drawing.Color.White;
            this.CategoriesBtn.Location = new System.Drawing.Point(114, 118);
            this.CategoriesBtn.Margin = new System.Windows.Forms.Padding(0);
            this.CategoriesBtn.MaximumSize = new System.Drawing.Size(100, 50);
            this.CategoriesBtn.MinimumSize = new System.Drawing.Size(100, 50);
            this.CategoriesBtn.Name = "CategoriesBtn";
            this.CategoriesBtn.Size = new System.Drawing.Size(100, 50);
            this.CategoriesBtn.TabIndex = 81;
            this.CategoriesBtn.Text = "Category Select";
            this.CategoriesBtn.UseVisualStyleBackColor = false;
            this.CategoriesBtn.Click += new System.EventHandler(this.CategoriesBtn_Click);
            // 
            // invAge_tb
            // 
            this.invAge_tb.Location = new System.Drawing.Point(169, 202);
            this.invAge_tb.Name = "invAge_tb";
            this.invAge_tb.Size = new System.Drawing.Size(72, 20);
            this.invAge_tb.TabIndex = 13;
            // 
            // ICN_sub_item_tb
            // 
            this.ICN_sub_item_tb.Location = new System.Drawing.Point(294, 95);
            this.ICN_sub_item_tb.MaxLength = 2;
            this.ICN_sub_item_tb.Name = "ICN_sub_item_tb";
            this.ICN_sub_item_tb.Size = new System.Drawing.Size(22, 20);
            this.ICN_sub_item_tb.TabIndex = 6;
            // 
            // status_cb
            // 
            this.status_cb.FormattingEnabled = true;
            this.status_cb.ItemHeight = 13;
            this.status_cb.Location = new System.Drawing.Point(106, 171);
            this.status_cb.Name = "status_cb";
            this.status_cb.Size = new System.Drawing.Size(121, 21);
            this.status_cb.TabIndex = 85;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(807, 94);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 18);
            this.label20.TabIndex = 86;
            this.label20.Text = "Doc #:";
            // 
            // Doc_nr_tb
            // 
            this.Doc_nr_tb.Location = new System.Drawing.Point(869, 94);
            this.Doc_nr_tb.Name = "Doc_nr_tb";
            this.Doc_nr_tb.Size = new System.Drawing.Size(72, 20);
            this.Doc_nr_tb.TabIndex = 9;
            this.Doc_nr_tb.Leave += new System.EventHandler(this.Doc_nr_tb_Leave);
            // 
            // fieldCost_tb
            // 
            this.fieldCost_tb.Location = new System.Drawing.Point(603, 236);
            this.fieldCost_tb.Name = "fieldCost_tb";
            this.fieldCost_tb.Size = new System.Drawing.Size(87, 20);
            this.fieldCost_tb.TabIndex = 16;
            // 
            // dateCalendarStart
            // 
            this.dateCalendarStart.AllowKeyUpAndDown = false;
            this.dateCalendarStart.AllowMonthlySelection = false;
            this.dateCalendarStart.AllowWeekends = true;
            this.dateCalendarStart.AutoSize = true;
            this.dateCalendarStart.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarStart.Location = new System.Drawing.Point(326, 171);
            this.dateCalendarStart.Name = "dateCalendarStart";
            this.dateCalendarStart.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarStart.SelectedDate = "10/12/2009";
            this.dateCalendarStart.Size = new System.Drawing.Size(244, 27);
            this.dateCalendarStart.TabIndex = 11;
            this.dateCalendarStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // dateCalendarEnd
            // 
            this.dateCalendarEnd.AllowKeyUpAndDown = false;
            this.dateCalendarEnd.AllowMonthlySelection = false;
            this.dateCalendarEnd.AllowWeekends = true;
            this.dateCalendarEnd.AutoSize = true;
            this.dateCalendarEnd.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarEnd.Location = new System.Drawing.Point(597, 172);
            this.dateCalendarEnd.Name = "dateCalendarEnd";
            this.dateCalendarEnd.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarEnd.SelectedDate = "10/12/2009";
            this.dateCalendarEnd.Size = new System.Drawing.Size(231, 27);
            this.dateCalendarEnd.TabIndex = 12;
            this.dateCalendarEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // InventoryInquirySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.Cancel_btn;
            this.ClientSize = new System.Drawing.Size(1008, 582);
            this.ControlBox = false;
            this.Controls.Add(this.fieldCost_tb);
            this.Controls.Add(label21);
            this.Controls.Add(this.Doc_nr_tb);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.status_cb);
            this.Controls.Add(this.ICN_sub_item_tb);
            this.Controls.Add(this.invAge_tb);
            this.Controls.Add(this.CategoriesBtn);
            this.Controls.Add(this.ICN_item_tb);
            this.Controls.Add(this.ICN_doc_type_tb);
            this.Controls.Add(this.ICN_doc_tb);
            this.Controls.Add(this.ICN_year_tb);
            this.Controls.Add(this.ICN_shop_tb);
            this.Controls.Add(this.gunNbr_tb);
            this.Controls.Add(this.rfb_tb);
            this.Controls.Add(this.loctn_tb);
            this.Controls.Add(label19);
            this.Controls.Add(this.shelf_tb);
            this.Controls.Add(label18);
            this.Controls.Add(this.aisle_tb);
            this.Controls.Add(label17);
            this.Controls.Add(this.serialNr_tb);
            this.Controls.Add(label16);
            this.Controls.Add(this.model_tb);
            this.Controls.Add(label15);
            this.Controls.Add(this.manuf_tb);
            this.Controls.Add(label14);
            this.Controls.Add(this.descr_tb);
            this.Controls.Add(label13);
            this.Controls.Add(this.sortDir_cb);
            this.Controls.Add(this.sortBy_cb);
            this.Controls.Add(label12);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.Find_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.highCost_tb);
            this.Controls.Add(this.lowCost_tb);
            this.Controls.Add(label10);
            this.Controls.Add(label11);
            this.Controls.Add(this.highRetailAmt_tb);
            this.Controls.Add(this.lowRetailAmt_tb);
            this.Controls.Add(label9);
            this.Controls.Add(label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateCalendarStart);
            this.Controls.Add(this.dateCalendarEnd);
            this.Controls.Add(labelReportType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InventoryInquirySearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryInquirySearch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryInquirySearch_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DateCalendar dateCalendarStart;
        private DateCalendar dateCalendarEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox highRetailAmt_tb;
        private System.Windows.Forms.TextBox lowRetailAmt_tb;
        private System.Windows.Forms.TextBox highCost_tb;
        private System.Windows.Forms.TextBox lowCost_tb;
        private System.Windows.Forms.ComboBox sortDir_cb;
        private System.Windows.Forms.ComboBox sortBy_cb;
        private System.Windows.Forms.TextBox descr_tb;
        private System.Windows.Forms.TextBox manuf_tb;
        private System.Windows.Forms.TextBox model_tb;
        private System.Windows.Forms.TextBox serialNr_tb;
        private System.Windows.Forms.TextBox aisle_tb;
        private System.Windows.Forms.TextBox shelf_tb;
        private System.Windows.Forms.TextBox loctn_tb;
        private System.Windows.Forms.TextBox rfb_tb;
        private System.Windows.Forms.TextBox gunNbr_tb;
        private System.Windows.Forms.TextBox ICN_shop_tb;
        private System.Windows.Forms.TextBox ICN_year_tb;
        private System.Windows.Forms.TextBox ICN_doc_tb;
        private System.Windows.Forms.TextBox ICN_doc_type_tb;
        private System.Windows.Forms.TextBox ICN_item_tb;
        private System.Windows.Forms.TextBox invAge_tb;
        private System.Windows.Forms.TextBox ICN_sub_item_tb;
        private System.Windows.Forms.ComboBox status_cb;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox Doc_nr_tb;
        private System.Windows.Forms.TextBox fieldCost_tb;
        private CustomButton Clear_btn;
        private CustomButton Find_btn;
        private CustomButton Cancel_btn;
        private CustomButton CategoriesBtn;
    }
}
