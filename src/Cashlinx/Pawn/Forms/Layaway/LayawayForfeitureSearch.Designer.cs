using CashlinxDesktop.UserControls;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Layaway
{
    partial class LayawayForfeitureSearch
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
            this.rdoLayawayNumber = new System.Windows.Forms.RadioButton();
            this.rdoAllEligible = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransactionNumber = new System.Windows.Forms.TextBox();
            this.txtShopNumber = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dpEligibilityDate = new DateCalendar();
            this.SuspendLayout();
            // 
            // rdoLayawayNumber
            // 
            this.rdoLayawayNumber.AutoSize = true;
            this.rdoLayawayNumber.BackColor = System.Drawing.Color.Transparent;
            this.rdoLayawayNumber.Checked = true;
            this.rdoLayawayNumber.Location = new System.Drawing.Point(188, 80);
            this.rdoLayawayNumber.Name = "rdoLayawayNumber";
            this.rdoLayawayNumber.Size = new System.Drawing.Size(112, 17);
            this.rdoLayawayNumber.TabIndex = 1;
            this.rdoLayawayNumber.TabStop = true;
            this.rdoLayawayNumber.Text = "Layaway Number:";
            this.rdoLayawayNumber.UseVisualStyleBackColor = false;
            this.rdoLayawayNumber.CheckedChanged += new System.EventHandler(this.rdoLayawayNumber_CheckedChanged);
            // 
            // rdoAllEligible
            // 
            this.rdoAllEligible.AutoSize = true;
            this.rdoAllEligible.BackColor = System.Drawing.Color.Transparent;
            this.rdoAllEligible.Location = new System.Drawing.Point(188, 137);
            this.rdoAllEligible.Name = "rdoAllEligible";
            this.rdoAllEligible.Size = new System.Drawing.Size(159, 17);
            this.rdoAllEligible.TabIndex = 6;
            this.rdoAllEligible.Text = "Display all eligible as of date";
            this.rdoAllEligible.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(199, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Or";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(353, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(239, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Layaway Forfeit Processing";
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Location = new System.Drawing.Point(370, 79);
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.Size = new System.Drawing.Size(100, 21);
            this.txtTransactionNumber.TabIndex = 4;
            // 
            // txtShopNumber
            // 
            this.txtShopNumber.Enabled = false;
            this.txtShopNumber.Location = new System.Drawing.Point(306, 79);
            this.txtShopNumber.MaxLength = 5;
            this.txtShopNumber.Name = "txtShopNumber";
            this.txtShopNumber.Size = new System.Drawing.Size(41, 21);
            this.txtShopNumber.TabIndex = 2;
            // 
            // btnSubmit
            // 
            this.btnSubmit.AutoSize = true;
            this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(561, 268);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 40);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(12, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dpEligibilityDate
            // 
            this.dpEligibilityDate.AllowKeyUpAndDown = false;
            this.dpEligibilityDate.AllowMonthlySelection = false;
            this.dpEligibilityDate.AllowWeekends = false;
            this.dpEligibilityDate.AutoSize = true;
            this.dpEligibilityDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dpEligibilityDate.BackColor = System.Drawing.Color.Transparent;
            this.dpEligibilityDate.Location = new System.Drawing.Point(353, 132);
            this.dpEligibilityDate.Name = "dpEligibilityDate";
            this.dpEligibilityDate.PositionPopupCalendarOverTextbox = true;
            this.dpEligibilityDate.SelectedDate = "mm/dd/yyyy";
            this.dpEligibilityDate.Size = new System.Drawing.Size(132, 26);
            this.dpEligibilityDate.TabIndex = 7;
            this.dpEligibilityDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // LayawayForfeitureSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 320);
            this.Controls.Add(this.dpEligibilityDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtShopNumber);
            this.Controls.Add(this.txtTransactionNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdoAllEligible);
            this.Controls.Add(this.rdoLayawayNumber);
            this.Name = "LayawayForfeitureSearch";
            this.Text = "Layaway Forfeiture Search";
            this.Shown += new System.EventHandler(this.LayawayForfeitureSearch_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoLayawayNumber;
        private System.Windows.Forms.RadioButton rdoAllEligible;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTransactionNumber;
        private System.Windows.Forms.TextBox txtShopNumber;
        private DateCalendar dpEligibilityDate;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}