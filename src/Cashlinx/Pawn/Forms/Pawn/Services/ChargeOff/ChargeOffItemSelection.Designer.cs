using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    partial class ChargeOffItemSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelLoan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCustName = new System.Windows.Forms.Label();
            this.labelCustAddr = new System.Windows.Forms.Label();
            this.labelCustAddr2 = new System.Windows.Forms.Label();
            this.labelCustAddr3 = new System.Windows.Forms.Label();
            this.customDataGridViewItems = new CustomDataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTotalLoanAmount = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.mdseDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loanAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewItems)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLoan
            // 
            this.labelLoan.AutoSize = true;
            this.labelLoan.BackColor = System.Drawing.Color.Transparent;
            this.labelLoan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoan.ForeColor = System.Drawing.Color.White;
            this.labelLoan.Location = new System.Drawing.Point(34, 23);
            this.labelLoan.Name = "labelLoan";
            this.labelLoan.Size = new System.Drawing.Size(91, 16);
            this.labelLoan.TabIndex = 0;
            this.labelLoan.Text = "Loan 123123";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(164, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Item(s) being replaced";
            // 
            // labelCustName
            // 
            this.labelCustName.AutoSize = true;
            this.labelCustName.BackColor = System.Drawing.Color.Transparent;
            this.labelCustName.Location = new System.Drawing.Point(37, 133);
            this.labelCustName.Name = "labelCustName";
            this.labelCustName.Size = new System.Drawing.Size(81, 13);
            this.labelCustName.TabIndex = 2;
            this.labelCustName.Text = "Wages, John Q";
            // 
            // labelCustAddr
            // 
            this.labelCustAddr.AutoSize = true;
            this.labelCustAddr.BackColor = System.Drawing.Color.Transparent;
            this.labelCustAddr.Location = new System.Drawing.Point(34, 154);
            this.labelCustAddr.Name = "labelCustAddr";
            this.labelCustAddr.Size = new System.Drawing.Size(125, 13);
            this.labelCustAddr.TabIndex = 3;
            this.labelCustAddr.Text = "10001 North Main Street";
            // 
            // labelCustAddr2
            // 
            this.labelCustAddr2.AutoSize = true;
            this.labelCustAddr2.BackColor = System.Drawing.Color.Transparent;
            this.labelCustAddr2.Location = new System.Drawing.Point(37, 176);
            this.labelCustAddr2.Name = "labelCustAddr2";
            this.labelCustAddr2.Size = new System.Drawing.Size(46, 13);
            this.labelCustAddr2.TabIndex = 4;
            this.labelCustAddr2.Text = "Apt A19";
            // 
            // labelCustAddr3
            // 
            this.labelCustAddr3.AutoSize = true;
            this.labelCustAddr3.BackColor = System.Drawing.Color.Transparent;
            this.labelCustAddr3.Location = new System.Drawing.Point(39, 200);
            this.labelCustAddr3.Name = "labelCustAddr3";
            this.labelCustAddr3.Size = new System.Drawing.Size(87, 13);
            this.labelCustAddr3.TabIndex = 5;
            this.labelCustAddr3.Text = "Dallas, TX 75000";
            // 
            // customDataGridViewItems
            // 
            this.customDataGridViewItems.AllowUserToAddRows = false;
            this.customDataGridViewItems.AllowUserToDeleteRows = false;
            this.customDataGridViewItems.AllowUserToResizeColumns = false;
            this.customDataGridViewItems.AllowUserToResizeRows = false;
            this.customDataGridViewItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewItems.CausesValidation = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.customDataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mdseDesc,
            this.loanAmount,
            this.icn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewItems.DefaultCellStyle = dataGridViewCellStyle5;
            this.customDataGridViewItems.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewItems.Location = new System.Drawing.Point(37, 259);
            this.customDataGridViewItems.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewItems.Name = "customDataGridViewItems";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.customDataGridViewItems.RowHeadersVisible = false;
            this.customDataGridViewItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewItems.Size = new System.Drawing.Size(606, 150);
            this.customDataGridViewItems.TabIndex = 6;
            this.customDataGridViewItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridViewItems_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(439, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Loan Amount";
            // 
            // labelTotalLoanAmount
            // 
            this.labelTotalLoanAmount.AutoSize = true;
            this.labelTotalLoanAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalLoanAmount.Location = new System.Drawing.Point(557, 432);
            this.labelTotalLoanAmount.Name = "labelTotalLoanAmount";
            this.labelTotalLoanAmount.Size = new System.Drawing.Size(47, 13);
            this.labelTotalLoanAmount.TabIndex = 8;
            this.labelTotalLoanAmount.Text = "$250.00";
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonBack.AutoSize = true;
            this.buttonBack.BackColor = System.Drawing.Color.Transparent;
            this.buttonBack.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(37, 471);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 40);
            this.buttonBack.TabIndex = 146;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonOK.AutoSize = true;
            this.buttonOK.BackColor = System.Drawing.Color.Transparent;
            this.buttonOK.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.Location = new System.Drawing.Point(528, 471);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 40);
            this.buttonOK.TabIndex = 147;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // mdseDesc
            // 
            this.mdseDesc.HeaderText = "Merchandise Description";
            this.mdseDesc.Name = "mdseDesc";
            this.mdseDesc.ReadOnly = true;
            this.mdseDesc.Width = 500;
            // 
            // loanAmount
            // 
            this.loanAmount.HeaderText = "Loan Amount";
            this.loanAmount.Name = "loanAmount";
            this.loanAmount.ReadOnly = true;
            // 
            // icn
            // 
            this.icn.HeaderText = "ICN";
            this.icn.Name = "icn";
            this.icn.ReadOnly = true;
            this.icn.Visible = false;
            // 
            // ChargeOffItemSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 523);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.labelTotalLoanAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customDataGridViewItems);
            this.Controls.Add(this.labelCustAddr3);
            this.Controls.Add(this.labelCustAddr2);
            this.Controls.Add(this.labelCustAddr);
            this.Controls.Add(this.labelCustName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelLoan);
            this.Name = "ChargeOffItemSelection";
            this.Text = "ChargeOffItemSelection";
            this.Load += new System.EventHandler(this.ChargeOffItemSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLoan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCustName;
        private System.Windows.Forms.Label labelCustAddr;
        private System.Windows.Forms.Label labelCustAddr2;
        private System.Windows.Forms.Label labelCustAddr3;
        private CustomDataGridView customDataGridViewItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTotalLoanAmount;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn loanAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
    }
}