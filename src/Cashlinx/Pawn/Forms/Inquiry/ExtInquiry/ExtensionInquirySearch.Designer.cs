using CashlinxDesktop.UserControls;
using Common.Libraries.Forms.Components;
using Pawn.Forms.Inquiry.LoanInquiry;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Inquiry.ExtInquiry

{
    partial class ExtensionInquirySearch
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
            System.Windows.Forms.Label label9;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanInquirySearch));
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
            this.dateCalendarEnd = new DateCalendar();
            this.dateCalendarStart = new DateCalendar();
            this.lowLoanAmt_tb = new System.Windows.Forms.TextBox();
            this.highLoanAmt_tb = new System.Windows.Forms.TextBox();
            this.userID_tb = new System.Windows.Forms.TextBox();
            labelReportType = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelReportType
            // 
            labelReportType.AutoSize = true;
            labelReportType.BackColor = System.Drawing.Color.Transparent;
            labelReportType.Font = new System.Drawing.Font("Tahoma", 11F);
            labelReportType.Location = new System.Drawing.Point(394, 83);
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
            label1.Location = new System.Drawing.Point(60, 83);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 18);
            label1.TabIndex = 13;
            label1.Text = "Date:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Tahoma", 11F);
            label2.Location = new System.Drawing.Point(60, 109);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(128, 18);
            label2.TabIndex = 18;
            label2.Text = "Ticket Number(s):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Tahoma", 11F);
            label3.Location = new System.Drawing.Point(422, 110);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(31, 18);
            label3.TabIndex = 33;
            label3.Text = "To:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Tahoma", 11F);
            label4.Location = new System.Drawing.Point(126, 245);
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
            label5.Location = new System.Drawing.Point(318, 159);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(31, 18);
            label5.TabIndex = 35;
            label5.Text = "To:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.Color.Transparent;
            label7.Font = new System.Drawing.Font("Tahoma", 11F);
            label7.Location = new System.Drawing.Point(130, 195);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 18);
            label7.TabIndex = 37;
            label7.Text = "UserID:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Font = new System.Drawing.Font("Tahoma", 11F);
            label9.Location = new System.Drawing.Point(60, 160);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(100, 18);
            label9.TabIndex = 39;
            label9.Text = "Extension Amount:";
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(269, 27);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(215, 19);
            this.labelHeading.TabIndex = 1;
            this.labelHeading.Text = "Pawn Loan Extension Inquiry";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateOption_rb
            // 
            this.dateOption_rb.AutoSize = true;
            this.dateOption_rb.BackColor = System.Drawing.Color.Transparent;
            this.dateOption_rb.Checked = true;
            this.dateOption_rb.Location = new System.Drawing.Point(29, 87);
            this.dateOption_rb.Name = "dateOption_rb";
            this.dateOption_rb.Size = new System.Drawing.Size(14, 13);
            this.dateOption_rb.TabIndex = 16;
            this.dateOption_rb.TabStop = true;
            this.dateOption_rb.UseVisualStyleBackColor = false;
            this.dateOption_rb.CheckedChanged += new System.EventHandler(this.dateOption_rb_CheckedChanged);
            // 
            // TicketOption_rb
            // 
            this.TicketOption_rb.AutoSize = true;
            this.TicketOption_rb.BackColor = System.Drawing.Color.Transparent;
            this.TicketOption_rb.Location = new System.Drawing.Point(29, 112);
            this.TicketOption_rb.Name = "TicketOption_rb";
            this.TicketOption_rb.Size = new System.Drawing.Size(14, 13);
            this.TicketOption_rb.TabIndex = 17;
            this.TicketOption_rb.UseVisualStyleBackColor = false;
            this.TicketOption_rb.CheckedChanged += new System.EventHandler(this.TicketOption_rb_CheckedChanged);
            // 
            // toTicket_tb
            // 
            this.toTicket_tb.Enabled = false;
            this.toTicket_tb.Location = new System.Drawing.Point(458, 110);
            this.toTicket_tb.Name = "toTicket_tb";
            this.toTicket_tb.Size = new System.Drawing.Size(87, 21);
            this.toTicket_tb.TabIndex = 32;
            // 
            // fromTicket_tb
            // 
            this.fromTicket_tb.Enabled = false;
            this.fromTicket_tb.Location = new System.Drawing.Point(190, 110);
            this.fromTicket_tb.Name = "fromTicket_tb";
            this.fromTicket_tb.Size = new System.Drawing.Size(87, 21);
            this.fromTicket_tb.TabIndex = 31;
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
            this.Cancel_btn.Location = new System.Drawing.Point(28, 308);
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
            this.Find_btn.Location = new System.Drawing.Point(535, 308);
            this.Find_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Find_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.Name = "Find_btn";
            this.Find_btn.Size = new System.Drawing.Size(100, 50);
            this.Find_btn.TabIndex = 20;
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
            this.Clear_btn.Location = new System.Drawing.Point(414, 308);
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
            this.sortBy_cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.sortBy_cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.sortBy_cb.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sortBy_cb.Cursor = System.Windows.Forms.Cursors.Default;
            this.sortBy_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortBy_cb.ForeColor = System.Drawing.Color.Black;
            this.sortBy_cb.FormattingEnabled = true;
            this.sortBy_cb.Location = new System.Drawing.Point(203, 246);
            this.sortBy_cb.Name = "sortBy_cb";
            this.sortBy_cb.Size = new System.Drawing.Size(144, 21);
            this.sortBy_cb.TabIndex = 40;
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
            this.sortDir_cb.Location = new System.Drawing.Point(364, 246);
            this.sortDir_cb.Name = "sortDir_cb";
            this.sortDir_cb.Size = new System.Drawing.Size(87, 21);
            this.sortDir_cb.TabIndex = 41;
            // 
            // dateCalendarEnd
            // 
            this.dateCalendarEnd.AllowKeyUpAndDown = false;
            this.dateCalendarEnd.AllowMonthlySelection = false;
            this.dateCalendarEnd.AllowWeekends = true;
            this.dateCalendarEnd.AutoSize = true;
            this.dateCalendarEnd.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarEnd.Location = new System.Drawing.Point(427, 79);
            this.dateCalendarEnd.Name = "dateCalendarEnd";
            this.dateCalendarEnd.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarEnd.SelectedDate = "10/12/2009";
            this.dateCalendarEnd.Size = new System.Drawing.Size(231, 27);
            this.dateCalendarEnd.TabIndex = 11;
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
            this.dateCalendarStart.Location = new System.Drawing.Point(120, 79);
            this.dateCalendarStart.Name = "dateCalendarStart";
            this.dateCalendarStart.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarStart.SelectedDate = "10/12/2009";
            this.dateCalendarStart.Size = new System.Drawing.Size(256, 27);
            this.dateCalendarStart.TabIndex = 10;
            this.dateCalendarStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lowLoanAmt_tb
            // 
            this.lowLoanAmt_tb.Location = new System.Drawing.Point(205, 160);
            this.lowLoanAmt_tb.Name = "lowLoanAmt_tb";
            this.lowLoanAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.lowLoanAmt_tb.TabIndex = 44;
            // 
            // highLoanAmt_tb
            // 
            this.highLoanAmt_tb.Location = new System.Drawing.Point(366, 161);
            this.highLoanAmt_tb.Name = "highLoanAmt_tb";
            this.highLoanAmt_tb.Size = new System.Drawing.Size(87, 21);
            this.highLoanAmt_tb.TabIndex = 45;
            // 
            // userID_tb
            // 
            this.userID_tb.Location = new System.Drawing.Point(205, 196);
            this.userID_tb.Name = "userID_tb";
            this.userID_tb.Size = new System.Drawing.Size(87, 21);
            this.userID_tb.TabIndex = 46;
            // 
            // LoanInquirySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(686, 377);
            this.ControlBox = false;
            this.Controls.Add(this.dateCalendarStart);
            this.Controls.Add(this.userID_tb);
            this.Controls.Add(this.highLoanAmt_tb);
            this.Controls.Add(this.lowLoanAmt_tb);
            this.Controls.Add(this.sortDir_cb);
            this.Controls.Add(this.sortBy_cb);
            this.Controls.Add(label9);
            this.Controls.Add(label7);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(this.dateCalendarEnd);
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
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoanInquirySearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoanInquirySearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private DateCalendar dateCalendarStart;
        private DateCalendar dateCalendarEnd;
        private System.Windows.Forms.RadioButton dateOption_rb;
        private System.Windows.Forms.RadioButton TicketOption_rb;
        private System.Windows.Forms.TextBox toTicket_tb;
        private System.Windows.Forms.TextBox fromTicket_tb;
        private CustomButton Cancel_btn;
        private CustomButton Find_btn;
        private CustomButton Clear_btn;
        private System.Windows.Forms.ComboBox sortBy_cb;
        private System.Windows.Forms.ComboBox sortDir_cb;
        private System.Windows.Forms.TextBox lowLoanAmt_tb;
        private System.Windows.Forms.TextBox highLoanAmt_tb;
        private System.Windows.Forms.TextBox userID_tb;

    }
}