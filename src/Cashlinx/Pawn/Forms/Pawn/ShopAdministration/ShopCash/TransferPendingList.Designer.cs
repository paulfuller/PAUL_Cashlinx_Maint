using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    partial class TransferPendingList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferPendingList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTransfers = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonClose = new CustomButton();
            this.viewBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.transferNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shoptransferid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransfers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shop Transfer In";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(23, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Transfers";
            // 
            // dataGridViewTransfers
            // 
            this.dataGridViewTransfers.AllowUserToAddRows = false;
            this.dataGridViewTransfers.AllowUserToDeleteRows = false;
            this.dataGridViewTransfers.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewTransfers.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTransfers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTransfers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransfers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.viewBtn,
            this.transferNumber,
            this.transferDate,
            this.transferFrom,
            this.amount,
            this.status,
            this.shoptransferid});
            this.dataGridViewTransfers.GridColor = System.Drawing.Color.LightBlue;
            this.dataGridViewTransfers.Location = new System.Drawing.Point(26, 117);
            this.dataGridViewTransfers.Name = "dataGridViewTransfers";
            this.dataGridViewTransfers.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTransfers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTransfers.RowHeadersVisible = false;
            this.dataGridViewTransfers.Size = new System.Drawing.Size(614, 198);
            this.dataGridViewTransfers.TabIndex = 2;
            this.dataGridViewTransfers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransfers_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Number";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Transfer Date";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Transfer From";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Status";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // customButtonClose
            // 
            this.customButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonClose.BackgroundImage")));
            this.customButtonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClose.FlatAppearance.BorderSize = 0;
            this.customButtonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClose.ForeColor = System.Drawing.Color.White;
            this.customButtonClose.Location = new System.Drawing.Point(514, 369);
            this.customButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClose.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.Name = "customButtonClose";
            this.customButtonClose.Size = new System.Drawing.Size(100, 50);
            this.customButtonClose.TabIndex = 3;
            this.customButtonClose.Text = "Close";
            this.customButtonClose.UseVisualStyleBackColor = false;
            this.customButtonClose.Click += new System.EventHandler(this.customButtonClose_Click);
            // 
            // viewBtn
            // 
            this.viewBtn.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.viewBtn.Name = "viewBtn";
            this.viewBtn.ReadOnly = true;
            this.viewBtn.Text = "View";
            this.viewBtn.UseColumnTextForButtonValue = true;
            this.viewBtn.Width = 75;
            // 
            // transferNumber
            // 
            this.transferNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.transferNumber.HeaderText = "Number";
            this.transferNumber.Name = "transferNumber";
            this.transferNumber.ReadOnly = true;
            this.transferNumber.Width = 69;
            // 
            // transferDate
            // 
            this.transferDate.HeaderText = "Transfer Date";
            this.transferDate.Name = "transferDate";
            this.transferDate.ReadOnly = true;
            this.transferDate.Width = 120;
            // 
            // transferFrom
            // 
            this.transferFrom.HeaderText = "Transfer From";
            this.transferFrom.Name = "transferFrom";
            this.transferFrom.ReadOnly = true;
            // 
            // amount
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.amount.DefaultCellStyle = dataGridViewCellStyle2;
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // shoptransferid
            // 
            this.shoptransferid.HeaderText = "ShopTransferId";
            this.shoptransferid.Name = "shoptransferid";
            this.shoptransferid.ReadOnly = true;
            this.shoptransferid.Visible = false;
            // 
            // TransferPendingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(670, 444);
            this.Controls.Add(this.customButtonClose);
            this.Controls.Add(this.dataGridViewTransfers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransferPendingList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransferPendingList";
            this.Load += new System.EventHandler(this.TransferPendingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransfers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewTransfers;
        private CustomButton customButtonClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewButtonColumn viewBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoptransferid;
    }
}