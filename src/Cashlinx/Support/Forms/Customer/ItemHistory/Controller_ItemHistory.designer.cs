using Common.Libraries.Forms.Components;

namespace Support.Forms.Customer.ItemHistory
{
    partial class Controller_ItemHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Controller_ItemHistory));
            this.IH_CategoryComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IH_AddItemToNewPawnLoan = new System.Windows.Forms.Button();
            this.IH_ItemHistoryDataGridView = new System.Windows.Forms.DataGridView();
            this.IH_History_TransactionNumberColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.IH_History_StatusDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IH_History_ItemStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IH_History_ItemDescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IH_History_DocType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IH_History_TktNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonExit = new CustomButton();
            this.label3 = new System.Windows.Forms.Label();
            this.IH_ProductComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.IH_ItemHistoryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // IH_CategoryComboBox
            // 
            this.IH_CategoryComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.IH_CategoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IH_CategoryComboBox.ForeColor = System.Drawing.Color.Black;
            this.IH_CategoryComboBox.FormattingEnabled = true;
            this.IH_CategoryComboBox.Items.AddRange(new object[] {
            "Pawn",
            "Buy",
            "Sale"});
            this.IH_CategoryComboBox.Location = new System.Drawing.Point(753, 25);
            this.IH_CategoryComboBox.Name = "IH_CategoryComboBox";
            this.IH_CategoryComboBox.Size = new System.Drawing.Size(95, 21);
            this.IH_CategoryComboBox.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(698, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Category";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(385, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 19);
            this.label4.TabIndex = 53;
            this.label4.Text = "Item History";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IH_AddItemToNewPawnLoan
            // 
            this.IH_AddItemToNewPawnLoan.BackColor = System.Drawing.Color.Transparent;
            this.IH_AddItemToNewPawnLoan.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.IH_AddItemToNewPawnLoan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.IH_AddItemToNewPawnLoan.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.IH_AddItemToNewPawnLoan.FlatAppearance.BorderSize = 0;
            this.IH_AddItemToNewPawnLoan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.IH_AddItemToNewPawnLoan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.IH_AddItemToNewPawnLoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IH_AddItemToNewPawnLoan.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.IH_AddItemToNewPawnLoan.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.IH_AddItemToNewPawnLoan.Location = new System.Drawing.Point(754, 451);
            this.IH_AddItemToNewPawnLoan.Margin = new System.Windows.Forms.Padding(0);
            this.IH_AddItemToNewPawnLoan.MaximumSize = new System.Drawing.Size(100, 50);
            this.IH_AddItemToNewPawnLoan.MinimumSize = new System.Drawing.Size(100, 50);
            this.IH_AddItemToNewPawnLoan.Name = "IH_AddItemToNewPawnLoan";
            this.IH_AddItemToNewPawnLoan.Size = new System.Drawing.Size(100, 50);
            this.IH_AddItemToNewPawnLoan.TabIndex = 52;
            this.IH_AddItemToNewPawnLoan.Text = "Add Item to \r\nNew Pawn Loan";
            this.IH_AddItemToNewPawnLoan.UseVisualStyleBackColor = false;
            this.IH_AddItemToNewPawnLoan.Click += new System.EventHandler(this.IH_AddItemToNewPawnLoan_Click);
            // 
            // IH_ItemHistoryDataGridView
            // 
            this.IH_ItemHistoryDataGridView.AllowUserToAddRows = false;
            this.IH_ItemHistoryDataGridView.AllowUserToDeleteRows = false;
            this.IH_ItemHistoryDataGridView.AllowUserToResizeColumns = false;
            this.IH_ItemHistoryDataGridView.AllowUserToResizeRows = false;
            this.IH_ItemHistoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.IH_ItemHistoryDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.IH_ItemHistoryDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.IH_ItemHistoryDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IH_ItemHistoryDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.IH_ItemHistoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IH_ItemHistoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IH_History_TransactionNumberColumn,
            this.IH_History_StatusDateColumn,
            this.IH_History_ItemStatusColumn,
            this.IH_History_ItemDescriptionColumn,
            this.IH_History_DocType,
            this.IH_History_TktNo});
            this.IH_ItemHistoryDataGridView.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IH_ItemHistoryDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.IH_ItemHistoryDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.IH_ItemHistoryDataGridView.GridColor = System.Drawing.Color.LightGray;
            this.IH_ItemHistoryDataGridView.Location = new System.Drawing.Point(9, 73);
            this.IH_ItemHistoryDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.IH_ItemHistoryDataGridView.MultiSelect = false;
            this.IH_ItemHistoryDataGridView.Name = "IH_ItemHistoryDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IH_ItemHistoryDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.IH_ItemHistoryDataGridView.RowHeadersVisible = false;
            this.IH_ItemHistoryDataGridView.RowHeadersWidth = 20;
            this.IH_ItemHistoryDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IH_ItemHistoryDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.IH_ItemHistoryDataGridView.RowTemplate.Height = 25;
            this.IH_ItemHistoryDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.IH_ItemHistoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.IH_ItemHistoryDataGridView.Size = new System.Drawing.Size(843, 378);
            this.IH_ItemHistoryDataGridView.TabIndex = 51;
            this.IH_ItemHistoryDataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.IH_ItemHistoryDataGridView_CellMouseUp);
            // 
            // IH_History_TransactionNumberColumn
            // 
            this.IH_History_TransactionNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IH_History_TransactionNumberColumn.DataPropertyName = "IH_History_TransactionNumberColumn";
            this.IH_History_TransactionNumberColumn.HeaderText = "Transaction Number";
            this.IH_History_TransactionNumberColumn.Name = "IH_History_TransactionNumberColumn";
            this.IH_History_TransactionNumberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IH_History_TransactionNumberColumn.Width = 109;
            // 
            // IH_History_StatusDateColumn
            // 
            this.IH_History_StatusDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IH_History_StatusDateColumn.DataPropertyName = "IH_History_StatusDateColumn";
            this.IH_History_StatusDateColumn.HeaderText = "Status Date";
            this.IH_History_StatusDateColumn.MinimumWidth = 100;
            this.IH_History_StatusDateColumn.Name = "IH_History_StatusDateColumn";
            this.IH_History_StatusDateColumn.ReadOnly = true;
            // 
            // IH_History_ItemStatusColumn
            // 
            this.IH_History_ItemStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IH_History_ItemStatusColumn.DataPropertyName = "IH_History_ItemStatusColumn";
            this.IH_History_ItemStatusColumn.HeaderText = "Item Status";
            this.IH_History_ItemStatusColumn.MinimumWidth = 100;
            this.IH_History_ItemStatusColumn.Name = "IH_History_ItemStatusColumn";
            this.IH_History_ItemStatusColumn.ReadOnly = true;
            // 
            // IH_History_ItemDescriptionColumn
            // 
            this.IH_History_ItemDescriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IH_History_ItemDescriptionColumn.DataPropertyName = "IH_History_ItemDescriptionColumn";
            this.IH_History_ItemDescriptionColumn.HeaderText = "Item Description";
            this.IH_History_ItemDescriptionColumn.Name = "IH_History_ItemDescriptionColumn";
            this.IH_History_ItemDescriptionColumn.ReadOnly = true;
            // 
            // IH_History_DocType
            // 
            this.IH_History_DocType.DataPropertyName = "IH_History_DocType";
            this.IH_History_DocType.HeaderText = "DocType";
            this.IH_History_DocType.Name = "IH_History_DocType";
            this.IH_History_DocType.Visible = false;
            this.IH_History_DocType.Width = 74;
            // 
            // IH_History_TktNo
            // 
            this.IH_History_TktNo.DataPropertyName = "IH_History_TktNo";
            this.IH_History_TktNo.HeaderText = global::Support.Properties.Resources.OverrideMachineName;
            this.IH_History_TktNo.Name = "IH_History_TktNo";
            this.IH_History_TktNo.Visible = false;
            this.IH_History_TktNo.Width = 19;
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
            // customButtonExit
            // 
            this.customButtonExit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonExit.BackgroundImage")));
            this.customButtonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonExit.FlatAppearance.BorderSize = 0;
            this.customButtonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonExit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonExit.ForeColor = System.Drawing.Color.White;
            this.customButtonExit.Location = new System.Drawing.Point(635, 451);
            this.customButtonExit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonExit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonExit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonExit.Name = "customButtonExit";
            this.customButtonExit.Size = new System.Drawing.Size(100, 50);
            this.customButtonExit.TabIndex = 56;
            this.customButtonExit.Text = "Exit";
            this.customButtonExit.UseVisualStyleBackColor = false;
            this.customButtonExit.Click += new System.EventHandler(this.customButtonExit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 14);
            this.label3.TabIndex = 149;
            this.label3.Text = "Product:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IH_ProductComboBox
            // 
            this.IH_ProductComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.IH_ProductComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IH_ProductComboBox.ForeColor = System.Drawing.Color.Black;
            this.IH_ProductComboBox.FormattingEnabled = true;
            this.IH_ProductComboBox.Items.AddRange(new object[] {
            "All",
            "Pawn",
            "Buy",
            "Sale",
            "Layaway",
            "Refund"});
            this.IH_ProductComboBox.Location = new System.Drawing.Point(70, 26);
            this.IH_ProductComboBox.Name = "IH_ProductComboBox";
            this.IH_ProductComboBox.Size = new System.Drawing.Size(95, 21);
            this.IH_ProductComboBox.TabIndex = 148;
            this.IH_ProductComboBox.SelectedIndexChanged += new System.EventHandler(this.IH_ProductComboBox_SelectedIndexChanged);
            // 
            // Controller_ItemHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(867, 514);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IH_ProductComboBox);
            this.Controls.Add(this.customButtonExit);
            this.Controls.Add(this.IH_CategoryComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IH_AddItemToNewPawnLoan);
            this.Controls.Add(this.IH_ItemHistoryDataGridView);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Controller_ItemHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Controller_ItemHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IH_ItemHistoryDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox IH_CategoryComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button IH_AddItemToNewPawnLoan;
        private System.Windows.Forms.DataGridView IH_ItemHistoryDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private CustomButton customButtonExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox IH_ProductComboBox;
        private System.Windows.Forms.DataGridViewLinkColumn IH_History_TransactionNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IH_History_StatusDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IH_History_ItemStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IH_History_ItemDescriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IH_History_DocType;
        private System.Windows.Forms.DataGridViewTextBoxColumn IH_History_TktNo;
    }
}
