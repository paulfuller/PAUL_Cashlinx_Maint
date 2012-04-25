using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Inquiry.PartialPaymentInquiry
{
    partial class PartialPaymentInquirySearch
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label labelReportType;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartialPaymentInquirySearch));
            this.sortDir_cb = new System.Windows.Forms.ComboBox();
            this.sortBy_cb = new System.Windows.Forms.ComboBox();
            this.toAmount_tb = new System.Windows.Forms.TextBox();
            this.Clear_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.Find_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.Cancel_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.fromAmount_tb = new System.Windows.Forms.TextBox();
            this.labelHeading = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtCustomerNumber = new System.Windows.Forms.TextBox();
            this.txtLoanTicketNumber = new System.Windows.Forms.TextBox();
            this.dateCalendarStart = new DateCalendar();
            this.dateCalendarEnd = new DateCalendar();
            this.txtDOB = new Date();
            label4 = new System.Windows.Forms.Label();
            labelReportType = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Tahoma", 11F);
            label4.Location = new System.Drawing.Point(228, 296);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(61, 18);
            label4.TabIndex = 66;
            label4.Text = "Sort By:";
            // 
            // labelReportType
            // 
            labelReportType.AutoSize = true;
            labelReportType.BackColor = System.Drawing.Color.Transparent;
            labelReportType.Font = new System.Drawing.Font("Tahoma", 11F);
            labelReportType.Location = new System.Drawing.Point(498, 79);
            labelReportType.Name = "labelReportType";
            labelReportType.Size = new System.Drawing.Size(31, 18);
            labelReportType.TabIndex = 57;
            labelReportType.Text = "To:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Tahoma", 11F);
            label3.Location = new System.Drawing.Point(498, 114);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(31, 18);
            label3.TabIndex = 65;
            label3.Text = "To:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Tahoma", 11F);
            label2.Location = new System.Drawing.Point(84, 114);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(189, 18);
            label2.TabIndex = 59;
            label2.Text = "Partial Payment Amount(s):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Tahoma", 11F);
            label1.Location = new System.Drawing.Point(84, 79);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(150, 18);
            label1.TabIndex = 58;
            label1.Text = "Partial Payment Date:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Tahoma", 11F);
            label6.Location = new System.Drawing.Point(84, 149);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(152, 18);
            label6.TabIndex = 70;
            label6.Text = "Customer Last Name:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Tahoma", 11F);
            label10.Location = new System.Drawing.Point(453, 149);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(152, 18);
            label10.TabIndex = 71;
            label10.Text = "Customer First Name:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = System.Drawing.Color.Transparent;
            label11.Font = new System.Drawing.Font("Tahoma", 11F);
            label11.Location = new System.Drawing.Point(343, 187);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(96, 18);
            label11.TabIndex = 73;
            label11.Text = "Date of Birth:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.Color.Transparent;
            label12.Font = new System.Drawing.Font("Tahoma", 11F);
            label12.Location = new System.Drawing.Point(306, 221);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(133, 18);
            label12.TabIndex = 75;
            label12.Text = "Customer Number:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = System.Drawing.Color.Transparent;
            label13.Font = new System.Drawing.Font("Tahoma", 11F);
            label13.Location = new System.Drawing.Point(294, 254);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(145, 18);
            label13.TabIndex = 77;
            label13.Text = "Loan Ticket Number:";
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
            this.sortDir_cb.Location = new System.Drawing.Point(466, 297);
            this.sortDir_cb.Name = "sortDir_cb";
            this.sortDir_cb.Size = new System.Drawing.Size(87, 21);
            this.sortDir_cb.TabIndex = 11;
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
            this.sortBy_cb.Location = new System.Drawing.Point(305, 297);
            this.sortBy_cb.Name = "sortBy_cb";
            this.sortBy_cb.Size = new System.Drawing.Size(144, 21);
            this.sortBy_cb.TabIndex = 10;
            // 
            // toAmount_tb
            // 
            this.toAmount_tb.Location = new System.Drawing.Point(535, 112);
            this.toAmount_tb.Name = "toAmount_tb";
            this.toAmount_tb.Size = new System.Drawing.Size(87, 21);
            this.toAmount_tb.TabIndex = 4;
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
            this.Clear_btn.Location = new System.Drawing.Point(591, 324);
            this.Clear_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Clear_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(100, 50);
            this.Clear_btn.TabIndex = 13;
            this.Clear_btn.Text = "Clear";
            this.Clear_btn.UseVisualStyleBackColor = false;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
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
            this.Find_btn.Location = new System.Drawing.Point(712, 324);
            this.Find_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Find_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Find_btn.Name = "Find_btn";
            this.Find_btn.Size = new System.Drawing.Size(100, 50);
            this.Find_btn.TabIndex = 14;
            this.Find_btn.Text = "Find";
            this.Find_btn.UseVisualStyleBackColor = false;
            this.Find_btn.Click += new System.EventHandler(this.Find_btn_Click);
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
            this.Cancel_btn.Location = new System.Drawing.Point(29, 324);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 12;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // fromAmount_tb
            // 
            this.fromAmount_tb.Location = new System.Drawing.Point(279, 112);
            this.fromAmount_tb.Name = "fromAmount_tb";
            this.fromAmount_tb.Size = new System.Drawing.Size(87, 21);
            this.fromAmount_tb.TabIndex = 3;
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
            this.labelHeading.Size = new System.Drawing.Size(175, 19);
            this.labelHeading.TabIndex = 49;
            this.labelHeading.Text = "Partial Payment Inquiry";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(611, 148);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(185, 21);
            this.txtFirstName.TabIndex = 6;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(242, 148);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(205, 21);
            this.txtLastName.TabIndex = 5;
            // 
            // txtCustomerNumber
            // 
            this.txtCustomerNumber.Location = new System.Drawing.Point(466, 220);
            this.txtCustomerNumber.Name = "txtCustomerNumber";
            this.txtCustomerNumber.Size = new System.Drawing.Size(123, 21);
            this.txtCustomerNumber.TabIndex = 8;
            // 
            // txtLoanTicketNumber
            // 
            this.txtLoanTicketNumber.Location = new System.Drawing.Point(466, 254);
            this.txtLoanTicketNumber.Name = "txtLoanTicketNumber";
            this.txtLoanTicketNumber.Size = new System.Drawing.Size(123, 21);
            this.txtLoanTicketNumber.TabIndex = 9;
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
            this.dateCalendarStart.TabIndex = 1;
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
            this.dateCalendarEnd.Location = new System.Drawing.Point(535, 77);
            this.dateCalendarEnd.Name = "dateCalendarEnd";
            this.dateCalendarEnd.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarEnd.SelectedDate = "10/12/2009";
            this.dateCalendarEnd.Size = new System.Drawing.Size(231, 27);
            this.dateCalendarEnd.TabIndex = 2;
            this.dateCalendarEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtDOB
            // 
            this.txtDOB.BackColor = System.Drawing.Color.Transparent;
            this.txtDOB.CausesValidation = false;
            this.txtDOB.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtDOB.ForeColor = System.Drawing.Color.Black;
            this.txtDOB.Location = new System.Drawing.Point(467, 185);
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.Size = new System.Drawing.Size(100, 20);
            this.txtDOB.TabIndex = 7;
            this.txtDOB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDOB.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]19[0-9][0-9]|20[0" +
                "-9][0-9]$";
            this.txtDOB.Leave += new System.EventHandler(this.txtDOB_Leave);
            // 
            // PartialPaymentInquirySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.Cancel_btn;
            this.ClientSize = new System.Drawing.Size(853, 398);
            this.ControlBox = false;
            this.Controls.Add(this.dateCalendarEnd);
            this.Controls.Add(this.txtDOB);
            this.Controls.Add(this.dateCalendarStart);
            this.Controls.Add(this.txtLoanTicketNumber);
            this.Controls.Add(label13);
            this.Controls.Add(this.txtCustomerNumber);
            this.Controls.Add(label12);
            this.Controls.Add(label11);
            this.Controls.Add(label10);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(label6);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.sortDir_cb);
            this.Controls.Add(this.sortBy_cb);
            this.Controls.Add(label4);
            this.Controls.Add(labelReportType);
            this.Controls.Add(this.toAmount_tb);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.Find_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.fromAmount_tb);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PartialPaymentInquirySearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PartialPaymentInquirySearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateCalendar dateCalendarStart;
        private System.Windows.Forms.ComboBox sortDir_cb;
        private System.Windows.Forms.ComboBox sortBy_cb;
        private DateCalendar dateCalendarEnd;
        private System.Windows.Forms.TextBox toAmount_tb;
        private CustomButton Clear_btn;
        private CustomButton Find_btn;
        private CustomButton Cancel_btn;
        private System.Windows.Forms.TextBox fromAmount_tb;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtCustomerNumber;
        private System.Windows.Forms.TextBox txtLoanTicketNumber;
        private Date txtDOB;

    }
}