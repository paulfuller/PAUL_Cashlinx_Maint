using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Services.PFI
{
    partial class PFI_Mailers
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PFI_Mailers));
            this.rdoPrintPFIMailers = new System.Windows.Forms.RadioButton();
            this.rdoReprintMailers = new System.Windows.Forms.RadioButton();
            this.rdoReprintMailersForTickets = new System.Windows.Forms.RadioButton();
            this.rdoReprintMailbook = new System.Windows.Forms.RadioButton();
            this.lblPFIMailerDate = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtEndTicket = new System.Windows.Forms.TextBox();
            this.txtBeginTicket = new System.Windows.Forms.TextBox();
            this.dateReprintMailers = new DateCalendar();
            this.dateReprintMailbook = new DateCalendar();
            this.continueButton = new Common.Libraries.Forms.Components.CustomButton();
            this.cancelButton = new Common.Libraries.Forms.Components.CustomButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTicketRange = new DateCalendar();
            this.SuspendLayout();
            // 
            // rdoPrintPFIMailers
            // 
            this.rdoPrintPFIMailers.BackColor = System.Drawing.Color.Transparent;
            this.rdoPrintPFIMailers.Location = new System.Drawing.Point(62, 76);
            this.rdoPrintPFIMailers.Name = "rdoPrintPFIMailers";
            this.rdoPrintPFIMailers.Size = new System.Drawing.Size(138, 24);
            this.rdoPrintPFIMailers.TabIndex = 1;
            this.rdoPrintPFIMailers.TabStop = true;
            this.rdoPrintPFIMailers.Text = "Print PFI Mailers As Of:";
            this.rdoPrintPFIMailers.UseVisualStyleBackColor = false;
            this.rdoPrintPFIMailers.CheckedChanged += new System.EventHandler(this.rdoPrintPFIMailers_CheckedChanged);
            // 
            // rdoReprintMailers
            // 
            this.rdoReprintMailers.BackColor = System.Drawing.Color.Transparent;
            this.rdoReprintMailers.Location = new System.Drawing.Point(62, 106);
            this.rdoReprintMailers.Name = "rdoReprintMailers";
            this.rdoReprintMailers.Size = new System.Drawing.Size(172, 24);
            this.rdoReprintMailers.TabIndex = 2;
            this.rdoReprintMailers.TabStop = true;
            this.rdoReprintMailers.Text = "Reprint PFI Mailers Printed On:";
            this.rdoReprintMailers.UseVisualStyleBackColor = false;
            this.rdoReprintMailers.CheckedChanged += new System.EventHandler(this.rdoReprintMailers_CheckedChanged);
            // 
            // rdoReprintMailersForTickets
            // 
            this.rdoReprintMailersForTickets.BackColor = System.Drawing.Color.Transparent;
            this.rdoReprintMailersForTickets.Location = new System.Drawing.Point(62, 136);
            this.rdoReprintMailersForTickets.Name = "rdoReprintMailersForTickets";
            this.rdoReprintMailersForTickets.Size = new System.Drawing.Size(211, 24);
            this.rdoReprintMailersForTickets.TabIndex = 4;
            this.rdoReprintMailersForTickets.TabStop = true;
            this.rdoReprintMailersForTickets.Text = "Reprint PFI Mailers for Ticket Numbers:";
            this.rdoReprintMailersForTickets.UseVisualStyleBackColor = false;
            this.rdoReprintMailersForTickets.CheckedChanged += new System.EventHandler(this.rdoReprintMailersForTickets_CheckedChanged);
            // 
            // rdoReprintMailbook
            // 
            this.rdoReprintMailbook.BackColor = System.Drawing.Color.Transparent;
            this.rdoReprintMailbook.Location = new System.Drawing.Point(62, 166);
            this.rdoReprintMailbook.Name = "rdoReprintMailbook";
            this.rdoReprintMailbook.Size = new System.Drawing.Size(138, 24);
            this.rdoReprintMailbook.TabIndex = 8;
            this.rdoReprintMailbook.TabStop = true;
            this.rdoReprintMailbook.Text = "Reprint Mailbook For:";
            this.rdoReprintMailbook.UseVisualStyleBackColor = false;
            this.rdoReprintMailbook.CheckedChanged += new System.EventHandler(this.rdoReprintMailbook_CheckedChanged);
            // 
            // lblPFIMailerDate
            // 
            this.lblPFIMailerDate.AutoSize = true;
            this.lblPFIMailerDate.BackColor = System.Drawing.Color.Transparent;
            this.lblPFIMailerDate.Location = new System.Drawing.Point(277, 82);
            this.lblPFIMailerDate.Name = "lblPFIMailerDate";
            this.lblPFIMailerDate.Size = new System.Drawing.Size(67, 13);
            this.lblPFIMailerDate.TabIndex = 151;
            this.lblPFIMailerDate.Text = "Current Date";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            this.lblTo.Location = new System.Drawing.Point(376, 139);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 13);
            this.lblTo.TabIndex = 154;
            this.lblTo.Text = "To";
            // 
            // txtEndTicket
            // 
            this.txtEndTicket.Location = new System.Drawing.Point(403, 136);
            this.txtEndTicket.Name = "txtEndTicket";
            this.txtEndTicket.Size = new System.Drawing.Size(100, 20);
            this.txtEndTicket.TabIndex = 6;
            this.txtEndTicket.Enter += new System.EventHandler(this.txtBeginTicket_Enter);
            // 
            // txtBeginTicket
            // 
            this.txtBeginTicket.Location = new System.Drawing.Point(270, 136);
            this.txtBeginTicket.Name = "txtBeginTicket";
            this.txtBeginTicket.Size = new System.Drawing.Size(100, 20);
            this.txtBeginTicket.TabIndex = 5;
            this.txtBeginTicket.Enter += new System.EventHandler(this.txtBeginTicket_Enter);
            // 
            // dateReprintMailers
            // 
            this.dateReprintMailers.AllowKeyUpAndDown = false;
            this.dateReprintMailers.AllowMonthlySelection = false;
            this.dateReprintMailers.AllowWeekends = true;
            this.dateReprintMailers.AutoSize = true;
            this.dateReprintMailers.BackColor = System.Drawing.Color.Transparent;
            this.dateReprintMailers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateReprintMailers.Location = new System.Drawing.Point(269, 104);
            this.dateReprintMailers.Name = "dateReprintMailers";
            this.dateReprintMailers.PositionPopupCalendarOverTextbox = false;
            this.dateReprintMailers.SelectedDate = "mm/dd/yyyy";
            this.dateReprintMailers.Size = new System.Drawing.Size(267, 32);
            this.dateReprintMailers.TabIndex = 3;
            this.dateReprintMailers.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateReprintMailers.Enter += new System.EventHandler(this.dateReprintMailers_Enter);
            // 
            // dateReprintMailbook
            // 
            this.dateReprintMailbook.AllowKeyUpAndDown = false;
            this.dateReprintMailbook.AllowMonthlySelection = false;
            this.dateReprintMailbook.AllowWeekends = true;
            this.dateReprintMailbook.AutoSize = true;
            this.dateReprintMailbook.BackColor = System.Drawing.Color.Transparent;
            this.dateReprintMailbook.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateReprintMailbook.Location = new System.Drawing.Point(269, 166);
            this.dateReprintMailbook.Name = "dateReprintMailbook";
            this.dateReprintMailbook.PositionPopupCalendarOverTextbox = false;
            this.dateReprintMailbook.SelectedDate = "mm/dd/yyyy";
            this.dateReprintMailbook.Size = new System.Drawing.Size(267, 32);
            this.dateReprintMailbook.TabIndex = 9;
            this.dateReprintMailbook.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateReprintMailbook.Enter += new System.EventHandler(this.dateReprintMailbook_Enter);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("continueButton.BackgroundImage")));
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.continueButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(737, 270);
            this.continueButton.Margin = new System.Windows.Forms.Padding(0);
            this.continueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 50);
            this.continueButton.TabIndex = 11;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(22, 270);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(285, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 19);
            this.label3.TabIndex = 161;
            this.label3.Text = "Print PFI Mailers";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTicketRange
            // 
            this.dateTicketRange.AllowKeyUpAndDown = false;
            this.dateTicketRange.AllowMonthlySelection = false;
            this.dateTicketRange.AllowWeekends = true;
            this.dateTicketRange.AutoSize = true;
            this.dateTicketRange.BackColor = System.Drawing.Color.Transparent;
            this.dateTicketRange.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTicketRange.Location = new System.Drawing.Point(509, 134);
            this.dateTicketRange.Name = "dateTicketRange";
            this.dateTicketRange.PositionPopupCalendarOverTextbox = false;
            this.dateTicketRange.SelectedDate = "mm/dd/yyyy";
            this.dateTicketRange.Size = new System.Drawing.Size(364, 32);
            this.dateTicketRange.TabIndex = 7;
            this.dateTicketRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateTicketRange.SelectedDateChanged += new System.EventHandler(this.dateTicketRange_SelectedDateChanged);
            this.dateTicketRange.Enter += new System.EventHandler(this.dateTicketRange_Enter);
            // 
            // PFI_Mailers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(860, 335);
            this.ControlBox = false;
            this.Controls.Add(this.dateReprintMailers);
            this.Controls.Add(this.dateTicketRange);
            this.Controls.Add(this.dateReprintMailbook);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.txtBeginTicket);
            this.Controls.Add(this.txtEndTicket);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblPFIMailerDate);
            this.Controls.Add(this.rdoReprintMailbook);
            this.Controls.Add(this.rdoReprintMailersForTickets);
            this.Controls.Add(this.rdoReprintMailers);
            this.Controls.Add(this.rdoPrintPFIMailers);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PFI_Mailers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print PFI Mailers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrintPFIMailers;
        private System.Windows.Forms.RadioButton rdoPrintPFIMailers;
        private System.Windows.Forms.RadioButton rdoReprintMailers;
        private System.Windows.Forms.RadioButton rdoReprintMailersForTickets;
        private System.Windows.Forms.RadioButton rdoReprintMailbook;
        private System.Windows.Forms.Label lblPFIMailerDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtEndTicket;
        private System.Windows.Forms.TextBox txtBeginTicket;
        private global::Pawn.Forms.UserControls.DateCalendar dateReprintMailers;
        private global::Pawn.Forms.UserControls.DateCalendar dateReprintMailbook;
        private CustomButton continueButton;
        private CustomButton cancelButton;
        private System.Windows.Forms.Label label3;
        private DateCalendar dateTicketRange;
    }
}
