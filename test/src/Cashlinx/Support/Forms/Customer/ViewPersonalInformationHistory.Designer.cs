namespace Support.Forms.Customer
{
    partial class ViewPersonalInformationHistory
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
            this.BtnBack = new Common.Libraries.Forms.Components.CustomButton();
            this.LblFormHeader = new Common.Libraries.Forms.Components.CustomLabel();
            this.DGInformationHistory = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.Shop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeforeChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AfterChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGInformationHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnBack
            // 
            this.BtnBack.BackColor = System.Drawing.Color.Transparent;
            this.BtnBack.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.BtnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnBack.FlatAppearance.BorderSize = 0;
            this.BtnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.ForeColor = System.Drawing.Color.White;
            this.BtnBack.Location = new System.Drawing.Point(32, 395);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(0);
            this.BtnBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.BtnBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(100, 50);
            this.BtnBack.TabIndex = 1;
            this.BtnBack.Text = "Back";
            this.BtnBack.UseVisualStyleBackColor = false;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // LblFormHeader
            // 
            this.LblFormHeader.AutoSize = true;
            this.LblFormHeader.BackColor = System.Drawing.Color.Transparent;
            this.LblFormHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFormHeader.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblFormHeader.Location = new System.Drawing.Point(349, 24);
            this.LblFormHeader.Name = "LblFormHeader";
            this.LblFormHeader.Size = new System.Drawing.Size(209, 20);
            this.LblFormHeader.TabIndex = 2;
            this.LblFormHeader.Text = "Personal Information History";
            // 
            // DGInformationHistory
            // 
            this.DGInformationHistory.AllowUserToAddRows = false;
            this.DGInformationHistory.AllowUserToDeleteRows = false;
            this.DGInformationHistory.AllowUserToResizeColumns = false;
            this.DGInformationHistory.AllowUserToResizeRows = false;
            this.DGInformationHistory.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DGInformationHistory.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGInformationHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGInformationHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGInformationHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Shop,
            this.TransactionDate,
            this.BeforeChange,
            this.AfterChange,
            this.UserID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGInformationHistory.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGInformationHistory.GridColor = System.Drawing.Color.LightGray;
            this.DGInformationHistory.Location = new System.Drawing.Point(13, 64);
            this.DGInformationHistory.Margin = new System.Windows.Forms.Padding(0);
            this.DGInformationHistory.Name = "DGInformationHistory";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGInformationHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGInformationHistory.RowHeadersVisible = false;
            this.DGInformationHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGInformationHistory.Size = new System.Drawing.Size(729, 318);
            this.DGInformationHistory.TabIndex = 3;
            // 
            // Shop
            // 
            this.Shop.HeaderText = "Shop/Dept";
            this.Shop.Name = "Shop";
            // 
            // TransactionDate
            // 
            this.TransactionDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TransactionDate.HeaderText = "Transaction Date";
            this.TransactionDate.MaxInputLength = 32;
            this.TransactionDate.Name = "TransactionDate";
            this.TransactionDate.ReadOnly = true;
            this.TransactionDate.Width = 114;
            // 
            // BeforeChange
            // 
            this.BeforeChange.HeaderText = "Before";
            this.BeforeChange.MaxInputLength = 40;
            this.BeforeChange.MinimumWidth = 40;
            this.BeforeChange.Name = "BeforeChange";
            // 
            // AfterChange
            // 
            this.AfterChange.HeaderText = "After";
            this.AfterChange.Name = "AfterChange";
            // 
            // UserID
            // 
            this.UserID.HeaderText = "User ID";
            this.UserID.Name = "UserID";
            // 
            // ViewPersonalInformationHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(762, 468);
            this.Controls.Add(this.DGInformationHistory);
            this.Controls.Add(this.LblFormHeader);
            this.Controls.Add(this.BtnBack);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewPersonalInformationHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewPersonalInformationHistory";
            this.Load += new System.EventHandler(this.ViewPersonalInformationHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGInformationHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Common.Libraries.Forms.Components.CustomButton BtnBack;
        private Common.Libraries.Forms.Components.CustomLabel LblFormHeader;
        private Common.Libraries.Forms.Components.CustomDataGridView DGInformationHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shop;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeforeChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn AfterChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
    }
}