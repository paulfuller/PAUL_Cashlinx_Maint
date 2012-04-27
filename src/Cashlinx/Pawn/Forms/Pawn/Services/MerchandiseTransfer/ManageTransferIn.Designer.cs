using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class ManageTransferIn
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
            this.label1 = new System.Windows.Forms.Label();
            this.gvTransfers = new CustomDataGridView();
            this.colShop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransferType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransferNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatusDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalNumberOfItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnRejectTransfer = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransfers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(255, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Merchandise Transfer In";
            // 
            // gvTransfers
            // 
            this.gvTransfers.AllowUserToAddRows = false;
            this.gvTransfers.AllowUserToDeleteRows = false;
            this.gvTransfers.AllowUserToResizeColumns = false;
            this.gvTransfers.AllowUserToResizeRows = false;
            this.gvTransfers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvTransfers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvTransfers.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvTransfers.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTransfers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvTransfers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTransfers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShop,
            this.colTransferType,
            this.colStatus,
            this.colTransferNumber,
            this.colStatusDate,
            this.colTotalNumberOfItems,
            this.colTotalCost});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTransfers.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvTransfers.GridColor = System.Drawing.Color.LightGray;
            this.gvTransfers.Location = new System.Drawing.Point(12, 67);
            this.gvTransfers.Margin = new System.Windows.Forms.Padding(0);
            this.gvTransfers.MultiSelect = false;
            this.gvTransfers.Name = "gvTransfers";
            this.gvTransfers.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransfers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvTransfers.RowHeadersVisible = false;
            this.gvTransfers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTransfers.ShowEditingIcon = false;
            this.gvTransfers.Size = new System.Drawing.Size(706, 387);
            this.gvTransfers.TabIndex = 1;
            this.gvTransfers.GridViewRowSelected += new System.EventHandler<GridViewRowSelectedEventArgs>(this.gvTransfers_GridViewRowSelected);
            this.gvTransfers.SelectionChanged += new System.EventHandler(this.gvTransfers_SelectionChanged);
            // 
            // colShop
            // 
            this.colShop.HeaderText = "Shop";
            this.colShop.Name = "colShop";
            this.colShop.ReadOnly = true;
            // 
            // colTransferType
            // 
            this.colTransferType.HeaderText = "Transfer Type";
            this.colTransferType.Name = "colTransferType";
            this.colTransferType.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colTransferNumber
            // 
            this.colTransferNumber.HeaderText = "Transfer Number";
            this.colTransferNumber.Name = "colTransferNumber";
            this.colTransferNumber.ReadOnly = true;
            // 
            // colStatusDate
            // 
            this.colStatusDate.HeaderText = "Status Date";
            this.colStatusDate.Name = "colStatusDate";
            this.colStatusDate.ReadOnly = true;
            // 
            // colTotalNumberOfItems
            // 
            this.colTotalNumberOfItems.HeaderText = "Total # Items";
            this.colTotalNumberOfItems.Name = "colTotalNumberOfItems";
            this.colTotalNumberOfItems.ReadOnly = true;
            // 
            // colTotalCost
            // 
            this.colTotalCost.HeaderText = "Total Cost";
            this.colTotalCost.Name = "colTotalCost";
            this.colTotalCost.ReadOnly = true;
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.AutoSize = true;
            this.btnContinue.BackColor = System.Drawing.Color.Transparent;
            this.btnContinue.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnContinue.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(618, 460);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(100, 40);
            this.btnContinue.TabIndex = 145;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnRejectTransfer
            // 
            this.btnRejectTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRejectTransfer.AutoSize = true;
            this.btnRejectTransfer.BackColor = System.Drawing.Color.Transparent;
            this.btnRejectTransfer.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnRejectTransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRejectTransfer.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnRejectTransfer.FlatAppearance.BorderSize = 0;
            this.btnRejectTransfer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRejectTransfer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRejectTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejectTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRejectTransfer.ForeColor = System.Drawing.Color.White;
            this.btnRejectTransfer.Location = new System.Drawing.Point(492, 460);
            this.btnRejectTransfer.Name = "btnRejectTransfer";
            this.btnRejectTransfer.Size = new System.Drawing.Size(129, 40);
            this.btnRejectTransfer.TabIndex = 146;
            this.btnRejectTransfer.Text = "Reject Transfer";
            this.btnRejectTransfer.UseVisualStyleBackColor = false;
            this.btnRejectTransfer.Click += new System.EventHandler(this.btnRejectTransfer_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.AutoSize = true;
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(396, 460);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 40);
            this.btnPrint.TabIndex = 147;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(12, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 148;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ManageTransferIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.ClientSize = new System.Drawing.Size(730, 512);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnRejectTransfer);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.gvTransfers);
            this.Controls.Add(this.label1);
            this.Name = "ManageTransferIn";
            this.Text = "ManageTransferIn";
            this.Load += new System.EventHandler(this.ManageTransferIn_Load);
            this.Shown += new System.EventHandler(this.ManageTransferIn_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gvTransfers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CustomDataGridView gvTransfers;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnRejectTransfer;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShop;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransferType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransferNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatusDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalNumberOfItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalCost;
    }
}
