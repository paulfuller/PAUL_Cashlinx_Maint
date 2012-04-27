using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Retail
{
    partial class LayawayDetails
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
            this.continueButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateFirstPaymentDate = new DateCalendar();
            this.txtMonthlyPaymentAmount = new System.Windows.Forms.TextBox();
            this.txtServiceFee = new System.Windows.Forms.TextBox();
            this.txtDownPayment = new CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPayment12 = new System.Windows.Forms.Label();
            this.lblPayment06 = new System.Windows.Forms.Label();
            this.lblPayment11 = new System.Windows.Forms.Label();
            this.lblPayment05 = new System.Windows.Forms.Label();
            this.lblPayment10 = new System.Windows.Forms.Label();
            this.lblPayment04 = new System.Windows.Forms.Label();
            this.lblPayment09 = new System.Windows.Forms.Label();
            this.lblPayment03 = new System.Windows.Forms.Label();
            this.lblPayment08 = new System.Windows.Forms.Label();
            this.lblPayment02 = new System.Windows.Forms.Label();
            this.lblPayment07 = new System.Windows.Forms.Label();
            this.lblPayment01 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNumberOfPayments = new CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // continueButton
            // 
            this.continueButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.continueButton.AutoSize = true;
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(392, 371);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 40);
            this.continueButton.TabIndex = 16;
            this.continueButton.Text = "OK";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(3, 371);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dateFirstPaymentDate);
            this.panel1.Controls.Add(this.txtMonthlyPaymentAmount);
            this.panel1.Controls.Add(this.txtServiceFee);
            this.panel1.Controls.Add(this.txtDownPayment);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtNumberOfPayments);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.continueButton);
            this.panel1.Location = new System.Drawing.Point(9, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 425);
            this.panel1.TabIndex = 0;
            // 
            // dateFirstPaymentDate
            // 
            this.dateFirstPaymentDate.AllowKeyUpAndDown = false;
            this.dateFirstPaymentDate.AllowMonthlySelection = false;
            this.dateFirstPaymentDate.AllowWeekends = true;
            this.dateFirstPaymentDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateFirstPaymentDate.AutoSize = true;
            this.dateFirstPaymentDate.BackColor = System.Drawing.Color.Transparent;
            this.dateFirstPaymentDate.Location = new System.Drawing.Point(255, 117);
            this.dateFirstPaymentDate.Name = "dateFirstPaymentDate";
            this.dateFirstPaymentDate.PositionPopupCalendarOverTextbox = true;
            this.dateFirstPaymentDate.SelectedDate = "mm/dd/yyyy";
            this.dateFirstPaymentDate.Size = new System.Drawing.Size(136, 26);
            this.dateFirstPaymentDate.TabIndex = 10;
            this.dateFirstPaymentDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.dateFirstPaymentDate.CalendarMouseLeave += new System.EventHandler(this.dateFirstPaymentDate_CalendarMouseLeave);
            this.dateFirstPaymentDate.CalendarMouseLeaving += new System.EventHandler(this.dateFirstPaymentDate_CalendarMouseLeaving);
            this.dateFirstPaymentDate.SelectedDateChanged += new System.EventHandler(this.dateFirstPaymentDate_SelectedDateChanged);
            this.dateFirstPaymentDate.SelectedDateChanging += new System.EventHandler(this.dateFirstPaymentDate_SelectedDateChanging);
            this.dateFirstPaymentDate.TextBoxTextChanged += new System.EventHandler(this.dateFirstPaymentDate_TextBoxTextChanged);
            this.dateFirstPaymentDate.Leave += new System.EventHandler(this.dateFirstPaymentDate_Leave);
            // 
            // txtMonthlyPaymentAmount
            // 
            this.txtMonthlyPaymentAmount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtMonthlyPaymentAmount.Enabled = false;
            this.txtMonthlyPaymentAmount.Location = new System.Drawing.Point(258, 143);
            this.txtMonthlyPaymentAmount.Name = "txtMonthlyPaymentAmount";
            this.txtMonthlyPaymentAmount.Size = new System.Drawing.Size(100, 21);
            this.txtMonthlyPaymentAmount.TabIndex = 12;
            this.txtMonthlyPaymentAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtServiceFee
            // 
            this.txtServiceFee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtServiceFee.Enabled = false;
            this.txtServiceFee.Location = new System.Drawing.Point(258, 72);
            this.txtServiceFee.Name = "txtServiceFee";
            this.txtServiceFee.Size = new System.Drawing.Size(100, 21);
            this.txtServiceFee.TabIndex = 5;
            this.txtServiceFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDownPayment
            // 
            this.txtDownPayment.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtDownPayment.CausesValidation = false;
            this.txtDownPayment.ErrorMessage = "The down payment must be in the format #.##";
            this.txtDownPayment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDownPayment.Location = new System.Drawing.Point(258, 49);
            this.txtDownPayment.Name = "txtDownPayment";
            this.txtDownPayment.Size = new System.Drawing.Size(100, 21);
            this.txtDownPayment.TabIndex = 2;
            this.txtDownPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDownPayment.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtDownPayment.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtDownPayment.Leave += new System.EventHandler(this.txtDownPayment_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Down Payment:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.lblPayment12, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment06, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment11, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment05, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment10, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment04, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment09, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment03, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment08, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment02, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment07, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPayment01, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(99, 219);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.7F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(296, 146);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // lblPayment12
            // 
            this.lblPayment12.AutoSize = true;
            this.lblPayment12.Location = new System.Drawing.Point(151, 120);
            this.lblPayment12.Name = "lblPayment12";
            this.lblPayment12.Size = new System.Drawing.Size(41, 13);
            this.lblPayment12.TabIndex = 11;
            this.lblPayment12.Text = "label23";
            // 
            // lblPayment06
            // 
            this.lblPayment06.AutoSize = true;
            this.lblPayment06.Location = new System.Drawing.Point(3, 120);
            this.lblPayment06.Name = "lblPayment06";
            this.lblPayment06.Size = new System.Drawing.Size(41, 13);
            this.lblPayment06.TabIndex = 10;
            this.lblPayment06.Text = "label22";
            // 
            // lblPayment11
            // 
            this.lblPayment11.AutoSize = true;
            this.lblPayment11.Location = new System.Drawing.Point(151, 96);
            this.lblPayment11.Name = "lblPayment11";
            this.lblPayment11.Size = new System.Drawing.Size(41, 13);
            this.lblPayment11.TabIndex = 9;
            this.lblPayment11.Text = "label21";
            // 
            // lblPayment05
            // 
            this.lblPayment05.AutoSize = true;
            this.lblPayment05.Location = new System.Drawing.Point(3, 96);
            this.lblPayment05.Name = "lblPayment05";
            this.lblPayment05.Size = new System.Drawing.Size(41, 13);
            this.lblPayment05.TabIndex = 8;
            this.lblPayment05.Text = "label20";
            // 
            // lblPayment10
            // 
            this.lblPayment10.AutoSize = true;
            this.lblPayment10.Location = new System.Drawing.Point(151, 72);
            this.lblPayment10.Name = "lblPayment10";
            this.lblPayment10.Size = new System.Drawing.Size(41, 13);
            this.lblPayment10.TabIndex = 7;
            this.lblPayment10.Text = "label19";
            // 
            // lblPayment04
            // 
            this.lblPayment04.AutoSize = true;
            this.lblPayment04.Location = new System.Drawing.Point(3, 72);
            this.lblPayment04.Name = "lblPayment04";
            this.lblPayment04.Size = new System.Drawing.Size(41, 13);
            this.lblPayment04.TabIndex = 6;
            this.lblPayment04.Text = "label18";
            // 
            // lblPayment09
            // 
            this.lblPayment09.AutoSize = true;
            this.lblPayment09.Location = new System.Drawing.Point(151, 48);
            this.lblPayment09.Name = "lblPayment09";
            this.lblPayment09.Size = new System.Drawing.Size(41, 13);
            this.lblPayment09.TabIndex = 5;
            this.lblPayment09.Text = "label17";
            // 
            // lblPayment03
            // 
            this.lblPayment03.AutoSize = true;
            this.lblPayment03.Location = new System.Drawing.Point(3, 48);
            this.lblPayment03.Name = "lblPayment03";
            this.lblPayment03.Size = new System.Drawing.Size(41, 13);
            this.lblPayment03.TabIndex = 4;
            this.lblPayment03.Text = "label16";
            // 
            // lblPayment08
            // 
            this.lblPayment08.AutoSize = true;
            this.lblPayment08.Location = new System.Drawing.Point(151, 24);
            this.lblPayment08.Name = "lblPayment08";
            this.lblPayment08.Size = new System.Drawing.Size(41, 13);
            this.lblPayment08.TabIndex = 3;
            this.lblPayment08.Text = "label15";
            // 
            // lblPayment02
            // 
            this.lblPayment02.AutoSize = true;
            this.lblPayment02.Location = new System.Drawing.Point(3, 24);
            this.lblPayment02.Name = "lblPayment02";
            this.lblPayment02.Size = new System.Drawing.Size(41, 13);
            this.lblPayment02.TabIndex = 2;
            this.lblPayment02.Text = "label14";
            // 
            // lblPayment07
            // 
            this.lblPayment07.AutoSize = true;
            this.lblPayment07.Location = new System.Drawing.Point(151, 0);
            this.lblPayment07.Name = "lblPayment07";
            this.lblPayment07.Size = new System.Drawing.Size(41, 13);
            this.lblPayment07.TabIndex = 0;
            this.lblPayment07.Text = "label13";
            // 
            // lblPayment01
            // 
            this.lblPayment01.AutoSize = true;
            this.lblPayment01.Location = new System.Drawing.Point(3, 0);
            this.lblPayment01.Name = "lblPayment01";
            this.lblPayment01.Size = new System.Drawing.Size(41, 13);
            this.lblPayment01.TabIndex = 0;
            this.lblPayment01.Text = "label12";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Service Fee Incl Tax:";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(176, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "Payment Schedule";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(231, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "*";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Number of Payments:";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(184, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Layaway Details";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "First Payment Date:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(231, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "*";
            // 
            // txtNumberOfPayments
            // 
            this.txtNumberOfPayments.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtNumberOfPayments.CausesValidation = false;
            this.txtNumberOfPayments.ErrorMessage = "The number of payments must be a numeric non-decimal number.";
            this.txtNumberOfPayments.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfPayments.Location = new System.Drawing.Point(258, 95);
            this.txtNumberOfPayments.Name = "txtNumberOfPayments";
            this.txtNumberOfPayments.Size = new System.Drawing.Size(100, 21);
            this.txtNumberOfPayments.TabIndex = 8;
            this.txtNumberOfPayments.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumberOfPayments.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtNumberOfPayments.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtNumberOfPayments.Leave += new System.EventHandler(this.txtNumberOfPayments_Leave);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Monthly Payment Amount:";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "colCost";
            this.dataGridViewTextBoxColumn1.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Cost";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "colDescription";
            this.dataGridViewTextBoxColumn2.FillWeight = 12.70586F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "colRetail";
            this.dataGridViewTextBoxColumn3.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Retail";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "colReason";
            this.dataGridViewTextBoxColumn4.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Reason";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "colTags";
            this.dataGridViewTextBoxColumn5.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn5.HeaderText = "# Tags";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // LayawayDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.ClientSize = new System.Drawing.Size(513, 509);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "LayawayDetails";
            this.Text = "Select Items";
            this.Load += new System.EventHandler(this.LayawayDetails_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private CustomTextBox txtDownPayment;
        private CustomTextBox txtNumberOfPayments;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblPayment01;
        private System.Windows.Forms.Label lblPayment07;
        private System.Windows.Forms.Label lblPayment02;
        private System.Windows.Forms.Label lblPayment08;
        private System.Windows.Forms.Label lblPayment03;
        private System.Windows.Forms.Label lblPayment09;
        private System.Windows.Forms.Label lblPayment04;
        private System.Windows.Forms.Label lblPayment10;
        private System.Windows.Forms.Label lblPayment05;
        private System.Windows.Forms.Label lblPayment11;
        private System.Windows.Forms.Label lblPayment06;
        private System.Windows.Forms.Label lblPayment12;
        private DateCalendar dateFirstPaymentDate;
        private System.Windows.Forms.TextBox txtServiceFee;
        private System.Windows.Forms.TextBox txtMonthlyPaymentAmount;
    }
}
