using System.Windows.Forms;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DgvCustomerComments = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.LblCustomerName = new System.Windows.Forms.Label();
            this.LblSSN = new System.Windows.Forms.Label();
            this.customerDisplayLabel = new System.Windows.Forms.Label();
            this.SSNDisplayLabel = new System.Windows.Forms.Label();
            this.LblHeader = new System.Windows.Forms.Label();
            this.btnCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.btnAddComment = new Support.Libraries.Forms.Components.SupportButton();
            this.GvcComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvcEntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvcMadeBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvcCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GvcEmployeeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCustomerComments)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvCustomerComments
            // 
            this.DgvCustomerComments.AllowUserToAddRows = false;
            this.DgvCustomerComments.AllowUserToDeleteRows = false;
            this.DgvCustomerComments.AllowUserToResizeColumns = false;
            this.DgvCustomerComments.AllowUserToResizeRows = false;
            this.DgvCustomerComments.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.DgvCustomerComments.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.DgvCustomerComments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvCustomerComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvCustomerComments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GvcComments,
            this.GvcEntryDate,
            this.GvcMadeBy,
            this.GvcCategory,
            this.GvcEmployeeNumber});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvCustomerComments.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvCustomerComments.GridColor = System.Drawing.Color.LightGray;
            this.DgvCustomerComments.Location = new System.Drawing.Point(10, 118);
            this.DgvCustomerComments.Margin = new System.Windows.Forms.Padding(0);
            this.DgvCustomerComments.MultiSelect = false;
            this.DgvCustomerComments.Name = "DgvCustomerComments";
            this.DgvCustomerComments.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.DgvCustomerComments.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvCustomerComments.RowHeadersVisible = false;
            this.DgvCustomerComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DgvCustomerComments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvCustomerComments.Size = new System.Drawing.Size(1018, 453);
            this.DgvCustomerComments.TabIndex = 7;
            this.DgvCustomerComments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCustomerComments_CellContentClick);
            this.DgvCustomerComments.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvCustomerComments_CellPainting);
            // 
            // LblCustomerName
            // 
            this.LblCustomerName.AutoSize = true;
            this.LblCustomerName.Location = new System.Drawing.Point(192, 90);
            this.LblCustomerName.Name = "LblCustomerName";
            this.LblCustomerName.Size = new System.Drawing.Size(98, 14);
            this.LblCustomerName.TabIndex = 2;
            this.LblCustomerName.Text = "Customer Name:";
            // 
            // LblSSN
            // 
            this.LblSSN.AutoSize = true;
            this.LblSSN.Location = new System.Drawing.Point(612, 93);
            this.LblSSN.Name = "LblSSN";
            this.LblSSN.Size = new System.Drawing.Size(33, 14);
            this.LblSSN.TabIndex = 4;
            this.LblSSN.Text = "SSN:";
            // 
            // customerDisplayLabel
            // 
            this.customerDisplayLabel.AutoSize = true;
            this.customerDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerDisplayLabel.Location = new System.Drawing.Point(322, 90);
            this.customerDisplayLabel.Name = "customerDisplayLabel";
            this.customerDisplayLabel.Size = new System.Drawing.Size(0, 16);
            this.customerDisplayLabel.TabIndex = 8;
            // 
            // SSNDisplayLabel
            // 
            this.SSNDisplayLabel.AutoSize = true;
            this.SSNDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSNDisplayLabel.Location = new System.Drawing.Point(684, 93);
            this.SSNDisplayLabel.Name = "SSNDisplayLabel";
            this.SSNDisplayLabel.Size = new System.Drawing.Size(0, 16);
            this.SSNDisplayLabel.TabIndex = 9;
            // 
            // LblHeader
            // 
            this.LblHeader.AutoSize = true;
            this.LblHeader.BackColor = System.Drawing.Color.Transparent;
            this.LblHeader.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblHeader.Location = new System.Drawing.Point(383, 24);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(231, 25);
            this.LblHeader.TabIndex = 10;
            this.LblHeader.Text = "Customer Comments";
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
            this.btnCancel.Location = new System.Drawing.Point(69, 591);
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
            this.btnAddComment.Location = new System.Drawing.Point(873, 591);
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
            // GvcComments
            // 
            this.GvcComments.HeaderText = "Comments";
            this.GvcComments.MaxInputLength = 4000;
            this.GvcComments.Name = "GvcComments";
            this.GvcComments.ReadOnly = true;
            this.GvcComments.Width = 530;
            // 
            // GvcEntryDate
            // 
            this.GvcEntryDate.HeaderText = "Date Made";
            this.GvcEntryDate.Name = "GvcEntryDate";
            this.GvcEntryDate.ReadOnly = true;
            this.GvcEntryDate.Width = 120;
            // 
            // GvcMadeBy
            // 
            this.GvcMadeBy.HeaderText = "Made By";
            this.GvcMadeBy.Name = "GvcMadeBy";
            this.GvcMadeBy.ReadOnly = true;
            // 
            // GvcCategory
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvcCategory.DefaultCellStyle = dataGridViewCellStyle2;
            this.GvcCategory.HeaderText = "Category";
            this.GvcCategory.Name = "GvcCategory";
            this.GvcCategory.ReadOnly = true;
            this.GvcCategory.Width = 180;
            // 
            // GvcEmployeeNumber
            // 
            this.GvcEmployeeNumber.HeaderText = "Employee Number";
            this.GvcEmployeeNumber.Name = "GvcEmployeeNumber";
            this.GvcEmployeeNumber.ReadOnly = true;
            // 
            // SupportCustomerComments
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1040, 680);
            this.Controls.Add(this.LblHeader);
            this.Controls.Add(this.SSNDisplayLabel);
            this.Controls.Add(this.customerDisplayLabel);
            this.Controls.Add(this.DgvCustomerComments);
            this.Controls.Add(this.LblSSN);
            this.Controls.Add(this.LblCustomerName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddComment);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        private System.Windows.Forms.Label LblCustomerName;
        private System.Windows.Forms.Label LblSSN;
        private Common.Libraries.Forms.Components.CustomDataGridView DgvCustomerComments;
        private System.Windows.Forms.Label customerDisplayLabel;
        private System.Windows.Forms.Label SSNDisplayLabel;
        private System.Windows.Forms.Label LblHeader;
        private DataGridViewTextBoxColumn GvcComments;
        private DataGridViewTextBoxColumn GvcEntryDate;
        private DataGridViewTextBoxColumn GvcMadeBy;
        private DataGridViewTextBoxColumn GvcCategory;
        private DataGridViewTextBoxColumn GvcEmployeeNumber;
    }
}