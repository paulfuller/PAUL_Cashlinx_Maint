using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;

namespace Pawn.Forms.Inquiry.CashTransferInquiry
{
    partial class CashTransferInquirySearch
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
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashTransferInquirySearch));
            this.label_source_primary = new System.Windows.Forms.Label();
            this.label_dest_primary = new System.Windows.Forms.Label();
            this.label_source_secondary = new System.Windows.Forms.Label();
            this.label_dest_secondary = new System.Windows.Forms.Label();
            this.labelHeading = new System.Windows.Forms.Label();
            this.dateOption_rb = new System.Windows.Forms.RadioButton();
            this.TicketOption_rb = new System.Windows.Forms.RadioButton();
            this.toTicket_tb = new System.Windows.Forms.TextBox();
            this.fromTicket_tb = new System.Windows.Forms.TextBox();
            this.Cancel_btn = new CustomButton();
            this.Find_btn = new CustomButton();
            this.Clear_btn = new CustomButton();
            this.sortBy_cb = new System.Windows.Forms.ComboBox();
            this.sortDir_cb = new System.Windows.Forms.ComboBox();
            this.status_cb = new System.Windows.Forms.ComboBox();
            this.lowLoanAmt_tb = new System.Windows.Forms.TextBox();
            this.highLoanAmt_tb = new System.Windows.Forms.TextBox();
            this.userID_tb = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.transfer_destination_primary_cb = new System.Windows.Forms.ComboBox();
            this.transfer_source_secondary_cb = new System.Windows.Forms.ComboBox();
            this.transfer_source_primary_cb = new System.Windows.Forms.ComboBox();
            this.transfer_destination_secondary_cb = new System.Windows.Forms.ComboBox();
            this.transfer_destination_secondary_txtBox = new System.Windows.Forms.TextBox();
            this.transfer_source_secondary_txtBox = new System.Windows.Forms.TextBox();
            this.transfer_type_cb = new System.Windows.Forms.ComboBox();
            this.dateCalendarEnd = new UserControls.DateCalendar();
            this.dateCalendarStart = new UserControls.DateCalendar();
            labelReportType = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelReportType
            // 
            labelReportType.AutoSize = true;
            labelReportType.BackColor = System.Drawing.Color.Transparent;
            labelReportType.Font = new System.Drawing.Font("Tahoma", 11F);
            labelReportType.Location = new System.Drawing.Point(498, 79);
            labelReportType.Name = "labelReportType";
            labelReportType.Size = new System.Drawing.Size(31, 18);
            labelReportType.TabIndex = 12;
            labelReportType.Text = "To:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Tahoma", 11F);
            label1.Location = new System.Drawing.Point(132, 79);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(105, 18);
            label1.TabIndex = 13;
            label1.Text = "Transfer Date:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Tahoma", 11F);
            label2.Location = new System.Drawing.Point(92, 111);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(145, 18);
            label2.TabIndex = 18;
            label2.Text = "Transfer Number(s):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Tahoma", 11F);
            label3.Location = new System.Drawing.Point(498, 111);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(31, 18);
            label3.TabIndex = 33;
            label3.Text = "To:";
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Tahoma", 11F);
            label4.Location = new System.Drawing.Point(228, 367);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(61, 18);
            label4.TabIndex = 34;
            label4.Text = "Sort By:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Transparent;
            label5.Font = new System.Drawing.Font("Tahoma", 11F);
            label5.Location = new System.Drawing.Point(383, 21);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(31, 18);
            label5.TabIndex = 35;
            label5.Text = "To:";
            // 
            // label7
            // 
            label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Font = new System.Drawing.Font("Tahoma", 11F);
            label7.Location = new System.Drawing.Point(98, 134);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 18);
            label7.TabIndex = 37;
            label7.Text = "UserID:";
            // 
            // label8
            // 
            label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.Color.Transparent;
            label8.Font = new System.Drawing.Font("Tahoma", 11F);
            label8.Location = new System.Drawing.Point(50, 107);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(107, 18);
            label8.TabIndex = 38;
            label8.Text = "Current Status:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Font = new System.Drawing.Font("Tahoma", 11F);
            label9.Location = new System.Drawing.Point(32, 21);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(125, 18);
            label9.TabIndex = 39;
            label9.Text = "Transfer Amount:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Tahoma", 11F);
            label10.Location = new System.Drawing.Point(242, 149);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(108, 18);
            label10.TabIndex = 48;
            label10.Text = "Transfer Type:";
            // 
            // label_source_primary
            // 
            this.label_source_primary.AutoSize = true;
            this.label_source_primary.BackColor = System.Drawing.Color.Transparent;
            this.label_source_primary.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label_source_primary.Location = new System.Drawing.Point(46, 49);
            this.label_source_primary.Name = "label_source_primary";
            this.label_source_primary.Size = new System.Drawing.Size(111, 18);
            this.label_source_primary.TabIndex = 40;
            this.label_source_primary.Text = "Primary Source:";
            // 
            // label_dest_primary
            // 
            this.label_dest_primary.AutoSize = true;
            this.label_dest_primary.BackColor = System.Drawing.Color.Transparent;
            this.label_dest_primary.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label_dest_primary.Location = new System.Drawing.Point(19, 78);
            this.label_dest_primary.Name = "label_dest_primary";
            this.label_dest_primary.Size = new System.Drawing.Size(138, 18);
            this.label_dest_primary.TabIndex = 41;
            this.label_dest_primary.Text = "Primary Destination:";
            // 
            // label_source_secondary
            // 
            this.label_source_secondary.AutoSize = true;
            this.label_source_secondary.BackColor = System.Drawing.Color.Transparent;
            this.label_source_secondary.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label_source_secondary.Location = new System.Drawing.Point(333, 49);
            this.label_source_secondary.Name = "label_source_secondary";
            this.label_source_secondary.Size = new System.Drawing.Size(81, 18);
            this.label_source_secondary.TabIndex = 46;
            this.label_source_secondary.Text = "Secondary:";
            // 
            // label_dest_secondary
            // 
            this.label_dest_secondary.AutoSize = true;
            this.label_dest_secondary.BackColor = System.Drawing.Color.Transparent;
            this.label_dest_secondary.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label_dest_secondary.Location = new System.Drawing.Point(333, 78);
            this.label_dest_secondary.Name = "label_dest_secondary";
            this.label_dest_secondary.Size = new System.Drawing.Size(81, 18);
            this.label_dest_secondary.TabIndex = 47;
            this.label_dest_secondary.Text = "Secondary:";
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(352, 27);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(163, 19);
            this.labelHeading.TabIndex = 1;
            this.labelHeading.Text = "Cash Transfer Inquiry";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateOption_rb
            // 
            this.dateOption_rb.AutoSize = true;
            this.dateOption_rb.BackColor = System.Drawing.Color.Transparent;
            this.dateOption_rb.Checked = true;
            this.dateOption_rb.Location = new System.Drawing.Point(74, 83);
            this.dateOption_rb.Name = "dateOption_rb";
            this.dateOption_rb.Size = new System.Drawing.Size(14, 13);
            this.dateOption_rb.TabIndex = 1;
            this.dateOption_rb.TabStop = true;
            this.dateOption_rb.UseVisualStyleBackColor = false;
            this.dateOption_rb.CheckedChanged += new System.EventHandler(this.dateOption_rb_CheckedChanged);
            // 
            // TicketOption_rb
            // 
            this.TicketOption_rb.AutoSize = true;
            this.TicketOption_rb.BackColor = System.Drawing.Color.Transparent;
            this.TicketOption_rb.Location = new System.Drawing.Point(74, 112);
            this.TicketOption_rb.Name = "TicketOption_rb";
            this.TicketOption_rb.Size = new System.Drawing.Size(14, 13);
            this.TicketOption_rb.TabIndex = 4;
            this.TicketOption_rb.UseVisualStyleBackColor = false;
            this.TicketOption_rb.CheckedChanged += new System.EventHandler(this.TicketOption_rb_CheckedChanged);
            // 
            // toTicket_tb
            // 
            this.toTicket_tb.Enabled = false;
            this.toTicket_tb.Location = new System.Drawing.Point(534, 112);
            this.toTicket_tb.Name = "toTicket_tb";
            this.toTicket_tb.Size = new System.Drawing.Size(87, 21);
            this.toTicket_tb.TabIndex = 6;
            // 
            // fromTicket_tb
            // 
            this.fromTicket_tb.Enabled = false;
            this.fromTicket_tb.Location = new System.Drawing.Point(240, 112);
            this.fromTicket_tb.Name = "fromTicket_tb";
            this.fromTicket_tb.Size = new System.Drawing.Size(87, 21);
            this.fromTicket_tb.TabIndex = 5;
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.Cancel_btn.Location = new System.Drawing.Point(29, 434);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 20;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // Find_btn
            // 
            this.Find_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.Find_btn.Location = new System.Drawing.Point(712, 434);
            this.Find_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Find_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.Name = "Find_btn";
            this.Find_btn.Size = new System.Drawing.Size(100, 50);
            this.Find_btn.TabIndex = 22;
            this.Find_btn.Text = "Find";
            this.Find_btn.UseVisualStyleBackColor = false;
            this.Find_btn.Click += new System.EventHandler(this.Find_btn_Click);
            // 
            // Clear_btn
            // 
            this.Clear_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.Clear_btn.Location = new System.Drawing.Point(591, 434);
            this.Clear_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Clear_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(100, 50);
            this.Clear_btn.TabIndex = 21;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = false;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // sortBy_cb
            // 
            this.sortBy_cb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sortBy_cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.sortBy_cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.sortBy_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sortBy_cb.Cursor = System.Windows.Forms.Cursors.Default;
            this.sortBy_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortBy_cb.ForeColor = System.Drawing.Color.Black;
            this.sortBy_cb.FormattingEnabled = true;
            this.sortBy_cb.Location = new System.Drawing.Point(305, 368);
            this.sortBy_cb.Name = "sortBy_cb";
            this.sortBy_cb.Size = new System.Drawing.Size(144, 21);
            this.sortBy_cb.TabIndex = 18;
            // 
            // sortDir_cb
            // 
            this.sortDir_cb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sortDir_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sortDir_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortDir_cb.ForeColor = System.Drawing.Color.Black;
            this.sortDir_cb.FormattingEnabled = true;
            this.sortDir_cb.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.sortDir_cb.Location = new System.Drawing.Point(466, 368);
            this.sortDir_cb.Name = "sortDir_cb";
            this.sortDir_cb.Size = new System.Drawing.Size(87, 21);
            this.sortDir_cb.TabIndex = 19;
            // 
            // status_cb
            // 
            this.status_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.status_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.status_cb.ForeColor = System.Drawing.Color.Black;
            this.status_cb.FormattingEnabled = true;
            this.status_cb.Location = new System.Drawing.Point(173, 104);
            this.status_cb.Name = "status_cb";
            this.status_cb.Size = new System.Drawing.Size(144, 21);
            this.status_cb.TabIndex = 16;
            // 
            // lowLoanAmt_tb
            // 
            this.lowLoanAmt_tb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lowLoanAmt_tb.Location = new System.Drawing.Point(173, 22);
            this.lowLoanAmt_tb.Name = "lowLoanAmt_tb";
            this.lowLoanAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.lowLoanAmt_tb.TabIndex = 8;
            // 
            // highLoanAmt_tb
            // 
            this.highLoanAmt_tb.Location = new System.Drawing.Point(420, 22);
            this.highLoanAmt_tb.Name = "highLoanAmt_tb";
            this.highLoanAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.highLoanAmt_tb.TabIndex = 9;
            // 
            // userID_tb
            // 
            this.userID_tb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.userID_tb.Location = new System.Drawing.Point(173, 135);
            this.userID_tb.Name = "userID_tb";
            this.userID_tb.Size = new System.Drawing.Size(87, 21);
            this.userID_tb.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label_dest_secondary);
            this.groupBox1.Controls.Add(this.label_source_secondary);
            this.groupBox1.Controls.Add(this.lowLoanAmt_tb);
            this.groupBox1.Controls.Add(this.userID_tb);
            this.groupBox1.Controls.Add(this.transfer_destination_primary_cb);
            this.groupBox1.Controls.Add(this.transfer_source_secondary_cb);
            this.groupBox1.Controls.Add(this.highLoanAmt_tb);
            this.groupBox1.Controls.Add(this.transfer_source_primary_cb);
            this.groupBox1.Controls.Add(label7);
            this.groupBox1.Controls.Add(label8);
            this.groupBox1.Controls.Add(this.label_dest_primary);
            this.groupBox1.Controls.Add(this.label_source_primary);
            this.groupBox1.Controls.Add(label9);
            this.groupBox1.Controls.Add(label5);
            this.groupBox1.Controls.Add(this.status_cb);
            this.groupBox1.Controls.Add(this.transfer_destination_secondary_cb);
            this.groupBox1.Controls.Add(this.transfer_destination_secondary_txtBox);
            this.groupBox1.Controls.Add(this.transfer_source_secondary_txtBox);
            this.groupBox1.Location = new System.Drawing.Point(60, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 174);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional Search Criteria";
            // 
            // transfer_destination_primary_cb
            // 
            this.transfer_destination_primary_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.transfer_destination_primary_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transfer_destination_primary_cb.ForeColor = System.Drawing.Color.Black;
            this.transfer_destination_primary_cb.FormattingEnabled = true;
            this.transfer_destination_primary_cb.Location = new System.Drawing.Point(173, 76);
            this.transfer_destination_primary_cb.Name = "transfer_destination_primary_cb";
            this.transfer_destination_primary_cb.Size = new System.Drawing.Size(144, 21);
            this.transfer_destination_primary_cb.TabIndex = 13;
            this.transfer_destination_primary_cb.SelectedIndexChanged += new System.EventHandler(this.transfer_destination_primary_cb_SelectedIndexChanged);
            // 
            // transfer_source_secondary_cb
            // 
            this.transfer_source_secondary_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.transfer_source_secondary_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transfer_source_secondary_cb.ForeColor = System.Drawing.Color.Black;
            this.transfer_source_secondary_cb.FormattingEnabled = true;
            this.transfer_source_secondary_cb.Location = new System.Drawing.Point(420, 49);
            this.transfer_source_secondary_cb.Name = "transfer_source_secondary_cb";
            this.transfer_source_secondary_cb.Size = new System.Drawing.Size(286, 21);
            this.transfer_source_secondary_cb.TabIndex = 11;
            // 
            // transfer_source_primary_cb
            // 
            this.transfer_source_primary_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.transfer_source_primary_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transfer_source_primary_cb.ForeColor = System.Drawing.Color.Black;
            this.transfer_source_primary_cb.FormattingEnabled = true;
            this.transfer_source_primary_cb.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.transfer_source_primary_cb.Location = new System.Drawing.Point(173, 49);
            this.transfer_source_primary_cb.Name = "transfer_source_primary_cb";
            this.transfer_source_primary_cb.Size = new System.Drawing.Size(144, 21);
            this.transfer_source_primary_cb.TabIndex = 10;
            this.transfer_source_primary_cb.SelectedIndexChanged += new System.EventHandler(this.transfer_source_primary_cb_SelectedIndexChanged);
            // 
            // transfer_destination_secondary_cb
            // 
            this.transfer_destination_secondary_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.transfer_destination_secondary_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transfer_destination_secondary_cb.ForeColor = System.Drawing.Color.Black;
            this.transfer_destination_secondary_cb.FormattingEnabled = true;
            this.transfer_destination_secondary_cb.Location = new System.Drawing.Point(420, 76);
            this.transfer_destination_secondary_cb.Name = "transfer_destination_secondary_cb";
            this.transfer_destination_secondary_cb.Size = new System.Drawing.Size(286, 21);
            this.transfer_destination_secondary_cb.TabIndex = 14;
            // 
            // transfer_destination_secondary_txtBox
            // 
            this.transfer_destination_secondary_txtBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.transfer_destination_secondary_txtBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.transfer_destination_secondary_txtBox.Location = new System.Drawing.Point(420, 76);
            this.transfer_destination_secondary_txtBox.Name = "transfer_destination_secondary_txtBox";
            this.transfer_destination_secondary_txtBox.Size = new System.Drawing.Size(286, 21);
            this.transfer_destination_secondary_txtBox.TabIndex = 15;
            // 
            // transfer_source_secondary_txtBox
            // 
            this.transfer_source_secondary_txtBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.transfer_source_secondary_txtBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.transfer_source_secondary_txtBox.Location = new System.Drawing.Point(420, 49);
            this.transfer_source_secondary_txtBox.Name = "transfer_source_secondary_txtBox";
            this.transfer_source_secondary_txtBox.Size = new System.Drawing.Size(286, 21);
            this.transfer_source_secondary_txtBox.TabIndex = 12;
            // 
            // transfer_type_cb
            // 
            this.transfer_type_cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.transfer_type_cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.transfer_type_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.transfer_type_cb.Cursor = System.Windows.Forms.Cursors.Default;
            this.transfer_type_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transfer_type_cb.ForeColor = System.Drawing.Color.Black;
            this.transfer_type_cb.FormattingEnabled = true;
            this.transfer_type_cb.Location = new System.Drawing.Point(356, 150);
            this.transfer_type_cb.Name = "transfer_type_cb";
            this.transfer_type_cb.Size = new System.Drawing.Size(128, 21);
            this.transfer_type_cb.TabIndex = 7;
            this.transfer_type_cb.SelectedIndexChanged += new System.EventHandler(this.transfer_type_cb_SelectedIndexChanged);
            // 
            // dateCalendarEnd
            // 
            this.dateCalendarEnd.AllowKeyUpAndDown = false;
            this.dateCalendarEnd.AllowMonthlySelection = false;
            this.dateCalendarEnd.AllowWeekends = true;
            this.dateCalendarEnd.AutoSize = true;
            this.dateCalendarEnd.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarEnd.Location = new System.Drawing.Point(535, 77);
            this.dateCalendarEnd.Name = "dateCalendarEnd";
            this.dateCalendarEnd.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarEnd.SelectedDate = "10/12/2009";
            this.dateCalendarEnd.Size = new System.Drawing.Size(231, 27);
            this.dateCalendarEnd.TabIndex = 3;
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
            this.dateCalendarStart.Location = new System.Drawing.Point(240, 77);
            this.dateCalendarStart.Name = "dateCalendarStart";
            this.dateCalendarStart.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarStart.SelectedDate = "10/12/2009";
            this.dateCalendarStart.Size = new System.Drawing.Size(256, 27);
            this.dateCalendarStart.TabIndex = 2;
            this.dateCalendarStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // CashTransferInquirySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.Cancel_btn;
            this.ClientSize = new System.Drawing.Size(853, 508);
            this.ControlBox = false;
            this.Controls.Add(this.dateCalendarEnd);
            this.Controls.Add(this.dateCalendarStart);
            this.Controls.Add(this.transfer_type_cb);
            this.Controls.Add(label10);
            this.Controls.Add(this.sortDir_cb);
            this.Controls.Add(this.sortBy_cb);
            this.Controls.Add(label4);
            this.Controls.Add(labelReportType);
            this.Controls.Add(label3);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.toTicket_tb);
            this.Controls.Add(this.Find_btn);
            this.Controls.Add(this.fromTicket_tb);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(label2);
            this.Controls.Add(this.TicketOption_rb);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(label1);
            this.Controls.Add(this.dateOption_rb);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CashTransferInquirySearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CashTransferInquirySearch";
            this.Load += new System.EventHandler(this.CashTransferInquirySearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private UserControls.DateCalendar dateCalendarStart;
        private UserControls.DateCalendar dateCalendarEnd;
        private System.Windows.Forms.RadioButton dateOption_rb;
        private System.Windows.Forms.RadioButton TicketOption_rb;
        private System.Windows.Forms.TextBox toTicket_tb;
        private System.Windows.Forms.TextBox fromTicket_tb;
        private CustomButton Cancel_btn;
        private CustomButton Find_btn;
        private CustomButton Clear_btn;
        private System.Windows.Forms.ComboBox sortBy_cb;
        private System.Windows.Forms.ComboBox sortDir_cb;
        private System.Windows.Forms.ComboBox status_cb;
        private System.Windows.Forms.TextBox lowLoanAmt_tb;
        private System.Windows.Forms.TextBox highLoanAmt_tb;
        private System.Windows.Forms.TextBox userID_tb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox transfer_destination_secondary_cb;
        private System.Windows.Forms.ComboBox transfer_destination_primary_cb;
        private System.Windows.Forms.ComboBox transfer_source_secondary_cb;
        private System.Windows.Forms.ComboBox transfer_source_primary_cb;
        private System.Windows.Forms.ComboBox transfer_type_cb;
        private System.Windows.Forms.TextBox transfer_destination_secondary_txtBox;
        private System.Windows.Forms.TextBox transfer_source_secondary_txtBox;
        private System.Windows.Forms.Label label_source_secondary;
        private System.Windows.Forms.Label label_dest_secondary;
        private System.Windows.Forms.Label label_source_primary;
        private System.Windows.Forms.Label label_dest_primary;

    }
}