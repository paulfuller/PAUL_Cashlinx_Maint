namespace Support.Forms.Customer.ProductTabs
{
    partial class SupportCustomerComments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAddComment = new Support.Libraries.Forms.Components.SupportButton();
            this.btnCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.LblCustomerName = new Common.Libraries.Forms.Components.CustomLabel();
            this.LblSSN = new Common.Libraries.Forms.Components.CustomLabel();
            this.TxbCustomerName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.TxbSSN = new Common.Libraries.Forms.Components.CustomTextBox();
            this.DgvCustomerComments = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.GvcComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvcEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DgvEmployeeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCustomerComments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddComment
            // 
            this.btnAddComment.BackColor = System.Drawing.Color.Transparent;
            this.btnAddComment.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnAddComment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddComment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddComment.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAddComment.FlatAppearance.BorderSize = 0;
            this.btnAddComment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddComment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddComment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddComment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddComment.ForeColor = System.Drawing.Color.White;
            this.btnAddComment.Location = new System.Drawing.Point(747, 375);
            this.btnAddComment.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddComment.MaximumSize = new System.Drawing.Size(90, 40);
            this.btnAddComment.MinimumSize = new System.Drawing.Size(90, 40);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Size = new System.Drawing.Size(90, 40);
            this.btnAddComment.TabIndex = 0;
            this.btnAddComment.Text = "Add";
            this.btnAddComment.UseVisualStyleBackColor = false;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(58, 375);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(90, 40);
            this.btnCancel.MinimumSize = new System.Drawing.Size(90, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // LblCustomerName
            // 
            this.LblCustomerName.AutoSize = true;
            this.LblCustomerName.Location = new System.Drawing.Point(165, 78);
            this.LblCustomerName.Name = "LblCustomerName";
            this.LblCustomerName.Size = new System.Drawing.Size(85, 13);
            this.LblCustomerName.TabIndex = 2;
            this.LblCustomerName.Text = "Customer Name:";
            // 
            // LblSSN
            // 
            this.LblSSN.AutoSize = true;
            this.LblSSN.Location = new System.Drawing.Point(525, 81);
            this.LblSSN.Name = "LblSSN";
            this.LblSSN.Size = new System.Drawing.Size(32, 13);
            this.LblSSN.TabIndex = 4;
            this.LblSSN.Text = "SSN:";
            // 
            // TxbCustomerName
            // 
            this.TxbCustomerName.CausesValidation = false;
            this.TxbCustomerName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.TxbCustomerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbCustomerName.Location = new System.Drawing.Point(304, 78);
            this.TxbCustomerName.Name = "TxbCustomerName";
            this.TxbCustomerName.Size = new System.Drawing.Size(100, 21);
            this.TxbCustomerName.TabIndex = 5;
            this.TxbCustomerName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            //this.TxbCustomerName.TextChanged += new System.EventHandler(this.TxbCustomerName_TextChanged);
            // 
            // TxbSSN
            // 
            this.TxbSSN.CausesValidation = false;
            this.TxbSSN.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.TxbSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbSSN.Location = new System.Drawing.Point(605, 75);
            this.TxbSSN.Name = "TxbSSN";
            this.TxbSSN.Size = new System.Drawing.Size(100, 21);
            this.TxbSSN.TabIndex = 6;
            this.TxbSSN.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // DgvCustomerComments
            // 
            this.DgvCustomerComments.AllowUserToAddRows = false;
            this.DgvCustomerComments.AllowUserToDeleteRows = false;
            this.DgvCustomerComments.AllowUserToResizeColumns = false;
            this.DgvCustomerComments.AllowUserToResizeRows = false;
            this.DgvCustomerComments.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvCustomerComments.CausesValidation = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvCustomerComments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DgvCustomerComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCustomerComments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GvcComments,
            this.DgvEntryDate,
            this.GvcEntryDate,
            this.DgvCategory,
            this.DgvEmployeeNumber});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvCustomerComments.DefaultCellStyle = dataGridViewCellStyle11;
            this.DgvCustomerComments.GridColor = System.Drawing.Color.LightGray;
            this.DgvCustomerComments.Location = new System.Drawing.Point(9, 102);
            this.DgvCustomerComments.Margin = new System.Windows.Forms.Padding(0);
            this.DgvCustomerComments.Name = "DgvCustomerComments";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvCustomerComments.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.DgvCustomerComments.RowHeadersVisible = false;
            this.DgvCustomerComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DgvCustomerComments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgvCustomerComments.Size = new System.Drawing.Size(873, 245);
            this.DgvCustomerComments.TabIndex = 7;
            // 
            // GvcComments
            // 
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvcComments.DefaultCellStyle = dataGridViewCellStyle10;
            this.GvcComments.HeaderText = "Comments";
            this.GvcComments.MaxInputLength = 4000;
            this.GvcComments.Name = "GvcComments";
            this.GvcComments.ReadOnly = true;
            this.GvcComments.Width = 400;
            // 
            // DgvEntryDate
            // 
            this.DgvEntryDate.HeaderText = "Date Made";
            this.DgvEntryDate.Name = "DgvEntryDate";
            // 
            // GvcEntryDate
            // 
            this.GvcEntryDate.HeaderText = "Made By";
            this.GvcEntryDate.Name = "GvcEntryDate";
            this.GvcEntryDate.Width = 175;
            // 
            // DgvCategory
            // 
            this.DgvCategory.HeaderText = "Category";
            this.DgvCategory.Name = "DgvCategory";
            // 
            // DgvEmployeeNumber
            // 
            this.DgvEmployeeNumber.HeaderText = "Employee Number";
            this.DgvEmployeeNumber.Name = "DgvEmployeeNumber";
            // 
            // SupportCustomerComments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(891, 455);
            this.Controls.Add(this.DgvCustomerComments);
            this.Controls.Add(this.TxbSSN);
            this.Controls.Add(this.TxbCustomerName);
            this.Controls.Add(this.LblSSN);
            this.Controls.Add(this.LblCustomerName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddComment);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupportCustomerComments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SupportCustomerComments";
            this.Load += new System.EventHandler(this.SupportCustomerComments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvCustomerComments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Libraries.Forms.Components.SupportButton btnAddComment;
        private Libraries.Forms.Components.SupportButton btnCancel;
        private Common.Libraries.Forms.Components.CustomLabel LblCustomerName;
        private Common.Libraries.Forms.Components.CustomLabel LblSSN;
        private Common.Libraries.Forms.Components.CustomTextBox TxbCustomerName;
        private Common.Libraries.Forms.Components.CustomTextBox TxbSSN;
        private Common.Libraries.Forms.Components.CustomDataGridView DgvCustomerComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn GvcComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GvcEntryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn DgvEmployeeNumber;
    }
}