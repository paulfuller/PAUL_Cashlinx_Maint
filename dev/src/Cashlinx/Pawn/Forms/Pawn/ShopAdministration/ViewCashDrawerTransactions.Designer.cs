using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class ViewCashDrawerTransactions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewCashDrawerTransactions));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelCashDrawerHeading = new System.Windows.Forms.Label();
            this.lblSettlementDateHeading = new System.Windows.Forms.Label();
            this.customLabelTrandateHeading = new CustomLabel();
            this.customLabelViewHeading = new CustomLabel();
            this.comboBoxProducts = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.customButtonUpdateView = new CustomButton();
            this.customDataGridViewTransactions = new CustomDataGridView();
            this.EmpNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TranTim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TranNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TranType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodofpmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisbursedAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonLast = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonFirst = new System.Windows.Forms.Button();
            this.customLabelTotalHeading = new CustomLabel();
            this.customLabelTotReceiptAmt = new CustomLabel();
            this.customLabelTotDisbursedAmt = new CustomLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCashDrawerHeading
            // 
            this.labelCashDrawerHeading.AutoSize = true;
            this.labelCashDrawerHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelCashDrawerHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCashDrawerHeading.ForeColor = System.Drawing.Color.White;
            this.labelCashDrawerHeading.Location = new System.Drawing.Point(21, 23);
            this.labelCashDrawerHeading.Name = "labelCashDrawerHeading";
            this.labelCashDrawerHeading.Size = new System.Drawing.Size(116, 19);
            this.labelCashDrawerHeading.TabIndex = 0;
            this.labelCashDrawerHeading.Text = "Cash Drawer #";
            // 
            // lblSettlementDateHeading
            // 
            this.lblSettlementDateHeading.AutoSize = true;
            this.lblSettlementDateHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblSettlementDateHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettlementDateHeading.ForeColor = System.Drawing.Color.White;
            this.lblSettlementDateHeading.Location = new System.Drawing.Point(657, 23);
            this.lblSettlementDateHeading.Name = "lblSettlementDateHeading";
            this.lblSettlementDateHeading.Size = new System.Drawing.Size(127, 19);
            this.lblSettlementDateHeading.TabIndex = 1;
            this.lblSettlementDateHeading.Text = "Settlement Date:";
            // 
            // customLabelTrandateHeading
            // 
            this.customLabelTrandateHeading.AutoSize = true;
            this.customLabelTrandateHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTrandateHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTrandateHeading.Location = new System.Drawing.Point(22, 94);
            this.customLabelTrandateHeading.Name = "customLabelTrandateHeading";
            this.customLabelTrandateHeading.Size = new System.Drawing.Size(93, 13);
            this.customLabelTrandateHeading.TabIndex = 2;
            this.customLabelTrandateHeading.Text = "Transaction Date:";
            // 
            // customLabelViewHeading
            // 
            this.customLabelViewHeading.AutoSize = true;
            this.customLabelViewHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelViewHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelViewHeading.Location = new System.Drawing.Point(336, 93);
            this.customLabelViewHeading.Name = "customLabelViewHeading";
            this.customLabelViewHeading.Size = new System.Drawing.Size(33, 13);
            this.customLabelViewHeading.TabIndex = 3;
            this.customLabelViewHeading.Text = "View:";
            // 
            // comboBoxProducts
            // 
            this.comboBoxProducts.FormattingEnabled = true;
            this.comboBoxProducts.Location = new System.Drawing.Point(375, 90);
            this.comboBoxProducts.Name = "comboBoxProducts";
            this.comboBoxProducts.Size = new System.Drawing.Size(121, 21);
            this.comboBoxProducts.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(514, 91);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(656, 91);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 6;
            // 
            // customButtonUpdateView
            // 
            this.customButtonUpdateView.BackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdateView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonUpdateView.BackgroundImage")));
            this.customButtonUpdateView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonUpdateView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonUpdateView.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonUpdateView.FlatAppearance.BorderSize = 0;
            this.customButtonUpdateView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdateView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdateView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonUpdateView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonUpdateView.ForeColor = System.Drawing.Color.White;
            this.customButtonUpdateView.Location = new System.Drawing.Point(791, 81);
            this.customButtonUpdateView.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonUpdateView.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonUpdateView.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonUpdateView.Name = "customButtonUpdateView";
            this.customButtonUpdateView.Size = new System.Drawing.Size(100, 50);
            this.customButtonUpdateView.TabIndex = 7;
            this.customButtonUpdateView.Text = "Update View";
            this.customButtonUpdateView.UseVisualStyleBackColor = false;
            // 
            // customDataGridViewTransactions
            // 
            this.customDataGridViewTransactions.AllowUserToAddRows = false;
            this.customDataGridViewTransactions.AllowUserToDeleteRows = false;
            this.customDataGridViewTransactions.AllowUserToResizeColumns = false;
            this.customDataGridViewTransactions.AllowUserToResizeRows = false;
            this.customDataGridViewTransactions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewTransactions.CausesValidation = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.customDataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmpNumber,
            this.TranTim,
            this.TranNumber,
            this.ReceiptNumber,
            this.TranType,
            this.CustomerName,
            this.methodofpmt,
            this.ReceiptAmount,
            this.DisbursedAmount});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle8;
            this.customDataGridViewTransactions.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewTransactions.Location = new System.Drawing.Point(25, 132);
            this.customDataGridViewTransactions.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewTransactions.Name = "customDataGridViewTransactions";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.customDataGridViewTransactions.RowHeadersVisible = false;
            this.customDataGridViewTransactions.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.customDataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewTransactions.Size = new System.Drawing.Size(866, 225);
            this.customDataGridViewTransactions.TabIndex = 8;
            // 
            // EmpNumber
            // 
            this.EmpNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.EmpNumber.HeaderText = "Emp. Number";
            this.EmpNumber.Name = "EmpNumber";
            this.EmpNumber.Width = 88;
            // 
            // TranTim
            // 
            this.TranTim.HeaderText = "Tran. Time";
            this.TranTim.Name = "TranTim";
            this.TranTim.Width = 70;
            // 
            // TranNumber
            // 
            this.TranNumber.HeaderText = "Tran. Number";
            this.TranNumber.Name = "TranNumber";
            this.TranNumber.Width = 70;
            // 
            // ReceiptNumber
            // 
            this.ReceiptNumber.HeaderText = "Receipt Number";
            this.ReceiptNumber.Name = "ReceiptNumber";
            // 
            // TranType
            // 
            this.TranType.HeaderText = "Tran. Type";
            this.TranType.Name = "TranType";
            this.TranType.Width = 70;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Width = 150;
            // 
            // methodofpmt
            // 
            this.methodofpmt.HeaderText = "Method Of Pmt.";
            this.methodofpmt.Name = "methodofpmt";
            // 
            // ReceiptAmount
            // 
            this.ReceiptAmount.HeaderText = "Receipt Amount";
            this.ReceiptAmount.Name = "ReceiptAmount";
            // 
            // DisbursedAmount
            // 
            this.DisbursedAmount.HeaderText = "Disbursed Amount";
            this.DisbursedAmount.Name = "DisbursedAmount";
            // 
            // buttonLast
            // 
            this.buttonLast.BackColor = System.Drawing.Color.Transparent;
            this.buttonLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonLast.BackgroundImage")));
            this.buttonLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLast.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonLast.FlatAppearance.BorderSize = 0;
            this.buttonLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLast.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLast.ForeColor = System.Drawing.Color.White;
            this.buttonLast.Location = new System.Drawing.Point(526, 369);
            this.buttonLast.Name = "buttonLast";
            this.buttonLast.Size = new System.Drawing.Size(60, 21);
            this.buttonLast.TabIndex = 62;
            this.buttonLast.Text = "Last";
            this.buttonLast.UseVisualStyleBackColor = false;
            // 
            // buttonNext
            // 
            this.buttonNext.BackColor = System.Drawing.Color.Transparent;
            this.buttonNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNext.BackgroundImage")));
            this.buttonNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNext.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonNext.FlatAppearance.BorderSize = 0;
            this.buttonNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.ForeColor = System.Drawing.Color.White;
            this.buttonNext.Location = new System.Drawing.Point(460, 369);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(60, 21);
            this.buttonNext.TabIndex = 61;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = false;
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.BackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPrevious.BackgroundImage")));
            this.buttonPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPrevious.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonPrevious.FlatAppearance.BorderSize = 0;
            this.buttonPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevious.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrevious.ForeColor = System.Drawing.Color.White;
            this.buttonPrevious.Location = new System.Drawing.Point(394, 369);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(60, 21);
            this.buttonPrevious.TabIndex = 60;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = false;
            // 
            // buttonFirst
            // 
            this.buttonFirst.BackColor = System.Drawing.Color.Transparent;
            this.buttonFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonFirst.BackgroundImage")));
            this.buttonFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFirst.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonFirst.FlatAppearance.BorderSize = 0;
            this.buttonFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFirst.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFirst.ForeColor = System.Drawing.Color.White;
            this.buttonFirst.Location = new System.Drawing.Point(328, 369);
            this.buttonFirst.Name = "buttonFirst";
            this.buttonFirst.Size = new System.Drawing.Size(60, 21);
            this.buttonFirst.TabIndex = 59;
            this.buttonFirst.Text = "First";
            this.buttonFirst.UseVisualStyleBackColor = false;
            // 
            // customLabelTotalHeading
            // 
            this.customLabelTotalHeading.AutoSize = true;
            this.customLabelTotalHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTotalHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTotalHeading.Location = new System.Drawing.Point(641, 406);
            this.customLabelTotalHeading.Name = "customLabelTotalHeading";
            this.customLabelTotalHeading.Size = new System.Drawing.Size(35, 13);
            this.customLabelTotalHeading.TabIndex = 63;
            this.customLabelTotalHeading.Text = "Total:";
            // 
            // customLabelTotReceiptAmt
            // 
            this.customLabelTotReceiptAmt.AutoSize = true;
            this.customLabelTotReceiptAmt.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTotReceiptAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTotReceiptAmt.Location = new System.Drawing.Point(703, 406);
            this.customLabelTotReceiptAmt.Name = "customLabelTotReceiptAmt";
            this.customLabelTotReceiptAmt.Size = new System.Drawing.Size(35, 13);
            this.customLabelTotReceiptAmt.TabIndex = 64;
            this.customLabelTotReceiptAmt.Text = "$0.00";
            // 
            // customLabelTotDisbursedAmt
            // 
            this.customLabelTotDisbursedAmt.AutoSize = true;
            this.customLabelTotDisbursedAmt.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTotDisbursedAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTotDisbursedAmt.Location = new System.Drawing.Point(788, 404);
            this.customLabelTotDisbursedAmt.Name = "customLabelTotDisbursedAmt";
            this.customLabelTotDisbursedAmt.Size = new System.Drawing.Size(35, 13);
            this.customLabelTotDisbursedAmt.TabIndex = 65;
            this.customLabelTotDisbursedAmt.Text = "$0.00";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(691, 398);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(70, 2);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(788, 399);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(70, 2);
            this.groupBox2.TabIndex = 67;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // ViewCashDrawerTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 476);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.customLabelTotDisbursedAmt);
            this.Controls.Add(this.customLabelTotReceiptAmt);
            this.Controls.Add(this.customLabelTotalHeading);
            this.Controls.Add(this.buttonLast);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonFirst);
            this.Controls.Add(this.customDataGridViewTransactions);
            this.Controls.Add(this.customButtonUpdateView);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBoxProducts);
            this.Controls.Add(this.customLabelViewHeading);
            this.Controls.Add(this.customLabelTrandateHeading);
            this.Controls.Add(this.lblSettlementDateHeading);
            this.Controls.Add(this.labelCashDrawerHeading);
            this.Name = "ViewCashDrawerTransactions";
            this.Text = "ViewCashDrawerTransactions";
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCashDrawerHeading;
        private System.Windows.Forms.Label lblSettlementDateHeading;
        private CustomLabel customLabelTrandateHeading;
        private CustomLabel customLabelViewHeading;
        private System.Windows.Forms.ComboBox comboBoxProducts;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private CustomButton customButtonUpdateView;
        private CustomDataGridView customDataGridViewTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranTim;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TranType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodofpmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisbursedAmount;
        private System.Windows.Forms.Button buttonLast;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonFirst;
        private CustomLabel customLabelTotalHeading;
        private CustomLabel customLabelTotReceiptAmt;
        private CustomLabel customLabelTotDisbursedAmt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}