using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;

namespace Pawn.Forms.Layaway
{
    partial class LayawayForfeitureResults
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
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.gvEligibleLayaways = new CustomDataGridView();
            this.colTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateMade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastPaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastPaymentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaidIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTransactionDate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotalEligible = new System.Windows.Forms.Label();
            this.lblTotalSelected = new System.Windows.Forms.Label();
            this.lblTotalEligibleValue = new System.Windows.Forms.Label();
            this.lblTotalSelectedValue = new System.Windows.Forms.Label();
            this.lblWarningMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvEligibleLayaways)).BeginInit();
            this.SuspendLayout();
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComplete.AutoSize = true;
            this.btnComplete.BackColor = System.Drawing.Color.Transparent;
            this.btnComplete.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnComplete.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnComplete.FlatAppearance.BorderSize = 0;
            this.btnComplete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(688, 448);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(100, 40);
            this.btnComplete.TabIndex = 8;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.btnCancel.Location = new System.Drawing.Point(12, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.AutoSize = true;
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(118, 448);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.AutoSize = true;
            this.btnSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectAll.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnSelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnSelectAll.FlatAppearance.BorderSize = 0;
            this.btnSelectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.ForeColor = System.Drawing.Color.White;
            this.btnSelectAll.Location = new System.Drawing.Point(12, 393);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(100, 40);
            this.btnSelectAll.TabIndex = 11;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeselectAll.AutoSize = true;
            this.btnDeselectAll.BackColor = System.Drawing.Color.Transparent;
            this.btnDeselectAll.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnDeselectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeselectAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnDeselectAll.FlatAppearance.BorderSize = 0;
            this.btnDeselectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeselectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeselectAll.ForeColor = System.Drawing.Color.White;
            this.btnDeselectAll.Location = new System.Drawing.Point(118, 393);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(100, 40);
            this.btnDeselectAll.TabIndex = 12;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = false;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.AutoSize = true;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(476, 448);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 40);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDetail.AutoSize = true;
            this.btnDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnDetail.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnDetail.FlatAppearance.BorderSize = 0;
            this.btnDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetail.ForeColor = System.Drawing.Color.White;
            this.btnDetail.Location = new System.Drawing.Point(582, 448);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(100, 40);
            this.btnDetail.TabIndex = 14;
            this.btnDetail.Text = "Detail";
            this.btnDetail.UseVisualStyleBackColor = false;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // gvEligibleLayaways
            // 
            this.gvEligibleLayaways.AllowUserToAddRows = false;
            this.gvEligibleLayaways.AllowUserToDeleteRows = false;
            this.gvEligibleLayaways.AllowUserToResizeColumns = false;
            this.gvEligibleLayaways.AllowUserToResizeRows = false;
            this.gvEligibleLayaways.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvEligibleLayaways.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvEligibleLayaways.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvEligibleLayaways.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEligibleLayaways.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEligibleLayaways.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEligibleLayaways.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTicket,
            this.colCustomer,
            this.colDateMade,
            this.colAmount,
            this.colLastPaymentDate,
            this.colLastPaymentAmount,
            this.colPaidIn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEligibleLayaways.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvEligibleLayaways.GridColor = System.Drawing.Color.LightGray;
            this.gvEligibleLayaways.Location = new System.Drawing.Point(12, 106);
            this.gvEligibleLayaways.Margin = new System.Windows.Forms.Padding(0);
            this.gvEligibleLayaways.Name = "gvEligibleLayaways";
            this.gvEligibleLayaways.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEligibleLayaways.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvEligibleLayaways.RowHeadersVisible = false;
            this.gvEligibleLayaways.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvEligibleLayaways.ShowEditingIcon = false;
            this.gvEligibleLayaways.Size = new System.Drawing.Size(776, 281);
            this.gvEligibleLayaways.TabIndex = 15;
            this.gvEligibleLayaways.GridViewRowSelecting += new System.EventHandler<GridViewRowSelectingEventArgs>(this.gvEligibleLayaways_GridViewRowSelecting);
            this.gvEligibleLayaways.SelectionChanged += new System.EventHandler(this.gvEligibleLayaways_SelectionChanged);
            // 
            // colTicket
            // 
            this.colTicket.HeaderText = "Ticket";
            this.colTicket.Name = "colTicket";
            this.colTicket.ReadOnly = true;
            // 
            // colCustomer
            // 
            this.colCustomer.HeaderText = "Customer";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.ReadOnly = true;
            // 
            // colDateMade
            // 
            this.colDateMade.HeaderText = "Date Made";
            this.colDateMade.Name = "colDateMade";
            this.colDateMade.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // colLastPaymentDate
            // 
            this.colLastPaymentDate.HeaderText = "Last Payment Date";
            this.colLastPaymentDate.Name = "colLastPaymentDate";
            this.colLastPaymentDate.ReadOnly = true;
            // 
            // colLastPaymentAmount
            // 
            this.colLastPaymentAmount.HeaderText = "Last Payment Amount";
            this.colLastPaymentAmount.Name = "colLastPaymentAmount";
            this.colLastPaymentAmount.ReadOnly = true;
            // 
            // colPaidIn
            // 
            this.colPaidIn.HeaderText = "Paid In";
            this.colPaidIn.Name = "colPaidIn";
            this.colPaidIn.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 440);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 2);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.AutoSize = true;
            this.lblTransactionDate.BackColor = System.Drawing.Color.Transparent;
            this.lblTransactionDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionDate.Location = new System.Drawing.Point(12, 59);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Size = new System.Drawing.Size(354, 17);
            this.lblTransactionDate.TabIndex = 17;
            this.lblTransactionDate.Text = "Select Eligible Record to Forfeit As Of 10/06/2010";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 2);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // lblTotalEligible
            // 
            this.lblTotalEligible.AutoSize = true;
            this.lblTotalEligible.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalEligible.Location = new System.Drawing.Point(634, 62);
            this.lblTotalEligible.Name = "lblTotalEligible";
            this.lblTotalEligible.Size = new System.Drawing.Size(118, 13);
            this.lblTotalEligible.TabIndex = 19;
            this.lblTotalEligible.Text = "Total Eligible to Forfeit:";
            // 
            // lblTotalSelected
            // 
            this.lblTotalSelected.AutoSize = true;
            this.lblTotalSelected.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalSelected.Location = new System.Drawing.Point(625, 81);
            this.lblTotalSelected.Name = "lblTotalSelected";
            this.lblTotalSelected.Size = new System.Drawing.Size(127, 13);
            this.lblTotalSelected.TabIndex = 20;
            this.lblTotalSelected.Text = "Total Selected to Forfeit:";
            // 
            // lblTotalEligibleValue
            // 
            this.lblTotalEligibleValue.AutoSize = true;
            this.lblTotalEligibleValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalEligibleValue.Location = new System.Drawing.Point(758, 62);
            this.lblTotalEligibleValue.Name = "lblTotalEligibleValue";
            this.lblTotalEligibleValue.Size = new System.Drawing.Size(13, 13);
            this.lblTotalEligibleValue.TabIndex = 21;
            this.lblTotalEligibleValue.Text = "3";
            // 
            // lblTotalSelectedValue
            // 
            this.lblTotalSelectedValue.AutoSize = true;
            this.lblTotalSelectedValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalSelectedValue.Location = new System.Drawing.Point(758, 81);
            this.lblTotalSelectedValue.Name = "lblTotalSelectedValue";
            this.lblTotalSelectedValue.Size = new System.Drawing.Size(13, 13);
            this.lblTotalSelectedValue.TabIndex = 22;
            this.lblTotalSelectedValue.Text = "2";
            // 
            // lblWarningMessage
            // 
            this.lblWarningMessage.AutoSize = true;
            this.lblWarningMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblWarningMessage.ForeColor = System.Drawing.Color.Red;
            this.lblWarningMessage.Location = new System.Drawing.Point(12, 82);
            this.lblWarningMessage.Name = "lblWarningMessage";
            this.lblWarningMessage.Size = new System.Drawing.Size(92, 13);
            this.lblWarningMessage.TabIndex = 23;
            this.lblWarningMessage.Text = "Warning Message";
            // 
            // LayawayForfeitureResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_512_BlueScale;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.lblWarningMessage);
            this.Controls.Add(this.lblTotalSelectedValue);
            this.Controls.Add(this.lblTotalEligibleValue);
            this.Controls.Add(this.lblTotalSelected);
            this.Controls.Add(this.lblTotalEligible);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTransactionDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gvEligibleLayaways);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDeselectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnComplete);
            this.Name = "LayawayForfeitureResults";
            this.Text = "Layaway Forfeiture Results";
            this.Shown += new System.EventHandler(this.LayawayForfeitureSearch_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gvEligibleLayaways)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDetail;
        private CustomDataGridView gvEligibleLayaways;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTransactionDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTotalEligible;
        private System.Windows.Forms.Label lblTotalSelected;
        private System.Windows.Forms.Label lblTotalEligibleValue;
        private System.Windows.Forms.Label lblTotalSelectedValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTicket;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateMade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastPaymentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastPaymentAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaidIn;
        private System.Windows.Forms.Label lblWarningMessage;
    }
}