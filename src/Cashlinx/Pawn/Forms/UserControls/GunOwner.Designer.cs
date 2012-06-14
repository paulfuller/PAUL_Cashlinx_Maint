namespace Pawn.Forms.UserControls
{
    partial class GunOwner
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label12;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dateEnd = new UserControls.DateCalendar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dateStart = new UserControls.DateCalendar();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtCustomerNumber = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Tahoma", 11F);
            label2.Location = new System.Drawing.Point(3, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(45, 27);
            label2.TabIndex = 58;
            label2.Text = "To:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Tahoma", 11F);
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(66, 27);
            label1.TabIndex = 58;
            label1.Text = "Date:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.Color.Transparent;
            label6.Font = new System.Drawing.Font("Tahoma", 11F);
            label6.Location = new System.Drawing.Point(3, 57);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(223, 27);
            label6.TabIndex = 70;
            label6.Text = "Customer Last Name:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Tahoma", 11F);
            label10.Location = new System.Drawing.Point(3, 89);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(224, 27);
            label10.TabIndex = 71;
            label10.Text = "Customer First Name:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.Color.Transparent;
            label12.Font = new System.Drawing.Font("Tahoma", 11F);
            label12.Location = new System.Drawing.Point(3, 121);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(196, 27);
            label12.TabIndex = 75;
            label12.Text = "Customer Number:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtLastName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtFirstName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCustomerNumber, 1, 3);
            this.tableLayoutPanel1.Controls.Add(label12, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(542, 161);
            this.tableLayoutPanel1.TabIndex = 107;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dateEnd, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(285, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(257, 46);
            this.tableLayoutPanel3.TabIndex = 108;
            // 
            // dateEnd
            // 
            this.dateEnd.AllowKeyUpAndDown = false;
            this.dateEnd.AllowMonthlySelection = false;
            this.dateEnd.AllowWeekends = true;
            this.dateEnd.AutoSize = true;
            this.dateEnd.BackColor = System.Drawing.Color.Transparent;
            this.dateEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEnd.Location = new System.Drawing.Point(55, 5);
            this.dateEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.PositionPopupCalendarOverTextbox = false;
            this.dateEnd.SelectedDate = "10/12/2009";
            this.dateEnd.Size = new System.Drawing.Size(197, 36);
            this.dateEnd.TabIndex = 1;
            this.dateEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateStart, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(276, 51);
            this.tableLayoutPanel2.TabIndex = 107;
            // 
            // dateStart
            // 
            this.dateStart.AllowKeyUpAndDown = false;
            this.dateStart.AllowMonthlySelection = false;
            this.dateStart.AllowWeekends = true;
            this.dateStart.AutoSize = true;
            this.dateStart.BackColor = System.Drawing.Color.Transparent;
            this.dateStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateStart.Location = new System.Drawing.Point(76, 5);
            this.dateStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateStart.Name = "dateStart";
            this.dateStart.PositionPopupCalendarOverTextbox = false;
            this.dateStart.SelectedDate = "10/12/2009";
            this.dateStart.Size = new System.Drawing.Size(197, 41);
            this.dateStart.TabIndex = 1;
            this.dateStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(285, 60);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(183, 26);
            this.txtLastName.TabIndex = 5;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(285, 92);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(183, 26);
            this.txtFirstName.TabIndex = 6;
            // 
            // txtCustomerNumber
            // 
            this.txtCustomerNumber.Location = new System.Drawing.Point(285, 124);
            this.txtCustomerNumber.Name = "txtCustomerNumber";
            this.txtCustomerNumber.Size = new System.Drawing.Size(123, 26);
            this.txtCustomerNumber.TabIndex = 8;
            // 
            // GunOwner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GunOwner";
            this.Size = new System.Drawing.Size(548, 180);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DateCalendar dateEnd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DateCalendar dateStart;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtCustomerNumber;
    }
}
