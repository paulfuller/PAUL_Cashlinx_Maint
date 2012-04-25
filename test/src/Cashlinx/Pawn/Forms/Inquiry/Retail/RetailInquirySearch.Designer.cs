using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Inquiry.Retail
{
    partial class RetailInquirySearch
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetailInquirySearch));
            this.labelHeading = new System.Windows.Forms.Label();
            this.dateOption_rb = new System.Windows.Forms.RadioButton();
            this.MSROption_rb = new System.Windows.Forms.RadioButton();
            this.toMSR_tb = new System.Windows.Forms.TextBox();
            this.fromMSR_tb = new System.Windows.Forms.TextBox();
            this.Cancel_btn = new CustomButton();
            this.Find_btn = new CustomButton();
            this.Clear_btn = new CustomButton();
            this.sortBy_cb = new System.Windows.Forms.ComboBox();
            this.sortDir_cb = new System.Windows.Forms.ComboBox();
            this.status_cb = new System.Windows.Forms.ComboBox();
            this.layawayOriginated_cb = new System.Windows.Forms.ComboBox();
            this.dateCalendarEnd = new DateCalendar();
            this.dateCalendarStart = new DateCalendar();
            this.lowSaleAmt_tb = new System.Windows.Forms.TextBox();
            this.highSaleAmt_tb = new System.Windows.Forms.TextBox();
            this.userID_tb = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.includeVoids_cb = new System.Windows.Forms.ComboBox();
            this.highCostAmt_tb = new System.Windows.Forms.TextBox();
            this.lowCostAmt_tb = new System.Windows.Forms.TextBox();
            labelReportType = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelReportType
            // 
            labelReportType.AutoSize = true;
            labelReportType.BackColor = System.Drawing.Color.Transparent;
            labelReportType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            labelReportType.Location = new System.Drawing.Point(299, 82);
            labelReportType.Name = "labelReportType";
            labelReportType.Size = new System.Drawing.Size(23, 13);
            labelReportType.TabIndex = 12;
            labelReportType.Text = "To:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label1.Location = new System.Drawing.Point(66, 85);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 13);
            label1.TabIndex = 13;
            label1.Text = "Date:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label2.Location = new System.Drawing.Point(58, 111);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(43, 13);
            label2.TabIndex = 18;
            label2.Text = "MSR #:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label3.Location = new System.Drawing.Point(299, 108);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(23, 13);
            label3.TabIndex = 33;
            label3.Text = "To:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label4.Location = new System.Drawing.Point(251, 384);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(46, 13);
            label4.TabIndex = 34;
            label4.Text = "Sort By:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label5.Location = new System.Drawing.Point(435, 178);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(23, 13);
            label5.TabIndex = 35;
            label5.Text = "To:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label6.Location = new System.Drawing.Point(92, 85);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(132, 13);
            label6.TabIndex = 36;
            label6.Text = "Originated from Layaway:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label7.Location = new System.Drawing.Point(177, 143);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(47, 13);
            label7.TabIndex = 37;
            label7.Text = "User ID:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label8.Location = new System.Drawing.Point(142, 114);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(82, 13);
            label8.TabIndex = 38;
            label8.Text = "Current Status:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label9.Location = new System.Drawing.Point(226, 178);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(71, 13);
            label9.TabIndex = 39;
            label9.Text = "Sale Amount:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label10.Location = new System.Drawing.Point(191, 53);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(33, 13);
            label10.TabIndex = 47;
            label10.Text = "Cost:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = System.Drawing.Color.Transparent;
            label11.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label11.Location = new System.Drawing.Point(362, 54);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(23, 13);
            label11.TabIndex = 46;
            label11.Text = "To:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.Color.Transparent;
            label12.Font = new System.Drawing.Font("Tahoma", 8.25F);
            label12.Location = new System.Drawing.Point(150, 172);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(74, 13);
            label12.TabIndex = 50;
            label12.Text = "Include Voids:";
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(336, 27);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(138, 19);
            this.labelHeading.TabIndex = 1;
            this.labelHeading.Text = "Retail Sale Inquiry";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateOption_rb
            // 
            this.dateOption_rb.AutoSize = true;
            this.dateOption_rb.BackColor = System.Drawing.Color.Transparent;
            this.dateOption_rb.Checked = true;
            this.dateOption_rb.Location = new System.Drawing.Point(38, 86);
            this.dateOption_rb.Name = "dateOption_rb";
            this.dateOption_rb.Size = new System.Drawing.Size(14, 13);
            this.dateOption_rb.TabIndex = 1;
            this.dateOption_rb.TabStop = true;
            this.dateOption_rb.UseVisualStyleBackColor = false;
            this.dateOption_rb.CheckedChanged += new System.EventHandler(this.dateOption_rb_CheckedChanged);
            // 
            // MSROption_rb
            // 
            this.MSROption_rb.AutoSize = true;
            this.MSROption_rb.BackColor = System.Drawing.Color.Transparent;
            this.MSROption_rb.Location = new System.Drawing.Point(38, 112);
            this.MSROption_rb.Name = "MSROption_rb";
            this.MSROption_rb.Size = new System.Drawing.Size(14, 13);
            this.MSROption_rb.TabIndex = 2;
            this.MSROption_rb.UseVisualStyleBackColor = false;
            // 
            // toMSR_tb
            // 
            this.toMSR_tb.Enabled = false;
            this.toMSR_tb.Location = new System.Drawing.Point(326, 108);
            this.toMSR_tb.Name = "toMSR_tb";
            this.toMSR_tb.Size = new System.Drawing.Size(123, 21);
            this.toMSR_tb.TabIndex = 6;
            // 
            // fromMSR_tb
            // 
            this.fromMSR_tb.Enabled = false;
            this.fromMSR_tb.Location = new System.Drawing.Point(107, 108);
            this.fromMSR_tb.Name = "fromMSR_tb";
            this.fromMSR_tb.Size = new System.Drawing.Size(129, 21);
            this.fromMSR_tb.TabIndex = 5;
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
            this.Cancel_btn.Location = new System.Drawing.Point(27, 411);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 19;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
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
            this.Find_btn.Location = new System.Drawing.Point(701, 411);
            this.Find_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Find_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.Name = "Find_btn";
            this.Find_btn.Size = new System.Drawing.Size(100, 50);
            this.Find_btn.TabIndex = 17;
            this.Find_btn.Text = "Find";
            this.Find_btn.UseVisualStyleBackColor = false;
            this.Find_btn.Click += new System.EventHandler(this.Find_btn_Click);
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
            this.Clear_btn.Location = new System.Drawing.Point(589, 411);
            this.Clear_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Clear_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(100, 50);
            this.Clear_btn.TabIndex = 18;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = false;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
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
            this.sortBy_cb.Location = new System.Drawing.Point(305, 381);
            this.sortBy_cb.Name = "sortBy_cb";
            this.sortBy_cb.Size = new System.Drawing.Size(144, 21);
            this.sortBy_cb.TabIndex = 15;
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
            this.sortDir_cb.Location = new System.Drawing.Point(466, 381);
            this.sortDir_cb.Name = "sortDir_cb";
            this.sortDir_cb.Size = new System.Drawing.Size(87, 21);
            this.sortDir_cb.TabIndex = 16;
            // 
            // status_cb
            // 
            this.status_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.status_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.status_cb.ForeColor = System.Drawing.Color.Black;
            this.status_cb.FormattingEnabled = true;
            this.status_cb.Location = new System.Drawing.Point(232, 111);
            this.status_cb.Name = "status_cb";
            this.status_cb.Size = new System.Drawing.Size(144, 21);
            this.status_cb.TabIndex = 12;
            // 
            // layawayOriginated_cb
            // 
            this.layawayOriginated_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.layawayOriginated_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.layawayOriginated_cb.ForeColor = System.Drawing.Color.Black;
            this.layawayOriginated_cb.FormattingEnabled = true;
            this.layawayOriginated_cb.Items.AddRange(new object[] {
            "Yes",
            "No",
            global::Pawn.Properties.Resources.OverrideMachineName});
            this.layawayOriginated_cb.Location = new System.Drawing.Point(232, 82);
            this.layawayOriginated_cb.Name = "layawayOriginated_cb";
            this.layawayOriginated_cb.Size = new System.Drawing.Size(144, 21);
            this.layawayOriginated_cb.TabIndex = 11;
            // 
            // dateCalendarEnd
            // 
            this.dateCalendarEnd.AllowKeyUpAndDown = false;
            this.dateCalendarEnd.AllowMonthlySelection = false;
            this.dateCalendarEnd.AllowWeekends = true;
            this.dateCalendarEnd.AutoSize = true;
            this.dateCalendarEnd.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarEnd.Location = new System.Drawing.Point(322, 77);
            this.dateCalendarEnd.Name = "dateCalendarEnd";
            this.dateCalendarEnd.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarEnd.SelectedDate = "10/12/2009";
            this.dateCalendarEnd.Size = new System.Drawing.Size(231, 27);
            this.dateCalendarEnd.TabIndex = 4;
            this.dateCalendarEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // dateCalendarStart
            // 
            this.dateCalendarStart.AllowKeyUpAndDown = false;
            this.dateCalendarStart.AllowMonthlySelection = false;
            this.dateCalendarStart.AllowWeekends = true;
            this.dateCalendarStart.AutoSize = true;
            this.dateCalendarStart.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarStart.Location = new System.Drawing.Point(107, 77);
            this.dateCalendarStart.Name = "dateCalendarStart";
            this.dateCalendarStart.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarStart.SelectedDate = "10/12/2009";
            this.dateCalendarStart.Size = new System.Drawing.Size(145, 27);
            this.dateCalendarStart.TabIndex = 3;
            this.dateCalendarStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lowSaleAmt_tb
            // 
            this.lowSaleAmt_tb.Location = new System.Drawing.Point(305, 174);
            this.lowSaleAmt_tb.Name = "lowSaleAmt_tb";
            this.lowSaleAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.lowSaleAmt_tb.TabIndex = 7;
            // 
            // highSaleAmt_tb
            // 
            this.highSaleAmt_tb.Location = new System.Drawing.Point(466, 175);
            this.highSaleAmt_tb.Name = "highSaleAmt_tb";
            this.highSaleAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.highSaleAmt_tb.TabIndex = 8;
            // 
            // userID_tb
            // 
            this.userID_tb.Location = new System.Drawing.Point(232, 140);
            this.userID_tb.Name = "userID_tb";
            this.userID_tb.Size = new System.Drawing.Size(87, 21);
            this.userID_tb.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.includeVoids_cb);
            this.groupBox1.Controls.Add(label12);
            this.groupBox1.Controls.Add(this.highCostAmt_tb);
            this.groupBox1.Controls.Add(this.userID_tb);
            this.groupBox1.Controls.Add(this.lowCostAmt_tb);
            this.groupBox1.Controls.Add(this.layawayOriginated_cb);
            this.groupBox1.Controls.Add(label10);
            this.groupBox1.Controls.Add(label11);
            this.groupBox1.Controls.Add(label7);
            this.groupBox1.Controls.Add(this.status_cb);
            this.groupBox1.Controls.Add(label8);
            this.groupBox1.Controls.Add(label6);
            this.groupBox1.Location = new System.Drawing.Point(73, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 207);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional Search Criteria";
            // 
            // includeVoids_cb
            // 
            this.includeVoids_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.includeVoids_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.includeVoids_cb.ForeColor = System.Drawing.Color.Black;
            this.includeVoids_cb.FormattingEnabled = true;
            this.includeVoids_cb.Items.AddRange(new object[] {
            "Yes",
            "No",
            global::Pawn.Properties.Resources.OverrideMachineName});
            this.includeVoids_cb.Location = new System.Drawing.Point(232, 169);
            this.includeVoids_cb.Name = "includeVoids_cb";
            this.includeVoids_cb.Size = new System.Drawing.Size(144, 21);
            this.includeVoids_cb.TabIndex = 14;
            // 
            // highCostAmt_tb
            // 
            this.highCostAmt_tb.Location = new System.Drawing.Point(393, 51);
            this.highCostAmt_tb.Name = "highCostAmt_tb";
            this.highCostAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.highCostAmt_tb.TabIndex = 10;
            // 
            // lowCostAmt_tb
            // 
            this.lowCostAmt_tb.Location = new System.Drawing.Point(232, 50);
            this.lowCostAmt_tb.Name = "lowCostAmt_tb";
            this.lowCostAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.lowCostAmt_tb.TabIndex = 9;
            // 
            // RetailInquirySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.Cancel_btn;
            this.ClientSize = new System.Drawing.Size(820, 490);
            this.ControlBox = false;
            this.Controls.Add(this.dateCalendarStart);
            this.Controls.Add(this.highSaleAmt_tb);
            this.Controls.Add(this.lowSaleAmt_tb);
            this.Controls.Add(this.sortDir_cb);
            this.Controls.Add(this.sortBy_cb);
            this.Controls.Add(label9);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(this.dateCalendarEnd);
            this.Controls.Add(labelReportType);
            this.Controls.Add(label3);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.toMSR_tb);
            this.Controls.Add(this.Find_btn);
            this.Controls.Add(this.fromMSR_tb);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(label2);
            this.Controls.Add(this.MSROption_rb);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(label1);
            this.Controls.Add(this.dateOption_rb);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RetailInquirySearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoanInquirySearch";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private DateCalendar dateCalendarStart;
        private DateCalendar dateCalendarEnd;
        private System.Windows.Forms.RadioButton dateOption_rb;
        private System.Windows.Forms.RadioButton MSROption_rb;
        private System.Windows.Forms.TextBox toMSR_tb;
        private System.Windows.Forms.TextBox fromMSR_tb;
        private CustomButton Cancel_btn;
        private CustomButton Find_btn;
        private CustomButton Clear_btn;
        private System.Windows.Forms.ComboBox sortBy_cb;
        private System.Windows.Forms.ComboBox sortDir_cb;
        private System.Windows.Forms.ComboBox status_cb;
        private System.Windows.Forms.ComboBox layawayOriginated_cb;
        private System.Windows.Forms.TextBox lowSaleAmt_tb;
        private System.Windows.Forms.TextBox highSaleAmt_tb;
        private System.Windows.Forms.TextBox userID_tb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox includeVoids_cb;
        private System.Windows.Forms.TextBox highCostAmt_tb;
        private System.Windows.Forms.TextBox lowCostAmt_tb;

    }
}