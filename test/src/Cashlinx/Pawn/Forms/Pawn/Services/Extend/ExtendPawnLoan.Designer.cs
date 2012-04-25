using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;


namespace Pawn.Forms.Pawn.Services.Extend
{
    partial class ExtendPawnLoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendPawnLoan));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelLoanNumber = new System.Windows.Forms.Label();
            this.labelCurrentDueDateHeading = new System.Windows.Forms.Label();
            this.labelNumDaysToExtendHeading = new System.Windows.Forms.Label();
            this.labelAmtToextend = new System.Windows.Forms.Label();
            this.labelDailyAmtHeading = new System.Windows.Forms.Label();
            this.labelextendedDueDate = new System.Windows.Forms.Label();
            this.labelCurrDueDate = new System.Windows.Forms.Label();
            this.labelDailyAmount = new System.Windows.Forms.Label();
            this.labelAdjustedDueDate = new System.Windows.Forms.Label();
            this.labellastdaypickup = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelLoanSelection = new System.Windows.Forms.Label();
            this.checkBoxPrintSingleMemoForExtn = new System.Windows.Forms.CheckBox();
            this.labelExtendPastPickupAmount = new System.Windows.Forms.Label();
            this.labelExtendPastRenewAmt = new System.Windows.Forms.Label();
            this.customButtonSkip = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonCancel = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonContinue = new Common.Libraries.Forms.Components.CustomButton();
            this.customTextBoxNumDaystoExtend = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customTextBoxAmtToExtend = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customButtonCalculate = new Common.Libraries.Forms.Components.CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNumDaysToExtend = new System.Windows.Forms.Label();
            this.ddlNumDaystoExtend = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAmtToExtend = new System.Windows.Forms.Label();
            this.dateCalendarLastPickupDate = new DateCalendar();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(13, 13);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(147, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Extend - Pawn Loan ";
            // 
            // labelLoanNumber
            // 
            this.labelLoanNumber.AutoSize = true;
            this.labelLoanNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoanNumber.ForeColor = System.Drawing.Color.White;
            this.labelLoanNumber.Location = new System.Drawing.Point(159, 13);
            this.labelLoanNumber.Name = "labelLoanNumber";
            this.labelLoanNumber.Size = new System.Drawing.Size(89, 16);
            this.labelLoanNumber.TabIndex = 1;
            this.labelLoanNumber.Text = "loannumber";
            // 
            // labelCurrentDueDateHeading
            // 
            this.labelCurrentDueDateHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCurrentDueDateHeading.AutoSize = true;
            this.labelCurrentDueDateHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentDueDateHeading.Location = new System.Drawing.Point(124, 11);
            this.labelCurrentDueDateHeading.Name = "labelCurrentDueDateHeading";
            this.labelCurrentDueDateHeading.Size = new System.Drawing.Size(90, 13);
            this.labelCurrentDueDateHeading.TabIndex = 0;
            this.labelCurrentDueDateHeading.Text = "Current Due Date";
            this.labelCurrentDueDateHeading.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelNumDaysToExtendHeading
            // 
            this.labelNumDaysToExtendHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelNumDaysToExtendHeading.AutoSize = true;
            this.labelNumDaysToExtendHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelNumDaysToExtendHeading.Location = new System.Drawing.Point(83, 46);
            this.labelNumDaysToExtendHeading.Name = "labelNumDaysToExtendHeading";
            this.labelNumDaysToExtendHeading.Size = new System.Drawing.Size(131, 13);
            this.labelNumDaysToExtendHeading.TabIndex = 1;
            this.labelNumDaysToExtendHeading.Text = "Number of Days to Extend";
            this.labelNumDaysToExtendHeading.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelAmtToextend
            // 
            this.labelAmtToextend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelAmtToextend.AutoSize = true;
            this.labelAmtToextend.BackColor = System.Drawing.Color.Transparent;
            this.labelAmtToextend.Location = new System.Drawing.Point(123, 81);
            this.labelAmtToextend.Name = "labelAmtToextend";
            this.labelAmtToextend.Size = new System.Drawing.Size(91, 13);
            this.labelAmtToextend.TabIndex = 2;
            this.labelAmtToextend.Text = "Amount to Extend";
            this.labelAmtToextend.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDailyAmtHeading
            // 
            this.labelDailyAmtHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDailyAmtHeading.AutoSize = true;
            this.labelDailyAmtHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelDailyAmtHeading.Location = new System.Drawing.Point(145, 116);
            this.labelDailyAmtHeading.Name = "labelDailyAmtHeading";
            this.labelDailyAmtHeading.Size = new System.Drawing.Size(69, 13);
            this.labelDailyAmtHeading.TabIndex = 3;
            this.labelDailyAmtHeading.Text = "Daily Amount";
            this.labelDailyAmtHeading.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelextendedDueDate
            // 
            this.labelextendedDueDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelextendedDueDate.AutoSize = true;
            this.labelextendedDueDate.BackColor = System.Drawing.Color.Transparent;
            this.labelextendedDueDate.Location = new System.Drawing.Point(113, 151);
            this.labelextendedDueDate.Name = "labelextendedDueDate";
            this.labelextendedDueDate.Size = new System.Drawing.Size(101, 13);
            this.labelextendedDueDate.TabIndex = 4;
            this.labelextendedDueDate.Text = "Extended Due Date";
            this.labelextendedDueDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCurrDueDate
            // 
            this.labelCurrDueDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCurrDueDate.AutoSize = true;
            this.labelCurrDueDate.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrDueDate.Location = new System.Drawing.Point(220, 11);
            this.labelCurrDueDate.Name = "labelCurrDueDate";
            this.labelCurrDueDate.Size = new System.Drawing.Size(65, 13);
            this.labelCurrDueDate.TabIndex = 6;
            this.labelCurrDueDate.Text = "12/12/2008";
            // 
            // labelDailyAmount
            // 
            this.labelDailyAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDailyAmount.AutoSize = true;
            this.labelDailyAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelDailyAmount.Location = new System.Drawing.Point(220, 116);
            this.labelDailyAmount.Name = "labelDailyAmount";
            this.labelDailyAmount.Size = new System.Drawing.Size(13, 13);
            this.labelDailyAmount.TabIndex = 9;
            this.labelDailyAmount.Text = "0";
            // 
            // labelAdjustedDueDate
            // 
            this.labelAdjustedDueDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAdjustedDueDate.AutoSize = true;
            this.labelAdjustedDueDate.BackColor = System.Drawing.Color.Transparent;
            this.labelAdjustedDueDate.Location = new System.Drawing.Point(220, 151);
            this.labelAdjustedDueDate.Name = "labelAdjustedDueDate";
            this.labelAdjustedDueDate.Size = new System.Drawing.Size(65, 13);
            this.labelAdjustedDueDate.TabIndex = 11;
            this.labelAdjustedDueDate.Text = "01/10/2009";
            // 
            // labellastdaypickup
            // 
            this.labellastdaypickup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labellastdaypickup.AutoSize = true;
            this.labellastdaypickup.BackColor = System.Drawing.Color.Transparent;
            this.labellastdaypickup.Location = new System.Drawing.Point(117, 185);
            this.labellastdaypickup.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.labellastdaypickup.Name = "labellastdaypickup";
            this.labellastdaypickup.Size = new System.Drawing.Size(97, 13);
            this.labellastdaypickup.TabIndex = 5;
            this.labellastdaypickup.Text = "Last Day to Pickup";
            this.labellastdaypickup.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(2, 512);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(717, 2);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // labelLoanSelection
            // 
            this.labelLoanSelection.AutoSize = true;
            this.labelLoanSelection.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanSelection.ForeColor = System.Drawing.Color.White;
            this.labelLoanSelection.Location = new System.Drawing.Point(497, 15);
            this.labelLoanSelection.Name = "labelLoanSelection";
            this.labelLoanSelection.Size = new System.Drawing.Size(34, 13);
            this.labelLoanSelection.TabIndex = 9;
            this.labelLoanSelection.Text = "1 of 1";
            // 
            // checkBoxPrintSingleMemoForExtn
            // 
            this.checkBoxPrintSingleMemoForExtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPrintSingleMemoForExtn.AutoSize = true;
            this.checkBoxPrintSingleMemoForExtn.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPrintSingleMemoForExtn.Location = new System.Drawing.Point(154, 427);
            this.checkBoxPrintSingleMemoForExtn.Name = "checkBoxPrintSingleMemoForExtn";
            this.checkBoxPrintSingleMemoForExtn.Size = new System.Drawing.Size(207, 17);
            this.checkBoxPrintSingleMemoForExtn.TabIndex = 4;
            this.checkBoxPrintSingleMemoForExtn.Text = "Print Single Memorandum of Extension";
            this.checkBoxPrintSingleMemoForExtn.UseVisualStyleBackColor = false;
            this.checkBoxPrintSingleMemoForExtn.Visible = false;
            // 
            // labelExtendPastPickupAmount
            // 
            this.labelExtendPastPickupAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExtendPastPickupAmount.AutoSize = true;
            this.labelExtendPastPickupAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelExtendPastPickupAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExtendPastPickupAmount.ForeColor = System.Drawing.Color.Red;
            this.labelExtendPastPickupAmount.Location = new System.Drawing.Point(118, 454);
            this.labelExtendPastPickupAmount.Name = "labelExtendPastPickupAmount";
            this.labelExtendPastPickupAmount.Size = new System.Drawing.Size(310, 13);
            this.labelExtendPastPickupAmount.TabIndex = 13;
            this.labelExtendPastPickupAmount.Text = "Amount to Extend is more than the Pickup Amount of ";
            this.labelExtendPastPickupAmount.Visible = false;
            // 
            // labelExtendPastRenewAmt
            // 
            this.labelExtendPastRenewAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExtendPastRenewAmt.AutoSize = true;
            this.labelExtendPastRenewAmt.BackColor = System.Drawing.Color.Transparent;
            this.labelExtendPastRenewAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExtendPastRenewAmt.ForeColor = System.Drawing.Color.Red;
            this.labelExtendPastRenewAmt.Location = new System.Drawing.Point(116, 470);
            this.labelExtendPastRenewAmt.Name = "labelExtendPastRenewAmt";
            this.labelExtendPastRenewAmt.Size = new System.Drawing.Size(320, 13);
            this.labelExtendPastRenewAmt.TabIndex = 14;
            this.labelExtendPastRenewAmt.Text = "Amount to Extend is more than the Renewal Amount of ";
            this.labelExtendPastRenewAmt.Visible = false;
            // 
            // customButtonSkip
            // 
            this.customButtonSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.customButtonSkip.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSkip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSkip.BackgroundImage")));
            this.customButtonSkip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSkip.CausesValidation = false;
            this.customButtonSkip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSkip.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSkip.FlatAppearance.BorderSize = 0;
            this.customButtonSkip.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSkip.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSkip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSkip.ForeColor = System.Drawing.Color.White;
            this.customButtonSkip.Location = new System.Drawing.Point(501, 524);
            this.customButtonSkip.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSkip.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSkip.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSkip.Name = "customButtonSkip";
            this.customButtonSkip.Size = new System.Drawing.Size(100, 50);
            this.customButtonSkip.TabIndex = 17;
            this.customButtonSkip.Text = "&Skip";
            this.customButtonSkip.UseVisualStyleBackColor = false;
            this.customButtonSkip.Click += new System.EventHandler(this.customButtonSkip_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.CausesValidation = false;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(16, 524);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 16;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // customButtonContinue
            // 
            this.customButtonContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.customButtonContinue.BackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonContinue.BackgroundImage")));
            this.customButtonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonContinue.CausesValidation = false;
            this.customButtonContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonContinue.FlatAppearance.BorderSize = 0;
            this.customButtonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonContinue.ForeColor = System.Drawing.Color.White;
            this.customButtonContinue.Location = new System.Drawing.Point(601, 524);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 15;
            this.customButtonContinue.Text = "Co&ntinue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // customTextBoxNumDaystoExtend
            // 
            this.customTextBoxNumDaystoExtend.AllowOnlyNumbers = true;
            this.customTextBoxNumDaystoExtend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxNumDaystoExtend.CausesValidation = false;
            this.customTextBoxNumDaystoExtend.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxNumDaystoExtend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxNumDaystoExtend.Location = new System.Drawing.Point(3, 3);
            this.customTextBoxNumDaystoExtend.MaxLength = 5;
            this.customTextBoxNumDaystoExtend.Name = "customTextBoxNumDaystoExtend";
            this.customTextBoxNumDaystoExtend.Required = true;
            this.customTextBoxNumDaystoExtend.Size = new System.Drawing.Size(43, 21);
            this.customTextBoxNumDaystoExtend.TabIndex = 1;
            this.customTextBoxNumDaystoExtend.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxNumDaystoExtend.KeyUp += new System.Windows.Forms.KeyEventHandler(this.customTextBoxNumDaystoExtend_KeyUp);
            this.customTextBoxNumDaystoExtend.Leave += new System.EventHandler(this.customTextBoxNumDaystoExtend_Leave);
            // 
            // customTextBoxAmtToExtend
            // 
            this.customTextBoxAmtToExtend.AllowDecimalNumbers = true;
            this.customTextBoxAmtToExtend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxAmtToExtend.CausesValidation = false;
            this.customTextBoxAmtToExtend.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxAmtToExtend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAmtToExtend.Location = new System.Drawing.Point(3, 3);
            this.customTextBoxAmtToExtend.MaxLength = 8;
            this.customTextBoxAmtToExtend.Name = "customTextBoxAmtToExtend";
            this.customTextBoxAmtToExtend.RegularExpression = true;
            this.customTextBoxAmtToExtend.Required = true;
            this.customTextBoxAmtToExtend.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxAmtToExtend.TabIndex = 2;
            this.customTextBoxAmtToExtend.ValidationExpression = "\\d*|\\d*\\.\\d{1,2}";
            this.customTextBoxAmtToExtend.KeyUp += new System.Windows.Forms.KeyEventHandler(this.customTextBoxAmtToExtend_KeyUp);
            this.customTextBoxAmtToExtend.Leave += new System.EventHandler(this.customTextBoxAmtToExtend_Leave);
            // 
            // customButtonCalculate
            // 
            this.customButtonCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.customButtonCalculate.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCalculate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCalculate.BackgroundImage")));
            this.customButtonCalculate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCalculate.CausesValidation = false;
            this.customButtonCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCalculate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCalculate.FlatAppearance.BorderSize = 0;
            this.customButtonCalculate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCalculate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCalculate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCalculate.ForeColor = System.Drawing.Color.White;
            this.customButtonCalculate.Location = new System.Drawing.Point(347, 524);
            this.customButtonCalculate.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCalculate.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCalculate.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCalculate.Name = "customButtonCalculate";
            this.customButtonCalculate.Size = new System.Drawing.Size(100, 50);
            this.customButtonCalculate.TabIndex = 18;
            this.customButtonCalculate.Text = "Ca&lculate";
            this.customButtonCalculate.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.48882F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.51118F));
            this.tableLayoutPanel1.Controls.Add(this.labelCurrentDueDateHeading, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelCurrDueDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelNumDaysToExtendHeading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelAmtToextend, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelDailyAmtHeading, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelextendedDueDate, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.dateCalendarLastPickupDate, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelAdjustedDueDate, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelDailyAmount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labellastdaypickup, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(19, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 341);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.customTextBoxNumDaystoExtend);
            this.flowLayoutPanel1.Controls.Add(this.lblNumDaysToExtend);
            this.flowLayoutPanel1.Controls.Add(this.ddlNumDaystoExtend);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(220, 38);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(448, 29);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // lblNumDaysToExtend
            // 
            this.lblNumDaysToExtend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumDaysToExtend.AutoSize = true;
            this.lblNumDaysToExtend.Location = new System.Drawing.Point(52, 10);
            this.lblNumDaysToExtend.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblNumDaysToExtend.Name = "lblNumDaysToExtend";
            this.lblNumDaysToExtend.Size = new System.Drawing.Size(35, 13);
            this.lblNumDaysToExtend.TabIndex = 2;
            this.lblNumDaysToExtend.Text = "label1";
            this.lblNumDaysToExtend.Visible = false;
            // 
            // ddlNumDaystoExtend
            // 
            this.ddlNumDaystoExtend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlNumDaystoExtend.FormattingEnabled = true;
            this.ddlNumDaystoExtend.Location = new System.Drawing.Point(93, 3);
            this.ddlNumDaystoExtend.Name = "ddlNumDaystoExtend";
            this.ddlNumDaystoExtend.Size = new System.Drawing.Size(121, 21);
            this.ddlNumDaystoExtend.TabIndex = 3;
            this.ddlNumDaystoExtend.SelectedIndexChanged += new System.EventHandler(this.ddlNumDaystoExtend_SelectedIndexChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.customTextBoxAmtToExtend);
            this.flowLayoutPanel2.Controls.Add(this.lblAmtToExtend);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(220, 73);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(448, 29);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // lblAmtToExtend
            // 
            this.lblAmtToExtend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAmtToExtend.AutoSize = true;
            this.lblAmtToExtend.Location = new System.Drawing.Point(109, 10);
            this.lblAmtToExtend.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblAmtToExtend.Name = "lblAmtToExtend";
            this.lblAmtToExtend.Size = new System.Drawing.Size(35, 13);
            this.lblAmtToExtend.TabIndex = 3;
            this.lblAmtToExtend.Text = "label2";
            // 
            // dateCalendarLastPickupDate
            // 
            this.dateCalendarLastPickupDate.AllowKeyUpAndDown = true;
            this.dateCalendarLastPickupDate.AllowMonthlySelection = false;
            this.dateCalendarLastPickupDate.AllowWeekends = false;
            this.dateCalendarLastPickupDate.AutoSize = true;
            this.dateCalendarLastPickupDate.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarLastPickupDate.CausesValidation = false;
            this.dateCalendarLastPickupDate.Location = new System.Drawing.Point(221, 179);
            this.dateCalendarLastPickupDate.Margin = new System.Windows.Forms.Padding(4);
            this.dateCalendarLastPickupDate.Name = "dateCalendarLastPickupDate";
            this.dateCalendarLastPickupDate.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarLastPickupDate.SelectedDate = "10/12/2009";
            this.dateCalendarLastPickupDate.Size = new System.Drawing.Size(132, 26);
            this.dateCalendarLastPickupDate.TabIndex = 3;
            this.dateCalendarLastPickupDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateCalendarLastPickupDate.SelectedDateChanged += new System.EventHandler(this.dateCalendarLastPickupDate_SelectedDateChanged);
            this.dateCalendarLastPickupDate.Leave += new System.EventHandler(this.dateCalendarLastPickupDate_Leave);
            // 
            // ExtendPawnLoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(709, 594);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.customButtonCalculate);
            this.Controls.Add(this.customButtonSkip);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.labelExtendPastRenewAmt);
            this.Controls.Add(this.labelExtendPastPickupAmount);
            this.Controls.Add(this.checkBoxPrintSingleMemoForExtn);
            this.Controls.Add(this.labelLoanSelection);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelLoanNumber);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendPawnLoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExtendPawnLoan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtendPawnLoan_FormClosing);
            this.Load += new System.EventHandler(this.ExtendPawnLoan_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelLoanNumber;
        private System.Windows.Forms.Label labelCurrentDueDateHeading;
        private System.Windows.Forms.Label labelNumDaysToExtendHeading;
        private System.Windows.Forms.Label labelAmtToextend;
        private System.Windows.Forms.Label labelDailyAmtHeading;
        private System.Windows.Forms.Label labelextendedDueDate;
        private System.Windows.Forms.Label labellastdaypickup;
        private System.Windows.Forms.Label labelCurrDueDate;
        private CustomTextBox customTextBoxNumDaystoExtend;
        private CustomTextBox customTextBoxAmtToExtend;
        private System.Windows.Forms.Label labelDailyAmount;
        private DateCalendar dateCalendarLastPickupDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelLoanSelection;
        private System.Windows.Forms.Label labelAdjustedDueDate;
        private System.Windows.Forms.CheckBox checkBoxPrintSingleMemoForExtn;
        private System.Windows.Forms.Label labelExtendPastPickupAmount;
        private System.Windows.Forms.Label labelExtendPastRenewAmt;
        private CustomButton customButtonContinue;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSkip;
        private CustomButton customButtonCalculate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblNumDaysToExtend;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label lblAmtToExtend;
        private System.Windows.Forms.ComboBox ddlNumDaystoExtend;
    }
}
