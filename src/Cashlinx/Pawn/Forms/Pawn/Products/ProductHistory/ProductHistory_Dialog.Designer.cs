//Odd file lock
namespace Pawn.Forms.Pawn.Products.ProductHistory
{
    partial class ProductHistory_Dialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_LoanStatsLayoutPanel = new System.Windows.Forms.Panel();
            this.PH_TotalLoanAmountLabel = new System.Windows.Forms.Label();
            this.PH_TotalLoanAmountText = new System.Windows.Forms.Label();
            this.PH_CurrentLoanNumberLabel = new System.Windows.Forms.Label();
            this.PH_CurrentLoanNumberText = new System.Windows.Forms.Label();
            this.PH_OrigLoanNumberLabel2 = new System.Windows.Forms.Label();
            this.PH_OrigLoanNumberText = new System.Windows.Forms.Label();
            this.PH_PrevLoanNumberLabel = new System.Windows.Forms.Label();
            this.PH_PrevLoanNumberText = new System.Windows.Forms.Label();
            this.PH_ApprovedByLabel = new System.Windows.Forms.Label();
            this.PH_ApprovedByValue = new System.Windows.Forms.Label();
            this.PH_LoanStatusLabel = new System.Windows.Forms.Label();
            this.PH_LoanStatusValue = new System.Windows.Forms.Label();
            this.PH_TerminalIDLabel = new System.Windows.Forms.Label();
            this.PH_TerminalIDValue = new System.Windows.Forms.Label();
            this.PH_MadeByEmpLabel = new System.Windows.Forms.Label();
            this.PH_MadeByEmpValue = new System.Windows.Forms.Label();
            this.PH_LoanOrigFeeLabel = new System.Windows.Forms.Label();
            this.PH_LoanOrigFeeValue = new System.Windows.Forms.Label();
            this.PH_ServiceChargeLabel = new System.Windows.Forms.Label();
            this.PH_ServiceChargeValue = new System.Windows.Forms.Label();
            this.PH_InterestLabel = new System.Windows.Forms.Label();
            this.PH_InterestValue = new System.Windows.Forms.Label();
            this.PH_LoanLabel = new System.Windows.Forms.Label();
            this.PH_LoanValue = new System.Windows.Forms.Label();
            this.PH_NotesLabel = new System.Windows.Forms.Label();
            this.PH_NotesValue = new System.Windows.Forms.Label();
            this.PH_CustomerIdentificationLabel = new System.Windows.Forms.Label();
            this.PH_CustomerIdentificationValue = new System.Windows.Forms.Label();
            this.PH_NotificationDateLabel = new System.Windows.Forms.Label();
            this.PH_NotificationDateValue = new System.Windows.Forms.Label();
            this.PH_EligibilityDateLabel = new System.Windows.Forms.Label();
            this.PH_EligibilityDateValue = new System.Windows.Forms.Label();
            this.PH_DueDateLabel = new System.Windows.Forms.Label();
            this.PH_DueDateValue = new System.Windows.Forms.Label();
            this.PH_OriginationDateLabel = new System.Windows.Forms.Label();
            this.PH_OriginationDateValue = new System.Windows.Forms.Label();
            this.PH_ItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.PH_ItemNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_LoanAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_SuggestedAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_LocationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PH_CloseButton = new System.Windows.Forms.Button();
            this.PH_LoanStatsLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PH_ItemsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IH_History_StatusDateColumn";
            this.dataGridViewTextBoxColumn1.HeaderText = "Status Date";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "IH_History_ItemStatusColumn";
            this.dataGridViewTextBoxColumn2.HeaderText = "Item Status";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "IH_History_ItemDescriptionColumn";
            this.dataGridViewTextBoxColumn3.HeaderText = "Item Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // PH_LoanStatsLayoutPanel
            // 
            this.PH_LoanStatsLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PH_LoanStatsLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_TotalLoanAmountLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_TotalLoanAmountText);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_CurrentLoanNumberLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_CurrentLoanNumberText);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_OrigLoanNumberLabel2);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_OrigLoanNumberText);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_PrevLoanNumberLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_PrevLoanNumberText);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_ApprovedByLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_ApprovedByValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_LoanStatusLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_LoanStatusValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_TerminalIDLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_TerminalIDValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_MadeByEmpLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_MadeByEmpValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_LoanOrigFeeLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_LoanOrigFeeValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_ServiceChargeLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_ServiceChargeValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_InterestLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_InterestValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_LoanLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_LoanValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_NotesLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_NotesValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_CustomerIdentificationLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_CustomerIdentificationValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_NotificationDateLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_NotificationDateValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_EligibilityDateLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_EligibilityDateValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_DueDateLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_DueDateValue);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_OriginationDateLabel);
            this.PH_LoanStatsLayoutPanel.Controls.Add(this.PH_OriginationDateValue);
            this.PH_LoanStatsLayoutPanel.Location = new System.Drawing.Point(11, 68);
            this.PH_LoanStatsLayoutPanel.Name = "PH_LoanStatsLayoutPanel";
            this.PH_LoanStatsLayoutPanel.Size = new System.Drawing.Size(776, 223);
            this.PH_LoanStatsLayoutPanel.TabIndex = 142;
            // 
            // PH_TotalLoanAmountLabel
            // 
            this.PH_TotalLoanAmountLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_TotalLoanAmountLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_TotalLoanAmountLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_TotalLoanAmountLabel.Location = new System.Drawing.Point(553, 120);
            this.PH_TotalLoanAmountLabel.Name = "PH_TotalLoanAmountLabel";
            this.PH_TotalLoanAmountLabel.Size = new System.Drawing.Size(134, 13);
            this.PH_TotalLoanAmountLabel.TabIndex = 187;
            this.PH_TotalLoanAmountLabel.Text = "Total Loan Amount:";
            this.PH_TotalLoanAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_TotalLoanAmountText
            // 
            this.PH_TotalLoanAmountText.BackColor = System.Drawing.Color.Transparent;
            this.PH_TotalLoanAmountText.ForeColor = System.Drawing.Color.Black;
            this.PH_TotalLoanAmountText.Location = new System.Drawing.Point(693, 120);
            this.PH_TotalLoanAmountText.Name = "PH_TotalLoanAmountText";
            this.PH_TotalLoanAmountText.Size = new System.Drawing.Size(61, 13);
            this.PH_TotalLoanAmountText.TabIndex = 188;
            this.PH_TotalLoanAmountText.Text = "$0.00";
            this.PH_TotalLoanAmountText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_CurrentLoanNumberLabel
            // 
            this.PH_CurrentLoanNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_CurrentLoanNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_CurrentLoanNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_CurrentLoanNumberLabel.Location = new System.Drawing.Point(7, 120);
            this.PH_CurrentLoanNumberLabel.Name = "PH_CurrentLoanNumberLabel";
            this.PH_CurrentLoanNumberLabel.Size = new System.Drawing.Size(142, 13);
            this.PH_CurrentLoanNumberLabel.TabIndex = 185;
            this.PH_CurrentLoanNumberLabel.Text = "Current Loan Number:";
            this.PH_CurrentLoanNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_CurrentLoanNumberText
            // 
            this.PH_CurrentLoanNumberText.BackColor = System.Drawing.Color.Transparent;
            this.PH_CurrentLoanNumberText.ForeColor = System.Drawing.Color.Black;
            this.PH_CurrentLoanNumberText.Location = new System.Drawing.Point(153, 120);
            this.PH_CurrentLoanNumberText.Name = "PH_CurrentLoanNumberText";
            this.PH_CurrentLoanNumberText.Size = new System.Drawing.Size(83, 13);
            this.PH_CurrentLoanNumberText.TabIndex = 186;
            this.PH_CurrentLoanNumberText.Text = "123456";
            this.PH_CurrentLoanNumberText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_OrigLoanNumberLabel2
            // 
            this.PH_OrigLoanNumberLabel2.BackColor = System.Drawing.Color.Transparent;
            this.PH_OrigLoanNumberLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_OrigLoanNumberLabel2.ForeColor = System.Drawing.Color.Black;
            this.PH_OrigLoanNumberLabel2.Location = new System.Drawing.Point(7, 68);
            this.PH_OrigLoanNumberLabel2.Name = "PH_OrigLoanNumberLabel2";
            this.PH_OrigLoanNumberLabel2.Size = new System.Drawing.Size(142, 13);
            this.PH_OrigLoanNumberLabel2.TabIndex = 183;
            this.PH_OrigLoanNumberLabel2.Text = "Original Loan Number:";
            this.PH_OrigLoanNumberLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_OrigLoanNumberText
            // 
            this.PH_OrigLoanNumberText.BackColor = System.Drawing.Color.Transparent;
            this.PH_OrigLoanNumberText.ForeColor = System.Drawing.Color.Black;
            this.PH_OrigLoanNumberText.Location = new System.Drawing.Point(153, 68);
            this.PH_OrigLoanNumberText.Name = "PH_OrigLoanNumberText";
            this.PH_OrigLoanNumberText.Size = new System.Drawing.Size(83, 13);
            this.PH_OrigLoanNumberText.TabIndex = 184;
            this.PH_OrigLoanNumberText.Text = "123456";
            this.PH_OrigLoanNumberText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_PrevLoanNumberLabel
            // 
            this.PH_PrevLoanNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_PrevLoanNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_PrevLoanNumberLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_PrevLoanNumberLabel.Location = new System.Drawing.Point(7, 94);
            this.PH_PrevLoanNumberLabel.Name = "PH_PrevLoanNumberLabel";
            this.PH_PrevLoanNumberLabel.Size = new System.Drawing.Size(142, 13);
            this.PH_PrevLoanNumberLabel.TabIndex = 177;
            this.PH_PrevLoanNumberLabel.Text = "Previous Loan Number:";
            this.PH_PrevLoanNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_PrevLoanNumberText
            // 
            this.PH_PrevLoanNumberText.BackColor = System.Drawing.Color.Transparent;
            this.PH_PrevLoanNumberText.ForeColor = System.Drawing.Color.Black;
            this.PH_PrevLoanNumberText.Location = new System.Drawing.Point(153, 94);
            this.PH_PrevLoanNumberText.Name = "PH_PrevLoanNumberText";
            this.PH_PrevLoanNumberText.Size = new System.Drawing.Size(83, 13);
            this.PH_PrevLoanNumberText.TabIndex = 178;
            this.PH_PrevLoanNumberText.Text = "123456";
            this.PH_PrevLoanNumberText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ApprovedByLabel
            // 
            this.PH_ApprovedByLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ApprovedByLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ApprovedByLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ApprovedByLabel.Location = new System.Drawing.Point(391, 120);
            this.PH_ApprovedByLabel.Name = "PH_ApprovedByLabel";
            this.PH_ApprovedByLabel.Size = new System.Drawing.Size(90, 13);
            this.PH_ApprovedByLabel.TabIndex = 175;
            this.PH_ApprovedByLabel.Text = "Approved By:";
            this.PH_ApprovedByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ApprovedByValue
            // 
            this.PH_ApprovedByValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_ApprovedByValue.ForeColor = System.Drawing.Color.Black;
            this.PH_ApprovedByValue.Location = new System.Drawing.Point(487, 120);
            this.PH_ApprovedByValue.Name = "PH_ApprovedByValue";
            this.PH_ApprovedByValue.Size = new System.Drawing.Size(70, 13);
            this.PH_ApprovedByValue.TabIndex = 176;
            this.PH_ApprovedByValue.Text = "D Dilbert";
            this.PH_ApprovedByValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_LoanStatusLabel
            // 
            this.PH_LoanStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_LoanStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_LoanStatusLabel.Location = new System.Drawing.Point(341, 42);
            this.PH_LoanStatusLabel.Name = "PH_LoanStatusLabel";
            this.PH_LoanStatusLabel.Size = new System.Drawing.Size(140, 13);
            this.PH_LoanStatusLabel.TabIndex = 173;
            this.PH_LoanStatusLabel.Text = "Customer Loan Status:";
            this.PH_LoanStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_LoanStatusValue
            // 
            this.PH_LoanStatusValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanStatusValue.ForeColor = System.Drawing.Color.Red;
            this.PH_LoanStatusValue.Location = new System.Drawing.Point(487, 42);
            this.PH_LoanStatusValue.Name = "PH_LoanStatusValue";
            this.PH_LoanStatusValue.Size = new System.Drawing.Size(70, 13);
            this.PH_LoanStatusValue.TabIndex = 174;
            this.PH_LoanStatusValue.Text = "Pickup";
            this.PH_LoanStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_TerminalIDLabel
            // 
            this.PH_TerminalIDLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_TerminalIDLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_TerminalIDLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_TerminalIDLabel.Location = new System.Drawing.Point(321, 16);
            this.PH_TerminalIDLabel.Name = "PH_TerminalIDLabel";
            this.PH_TerminalIDLabel.Size = new System.Drawing.Size(160, 13);
            this.PH_TerminalIDLabel.TabIndex = 167;
            this.PH_TerminalIDLabel.Text = "Terminal ID / Shop:";
            this.PH_TerminalIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_TerminalIDValue
            // 
            this.PH_TerminalIDValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_TerminalIDValue.ForeColor = System.Drawing.Color.Black;
            this.PH_TerminalIDValue.Location = new System.Drawing.Point(487, 16);
            this.PH_TerminalIDValue.Name = "PH_TerminalIDValue";
            this.PH_TerminalIDValue.Size = new System.Drawing.Size(70, 13);
            this.PH_TerminalIDValue.TabIndex = 168;
            this.PH_TerminalIDValue.Text = "36 1234";
            this.PH_TerminalIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_MadeByEmpLabel
            // 
            this.PH_MadeByEmpLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_MadeByEmpLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_MadeByEmpLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_MadeByEmpLabel.Location = new System.Drawing.Point(391, 144);
            this.PH_MadeByEmpLabel.Name = "PH_MadeByEmpLabel";
            this.PH_MadeByEmpLabel.Size = new System.Drawing.Size(90, 13);
            this.PH_MadeByEmpLabel.TabIndex = 165;
            this.PH_MadeByEmpLabel.Text = "Made By Emp:";
            this.PH_MadeByEmpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_MadeByEmpValue
            // 
            this.PH_MadeByEmpValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_MadeByEmpValue.ForeColor = System.Drawing.Color.Black;
            this.PH_MadeByEmpValue.Location = new System.Drawing.Point(487, 144);
            this.PH_MadeByEmpValue.Name = "PH_MadeByEmpValue";
            this.PH_MadeByEmpValue.Size = new System.Drawing.Size(79, 13);
            this.PH_MadeByEmpValue.TabIndex = 166;
            this.PH_MadeByEmpValue.Text = "J Skootch";
            this.PH_MadeByEmpValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_LoanOrigFeeLabel
            // 
            this.PH_LoanOrigFeeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanOrigFeeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_LoanOrigFeeLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_LoanOrigFeeLabel.Location = new System.Drawing.Point(552, 94);
            this.PH_LoanOrigFeeLabel.Name = "PH_LoanOrigFeeLabel";
            this.PH_LoanOrigFeeLabel.Size = new System.Drawing.Size(134, 13);
            this.PH_LoanOrigFeeLabel.TabIndex = 163;
            this.PH_LoanOrigFeeLabel.Text = "Loan Origination Fee:";
            this.PH_LoanOrigFeeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_LoanOrigFeeValue
            // 
            this.PH_LoanOrigFeeValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanOrigFeeValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_LoanOrigFeeValue.ForeColor = System.Drawing.Color.Black;
            this.PH_LoanOrigFeeValue.Location = new System.Drawing.Point(692, 94);
            this.PH_LoanOrigFeeValue.Name = "PH_LoanOrigFeeValue";
            this.PH_LoanOrigFeeValue.Size = new System.Drawing.Size(61, 13);
            this.PH_LoanOrigFeeValue.TabIndex = 164;
            this.PH_LoanOrigFeeValue.Text = "$0.00";
            this.PH_LoanOrigFeeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ServiceChargeLabel
            // 
            this.PH_ServiceChargeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_ServiceChargeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_ServiceChargeLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_ServiceChargeLabel.Location = new System.Drawing.Point(578, 68);
            this.PH_ServiceChargeLabel.Name = "PH_ServiceChargeLabel";
            this.PH_ServiceChargeLabel.Size = new System.Drawing.Size(108, 13);
            this.PH_ServiceChargeLabel.TabIndex = 161;
            this.PH_ServiceChargeLabel.Text = "Service Charge:";
            this.PH_ServiceChargeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_ServiceChargeValue
            // 
            this.PH_ServiceChargeValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_ServiceChargeValue.ForeColor = System.Drawing.Color.Black;
            this.PH_ServiceChargeValue.Location = new System.Drawing.Point(692, 68);
            this.PH_ServiceChargeValue.Name = "PH_ServiceChargeValue";
            this.PH_ServiceChargeValue.Size = new System.Drawing.Size(79, 13);
            this.PH_ServiceChargeValue.TabIndex = 162;
            this.PH_ServiceChargeValue.Text = "$4.00 Per Mon";
            this.PH_ServiceChargeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_InterestLabel
            // 
            this.PH_InterestLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_InterestLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_InterestLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_InterestLabel.Location = new System.Drawing.Point(579, 42);
            this.PH_InterestLabel.Name = "PH_InterestLabel";
            this.PH_InterestLabel.Size = new System.Drawing.Size(108, 13);
            this.PH_InterestLabel.TabIndex = 159;
            this.PH_InterestLabel.Text = "Interest Charge:";
            this.PH_InterestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_InterestValue
            // 
            this.PH_InterestValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_InterestValue.ForeColor = System.Drawing.Color.Black;
            this.PH_InterestValue.Location = new System.Drawing.Point(693, 42);
            this.PH_InterestValue.Name = "PH_InterestValue";
            this.PH_InterestValue.Size = new System.Drawing.Size(79, 13);
            this.PH_InterestValue.TabIndex = 160;
            this.PH_InterestValue.Text = "$8.00";
            this.PH_InterestValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_LoanLabel
            // 
            this.PH_LoanLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_LoanLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_LoanLabel.Location = new System.Drawing.Point(578, 16);
            this.PH_LoanLabel.Name = "PH_LoanLabel";
            this.PH_LoanLabel.Size = new System.Drawing.Size(108, 13);
            this.PH_LoanLabel.TabIndex = 157;
            this.PH_LoanLabel.Text = "Principal Loan Amount:";
            this.PH_LoanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_LoanValue
            // 
            this.PH_LoanValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_LoanValue.ForeColor = System.Drawing.Color.Black;
            this.PH_LoanValue.Location = new System.Drawing.Point(692, 16);
            this.PH_LoanValue.Name = "PH_LoanValue";
            this.PH_LoanValue.Size = new System.Drawing.Size(79, 13);
            this.PH_LoanValue.TabIndex = 158;
            this.PH_LoanValue.Text = "$160.00";
            this.PH_LoanValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_NotesLabel
            // 
            this.PH_NotesLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_NotesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_NotesLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_NotesLabel.Location = new System.Drawing.Point(1, 198);
            this.PH_NotesLabel.Name = "PH_NotesLabel";
            this.PH_NotesLabel.Size = new System.Drawing.Size(148, 13);
            this.PH_NotesLabel.TabIndex = 155;
            this.PH_NotesLabel.Text = "Notes:";
            this.PH_NotesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_NotesValue
            // 
            this.PH_NotesValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_NotesValue.ForeColor = System.Drawing.Color.Black;
            this.PH_NotesValue.Location = new System.Drawing.Point(153, 198);
            this.PH_NotesValue.Name = "PH_NotesValue";
            this.PH_NotesValue.Size = new System.Drawing.Size(234, 13);
            this.PH_NotesValue.TabIndex = 156;
            this.PH_NotesValue.Text = "ROCK AND ROLL ISNT NOISE POLLUTION";
            this.PH_NotesValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_CustomerIdentificationLabel
            // 
            this.PH_CustomerIdentificationLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_CustomerIdentificationLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_CustomerIdentificationLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_CustomerIdentificationLabel.Location = new System.Drawing.Point(4, 172);
            this.PH_CustomerIdentificationLabel.Name = "PH_CustomerIdentificationLabel";
            this.PH_CustomerIdentificationLabel.Size = new System.Drawing.Size(145, 13);
            this.PH_CustomerIdentificationLabel.TabIndex = 153;
            this.PH_CustomerIdentificationLabel.Text = "Customer Identification:";
            this.PH_CustomerIdentificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_CustomerIdentificationValue
            // 
            this.PH_CustomerIdentificationValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_CustomerIdentificationValue.ForeColor = System.Drawing.Color.Black;
            this.PH_CustomerIdentificationValue.Location = new System.Drawing.Point(153, 172);
            this.PH_CustomerIdentificationValue.Name = "PH_CustomerIdentificationValue";
            this.PH_CustomerIdentificationValue.Size = new System.Drawing.Size(234, 13);
            this.PH_CustomerIdentificationValue.TabIndex = 154;
            this.PH_CustomerIdentificationValue.Text = "TX Driver\'s License 1234567890 exp 10/2012";
            this.PH_CustomerIdentificationValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_NotificationDateLabel
            // 
            this.PH_NotificationDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_NotificationDateLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_NotificationDateLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_NotificationDateLabel.Location = new System.Drawing.Point(351, 94);
            this.PH_NotificationDateLabel.Name = "PH_NotificationDateLabel";
            this.PH_NotificationDateLabel.Size = new System.Drawing.Size(130, 13);
            this.PH_NotificationDateLabel.TabIndex = 151;
            this.PH_NotificationDateLabel.Text = "PFI Notification Date:";
            this.PH_NotificationDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_NotificationDateValue
            // 
            this.PH_NotificationDateValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_NotificationDateValue.ForeColor = System.Drawing.Color.Black;
            this.PH_NotificationDateValue.Location = new System.Drawing.Point(485, 94);
            this.PH_NotificationDateValue.Name = "PH_NotificationDateValue";
            this.PH_NotificationDateValue.Size = new System.Drawing.Size(83, 13);
            this.PH_NotificationDateValue.TabIndex = 152;
            this.PH_NotificationDateValue.Text = "09/20/2008";
            this.PH_NotificationDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_EligibilityDateLabel
            // 
            this.PH_EligibilityDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_EligibilityDateLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_EligibilityDateLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_EligibilityDateLabel.Location = new System.Drawing.Point(351, 68);
            this.PH_EligibilityDateLabel.Name = "PH_EligibilityDateLabel";
            this.PH_EligibilityDateLabel.Size = new System.Drawing.Size(130, 13);
            this.PH_EligibilityDateLabel.TabIndex = 149;
            this.PH_EligibilityDateLabel.Text = "PFI Eligibility Date:";
            this.PH_EligibilityDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_EligibilityDateValue
            // 
            this.PH_EligibilityDateValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_EligibilityDateValue.ForeColor = System.Drawing.Color.Black;
            this.PH_EligibilityDateValue.Location = new System.Drawing.Point(485, 68);
            this.PH_EligibilityDateValue.Name = "PH_EligibilityDateValue";
            this.PH_EligibilityDateValue.Size = new System.Drawing.Size(83, 13);
            this.PH_EligibilityDateValue.TabIndex = 150;
            this.PH_EligibilityDateValue.Text = "09/20/2008";
            this.PH_EligibilityDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_DueDateLabel
            // 
            this.PH_DueDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_DueDateLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_DueDateLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_DueDateLabel.Location = new System.Drawing.Point(41, 42);
            this.PH_DueDateLabel.Name = "PH_DueDateLabel";
            this.PH_DueDateLabel.Size = new System.Drawing.Size(108, 13);
            this.PH_DueDateLabel.TabIndex = 124;
            this.PH_DueDateLabel.Text = "Due Date:";
            this.PH_DueDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_DueDateValue
            // 
            this.PH_DueDateValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_DueDateValue.ForeColor = System.Drawing.Color.Black;
            this.PH_DueDateValue.Location = new System.Drawing.Point(153, 42);
            this.PH_DueDateValue.Name = "PH_DueDateValue";
            this.PH_DueDateValue.Size = new System.Drawing.Size(83, 13);
            this.PH_DueDateValue.TabIndex = 126;
            this.PH_DueDateValue.Text = "09/20/2008";
            this.PH_DueDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_OriginationDateLabel
            // 
            this.PH_OriginationDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.PH_OriginationDateLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_OriginationDateLabel.ForeColor = System.Drawing.Color.Black;
            this.PH_OriginationDateLabel.Location = new System.Drawing.Point(7, 16);
            this.PH_OriginationDateLabel.Name = "PH_OriginationDateLabel";
            this.PH_OriginationDateLabel.Size = new System.Drawing.Size(142, 13);
            this.PH_OriginationDateLabel.TabIndex = 118;
            this.PH_OriginationDateLabel.Text = "Origination Date/Time:";
            this.PH_OriginationDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_OriginationDateValue
            // 
            this.PH_OriginationDateValue.BackColor = System.Drawing.Color.Transparent;
            this.PH_OriginationDateValue.ForeColor = System.Drawing.Color.Black;
            this.PH_OriginationDateValue.Location = new System.Drawing.Point(153, 16);
            this.PH_OriginationDateValue.Name = "PH_OriginationDateValue";
            this.PH_OriginationDateValue.Size = new System.Drawing.Size(83, 13);
            this.PH_OriginationDateValue.TabIndex = 121;
            this.PH_OriginationDateValue.Text = "09/20/2008";
            this.PH_OriginationDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_ItemsDataGridView
            // 
            this.PH_ItemsDataGridView.AllowUserToAddRows = false;
            this.PH_ItemsDataGridView.AllowUserToDeleteRows = false;
            this.PH_ItemsDataGridView.AllowUserToResizeColumns = false;
            this.PH_ItemsDataGridView.AllowUserToResizeRows = false;
            this.PH_ItemsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.PH_ItemsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.PH_ItemsDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.PH_ItemsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PH_ItemsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PH_ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PH_ItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PH_ItemNumberColumn,
            this.PH_DescriptionColumn,
            this.PH_LoanAmountColumn,
            this.PH_SuggestedAmountColumn,
            this.PH_LocationColumn});
            this.PH_ItemsDataGridView.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PH_ItemsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.PH_ItemsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PH_ItemsDataGridView.GridColor = System.Drawing.Color.LightGray;
            this.PH_ItemsDataGridView.Location = new System.Drawing.Point(11, 297);
            this.PH_ItemsDataGridView.MultiSelect = false;
            this.PH_ItemsDataGridView.Name = "PH_ItemsDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PH_ItemsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PH_ItemsDataGridView.RowHeadersVisible = false;
            this.PH_ItemsDataGridView.RowHeadersWidth = 20;
            this.PH_ItemsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PH_ItemsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.PH_ItemsDataGridView.RowTemplate.Height = 25;
            this.PH_ItemsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PH_ItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PH_ItemsDataGridView.Size = new System.Drawing.Size(776, 226);
            this.PH_ItemsDataGridView.TabIndex = 154;
            // 
            // PH_ItemNumberColumn
            // 
            this.PH_ItemNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PH_ItemNumberColumn.DataPropertyName = "PH_ItemNumberColumn";
            this.PH_ItemNumberColumn.HeaderText = "Item Number";
            this.PH_ItemNumberColumn.Name = "PH_ItemNumberColumn";
            this.PH_ItemNumberColumn.ReadOnly = true;
            this.PH_ItemNumberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // PH_DescriptionColumn
            // 
            this.PH_DescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PH_DescriptionColumn.DataPropertyName = "PH_DescriptionColumn";
            this.PH_DescriptionColumn.HeaderText = "Description";
            this.PH_DescriptionColumn.Name = "PH_DescriptionColumn";
            this.PH_DescriptionColumn.ReadOnly = true;
            // 
            // PH_LoanAmountColumn
            // 
            this.PH_LoanAmountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PH_LoanAmountColumn.DataPropertyName = "PH_LoanAmountColumn";
            this.PH_LoanAmountColumn.HeaderText = "Loan Amount";
            this.PH_LoanAmountColumn.Name = "PH_LoanAmountColumn";
            this.PH_LoanAmountColumn.ReadOnly = true;
            // 
            // PH_SuggestedAmountColumn
            // 
            this.PH_SuggestedAmountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PH_SuggestedAmountColumn.DataPropertyName = "PH_SuggestedAmountColumn";
            this.PH_SuggestedAmountColumn.HeaderText = "Suggested Amount";
            this.PH_SuggestedAmountColumn.Name = "PH_SuggestedAmountColumn";
            this.PH_SuggestedAmountColumn.ReadOnly = true;
            // 
            // PH_LocationColumn
            // 
            this.PH_LocationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PH_LocationColumn.DataPropertyName = "PH_LocationColumn";
            this.PH_LocationColumn.HeaderText = "Location";
            this.PH_LocationColumn.Name = "PH_LocationColumn";
            this.PH_LocationColumn.ReadOnly = true;
            // 
            // PH_CloseButton
            // 
            this.PH_CloseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PH_CloseButton.AutoSize = true;
            this.PH_CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.PH_CloseButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.PH_CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PH_CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PH_CloseButton.FlatAppearance.BorderSize = 0;
            this.PH_CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PH_CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PH_CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PH_CloseButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_CloseButton.ForeColor = System.Drawing.Color.White;
            this.PH_CloseButton.Location = new System.Drawing.Point(351, 526);
            this.PH_CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.PH_CloseButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.Name = "PH_CloseButton";
            this.PH_CloseButton.Size = new System.Drawing.Size(100, 50);
            this.PH_CloseButton.TabIndex = 156;
            this.PH_CloseButton.Text = "Close";
            this.PH_CloseButton.UseVisualStyleBackColor = false;
            this.PH_CloseButton.Click += new System.EventHandler(this.PH_CloseButton_Click);
            // 
            // ProductHistory_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(799, 585);
            this.ControlBox = false;
            this.Controls.Add(this.PH_CloseButton);
            this.Controls.Add(this.PH_ItemsDataGridView);
            this.Controls.Add(this.PH_LoanStatsLayoutPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductHistory_Dialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pawn Loan 323596 - Veronica Venagas";
            this.PH_LoanStatsLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PH_ItemsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Panel PH_LoanStatsLayoutPanel;
        private System.Windows.Forms.Label PH_DueDateLabel;
        private System.Windows.Forms.Label PH_DueDateValue;
        private System.Windows.Forms.Label PH_OriginationDateLabel;
        private System.Windows.Forms.Label PH_OriginationDateValue;
        private System.Windows.Forms.Label PH_NotificationDateLabel;
        private System.Windows.Forms.Label PH_NotificationDateValue;
        private System.Windows.Forms.Label PH_EligibilityDateLabel;
        private System.Windows.Forms.Label PH_EligibilityDateValue;
        private System.Windows.Forms.Label PH_NotesLabel;
        private System.Windows.Forms.Label PH_NotesValue;
        private System.Windows.Forms.Label PH_CustomerIdentificationLabel;
        private System.Windows.Forms.Label PH_CustomerIdentificationValue;
        private System.Windows.Forms.Label PH_InterestLabel;
        private System.Windows.Forms.Label PH_InterestValue;
        private System.Windows.Forms.Label PH_LoanLabel;
        private System.Windows.Forms.Label PH_LoanValue;
        private System.Windows.Forms.Label PH_LoanOrigFeeLabel;
        private System.Windows.Forms.Label PH_LoanOrigFeeValue;
        private System.Windows.Forms.Label PH_ServiceChargeLabel;
        private System.Windows.Forms.Label PH_ServiceChargeValue;
        private System.Windows.Forms.Label PH_MadeByEmpLabel;
        private System.Windows.Forms.Label PH_MadeByEmpValue;
        private System.Windows.Forms.Label PH_TerminalIDLabel;
        private System.Windows.Forms.Label PH_TerminalIDValue;
        private System.Windows.Forms.Label PH_LoanStatusLabel;
        private System.Windows.Forms.Label PH_LoanStatusValue;
        private System.Windows.Forms.Label PH_ApprovedByLabel;
        private System.Windows.Forms.Label PH_ApprovedByValue;
        private System.Windows.Forms.DataGridView PH_ItemsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PH_ItemNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PH_DescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PH_LoanAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PH_SuggestedAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PH_LocationColumn;
        private System.Windows.Forms.Button PH_CloseButton;
        private System.Windows.Forms.Label PH_PrevLoanNumberLabel;
        private System.Windows.Forms.Label PH_PrevLoanNumberText;
        private System.Windows.Forms.Label PH_OrigLoanNumberLabel2;
        private System.Windows.Forms.Label PH_OrigLoanNumberText;
        private System.Windows.Forms.Label PH_CurrentLoanNumberLabel;
        private System.Windows.Forms.Label PH_CurrentLoanNumberText;
        private System.Windows.Forms.Label PH_TotalLoanAmountLabel;
        private System.Windows.Forms.Label PH_TotalLoanAmountText;
    }
}