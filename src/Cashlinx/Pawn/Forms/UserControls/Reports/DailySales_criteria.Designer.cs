using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls.Reports
{
    partial class DailySalesCriteria_panel
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SortBy_cb = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Option_cb = new System.Windows.Forms.ComboBox();
            this.labelAsterisk3 = new System.Windows.Forms.Label();
            this.labelReportStartDate = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.HighVariance_tb = new CustomTextBox();
            this.LowVariance_tb = new CustomTextBox();
            this.HighSalesAmt_tb = new CustomTextBox();
            this.LowSalesAmt_tb = new CustomTextBox();
            this.dateCalendarEnd = new DateCalendar();
            this.dateCalendarStart = new DateCalendar();
            this.labelReportDetail = new System.Windows.Forms.Label();
            this.comboboxReportDetail = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label1.Location = new System.Drawing.Point(21, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Variances %:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label2.Location = new System.Drawing.Point(55, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Sort By:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label3.Location = new System.Drawing.Point(460, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 18);
            this.label3.TabIndex = 21;
            this.label3.Text = "To:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label4.Location = new System.Drawing.Point(460, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "To:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label5.Location = new System.Drawing.Point(14, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 18);
            this.label5.TabIndex = 23;
            this.label5.Text = "Sales Amount:";
            // 
            // SortBy_cb
            // 
            this.SortBy_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SortBy_cb.FormattingEnabled = true;
            this.SortBy_cb.Location = new System.Drawing.Point(143, 110);
            this.SortBy_cb.Name = "SortBy_cb";
            this.SortBy_cb.Size = new System.Drawing.Size(121, 21);
            this.SortBy_cb.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label6.Location = new System.Drawing.Point(436, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 18);
            this.label6.TabIndex = 29;
            this.label6.Text = "Option:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(497, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "*";
            // 
            // Option_cb
            // 
            this.Option_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Option_cb.FormattingEnabled = true;
            this.Option_cb.Location = new System.Drawing.Point(511, 109);
            this.Option_cb.Name = "Option_cb";
            this.Option_cb.Size = new System.Drawing.Size(121, 21);
            this.Option_cb.TabIndex = 31;
            // 
            // labelAsterisk3
            // 
            this.labelAsterisk3.AutoSize = true;
            this.labelAsterisk3.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk3.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk3.Location = new System.Drawing.Point(124, 15);
            this.labelAsterisk3.Name = "labelAsterisk3";
            this.labelAsterisk3.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk3.TabIndex = 34;
            this.labelAsterisk3.Text = "*";
            // 
            // labelReportStartDate
            // 
            this.labelReportStartDate.AutoSize = true;
            this.labelReportStartDate.BackColor = System.Drawing.Color.Transparent;
            this.labelReportStartDate.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportStartDate.Location = new System.Drawing.Point(36, 15);
            this.labelReportStartDate.Name = "labelReportStartDate";
            this.labelReportStartDate.Size = new System.Drawing.Size(80, 18);
            this.labelReportStartDate.TabIndex = 32;
            this.labelReportStartDate.Text = "Start Date:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label8.Location = new System.Drawing.Point(418, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 18);
            this.label8.TabIndex = 36;
            this.label8.Text = "End Date:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(497, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 16);
            this.label9.TabIndex = 37;
            this.label9.Text = "*";
            // 
            // HighVariance_tb
            // 
            this.HighVariance_tb.CausesValidation = false;
            this.HighVariance_tb.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.HighVariance_tb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HighVariance_tb.Location = new System.Drawing.Point(511, 78);
            this.HighVariance_tb.Name = "HighVariance_tb";
            this.HighVariance_tb.Size = new System.Drawing.Size(100, 21);
            this.HighVariance_tb.TabIndex = 27;
            this.HighVariance_tb.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // LowVariance_tb
            // 
            this.LowVariance_tb.CausesValidation = false;
            this.LowVariance_tb.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.LowVariance_tb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LowVariance_tb.Location = new System.Drawing.Point(143, 79);
            this.LowVariance_tb.Name = "LowVariance_tb";
            this.LowVariance_tb.Size = new System.Drawing.Size(100, 21);
            this.LowVariance_tb.TabIndex = 26;
            this.LowVariance_tb.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // HighSalesAmt_tb
            // 
            this.HighSalesAmt_tb.CausesValidation = false;
            this.HighSalesAmt_tb.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.HighSalesAmt_tb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HighSalesAmt_tb.Location = new System.Drawing.Point(511, 50);
            this.HighSalesAmt_tb.Name = "HighSalesAmt_tb";
            this.HighSalesAmt_tb.Size = new System.Drawing.Size(100, 21);
            this.HighSalesAmt_tb.TabIndex = 25;
            this.HighSalesAmt_tb.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // LowSalesAmt_tb
            // 
            this.LowSalesAmt_tb.CausesValidation = false;
            this.LowSalesAmt_tb.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.LowSalesAmt_tb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LowSalesAmt_tb.Location = new System.Drawing.Point(143, 51);
            this.LowSalesAmt_tb.Name = "LowSalesAmt_tb";
            this.LowSalesAmt_tb.Size = new System.Drawing.Size(100, 21);
            this.LowSalesAmt_tb.TabIndex = 24;
            this.LowSalesAmt_tb.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // dateCalendarEnd
            // 
            this.dateCalendarEnd.AllowKeyUpAndDown = false;
            this.dateCalendarEnd.AllowMonthlySelection = false;
            this.dateCalendarEnd.AllowWeekends = true;
            this.dateCalendarEnd.AutoSize = true;
            this.dateCalendarEnd.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarEnd.Location = new System.Drawing.Point(511, 12);
            this.dateCalendarEnd.Name = "dateCalendarEnd";
            this.dateCalendarEnd.PositionPopupCalendarOverTextbox = true;
            this.dateCalendarEnd.SelectedDate = "10/12/2009";
            this.dateCalendarEnd.Size = new System.Drawing.Size(267, 26);
            this.dateCalendarEnd.TabIndex = 35;
            this.dateCalendarEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // dateCalendarStart
            // 
            this.dateCalendarStart.AllowKeyUpAndDown = false;
            this.dateCalendarStart.AllowMonthlySelection = false;
            this.dateCalendarStart.AllowWeekends = true;
            this.dateCalendarStart.AutoSize = true;
            this.dateCalendarStart.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateCalendarStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarStart.Location = new System.Drawing.Point(137, 13);
            this.dateCalendarStart.Name = "dateCalendarStart";
            this.dateCalendarStart.PositionPopupCalendarOverTextbox = true;
            this.dateCalendarStart.SelectedDate = "10/12/2009";
            this.dateCalendarStart.Size = new System.Drawing.Size(137, 26);
            this.dateCalendarStart.TabIndex = 33;
            this.dateCalendarStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // labelReportDetail
            // 
            this.labelReportDetail.AutoSize = true;
            this.labelReportDetail.BackColor = System.Drawing.Color.Transparent;
            this.labelReportDetail.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportDetail.Location = new System.Drawing.Point(238, 149);
            this.labelReportDetail.Name = "labelReportDetail";
            this.labelReportDetail.Size = new System.Drawing.Size(96, 18);
            this.labelReportDetail.TabIndex = 39;
            this.labelReportDetail.Text = "Report Detail:";
            // 
            // comboboxReportDetail
            // 
            this.comboboxReportDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboboxReportDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxReportDetail.FormattingEnabled = true;
            this.comboboxReportDetail.Items.AddRange(new object[] {
            "Detail",
            "Summary"});
            this.comboboxReportDetail.Location = new System.Drawing.Point(353, 149);
            this.comboboxReportDetail.Name = "comboboxReportDetail";
            this.comboboxReportDetail.Size = new System.Drawing.Size(129, 21);
            this.comboboxReportDetail.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(339, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 16);
            this.label10.TabIndex = 41;
            this.label10.Text = "*";
            // 
            // DailySalesCriteria_panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dateCalendarStart);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboboxReportDetail);
            this.Controls.Add(this.labelReportDetail);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dateCalendarEnd);
            this.Controls.Add(this.labelAsterisk3);
            this.Controls.Add(this.labelReportStartDate);
            this.Controls.Add(this.Option_cb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SortBy_cb);
            this.Controls.Add(this.HighVariance_tb);
            this.Controls.Add(this.LowVariance_tb);
            this.Controls.Add(this.HighSalesAmt_tb);
            this.Controls.Add(this.LowSalesAmt_tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(0, 90);
            this.Name = "DailySalesCriteria_panel";
            this.Size = new System.Drawing.Size(800, 190);
            this.VisibleChanged += new System.EventHandler(this.DailySalesCriteria_panel_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelAsterisk3;
        private System.Windows.Forms.Label labelReportStartDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public CustomTextBox LowSalesAmt_tb;
        public CustomTextBox HighSalesAmt_tb;
        public CustomTextBox LowVariance_tb;
        public CustomTextBox HighVariance_tb;
        public System.Windows.Forms.ComboBox SortBy_cb;
        public System.Windows.Forms.ComboBox Option_cb;
        public DateCalendar dateCalendarStart;
        public DateCalendar dateCalendarEnd;
        private System.Windows.Forms.Label labelReportDetail;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox comboboxReportDetail;

    }
}
