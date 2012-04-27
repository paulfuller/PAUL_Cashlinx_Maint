using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class ViewCashDrawerAssignments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewCashDrawerAssignments));
            this.labelHeading = new System.Windows.Forms.Label();
            this.customDataGridViewCDAssignments = new CustomDataGridView();
            this.customButtonClose = new CustomButton();
            this.EmployeeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashDrawerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Safe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonManageCashDrawers = new CustomButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewCDAssignments)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(34, 27);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(221, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Cash Drawer Assignments";
            // 
            // customDataGridViewCDAssignments
            // 
            this.customDataGridViewCDAssignments.AllowUserToAddRows = false;
            this.customDataGridViewCDAssignments.AllowUserToDeleteRows = false;
            this.customDataGridViewCDAssignments.AllowUserToResizeColumns = false;
            this.customDataGridViewCDAssignments.AllowUserToResizeRows = false;
            this.customDataGridViewCDAssignments.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewCDAssignments.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewCDAssignments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewCDAssignments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewCDAssignments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewCDAssignments.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewCDAssignments.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewCDAssignments.Location = new System.Drawing.Point(58, 87);
            this.customDataGridViewCDAssignments.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewCDAssignments.Name = "customDataGridViewCDAssignments";
            this.customDataGridViewCDAssignments.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewCDAssignments.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridViewCDAssignments.RowHeadersVisible = false;
            this.customDataGridViewCDAssignments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewCDAssignments.Size = new System.Drawing.Size(527, 197);
            this.customDataGridViewCDAssignments.TabIndex = 1;
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
            this.customButtonClose.Location = new System.Drawing.Point(58, 307);
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
            // EmployeeNumber
            // 
            this.EmployeeNumber.HeaderText = "Emp #";
            this.EmployeeNumber.Name = "EmployeeNumber";
            // 
            // EmployeeName
            // 
            this.EmployeeName.HeaderText = "Employee Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.Width = 200;
            // 
            // CashDrawerName
            // 
            this.CashDrawerName.HeaderText = "Cash Drawer Name";
            this.CashDrawerName.Name = "CashDrawerName";
            this.CashDrawerName.Width = 200;
            // 
            // Safe
            // 
            this.Safe.HeaderText = "Safe";
            this.Safe.Name = "Safe";
            this.Safe.Width = 150;
            // 
            // customButtonManageCashDrawers
            // 
            this.customButtonManageCashDrawers.BackColor = System.Drawing.Color.Transparent;
            this.customButtonManageCashDrawers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonManageCashDrawers.BackgroundImage")));
            this.customButtonManageCashDrawers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonManageCashDrawers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonManageCashDrawers.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonManageCashDrawers.FlatAppearance.BorderSize = 0;
            this.customButtonManageCashDrawers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonManageCashDrawers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonManageCashDrawers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonManageCashDrawers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonManageCashDrawers.ForeColor = System.Drawing.Color.White;
            this.customButtonManageCashDrawers.Location = new System.Drawing.Point(460, 307);
            this.customButtonManageCashDrawers.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonManageCashDrawers.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonManageCashDrawers.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonManageCashDrawers.Name = "customButtonManageCashDrawers";
            this.customButtonManageCashDrawers.Size = new System.Drawing.Size(100, 50);
            this.customButtonManageCashDrawers.TabIndex = 4;
            this.customButtonManageCashDrawers.Text = "Manage Drawers";
            this.customButtonManageCashDrawers.UseVisualStyleBackColor = false;
            this.customButtonManageCashDrawers.Click += new System.EventHandler(this.customButtonManageCashDrawers_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Emp #";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Employee Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Cash Drawer Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // ViewCashDrawerAssignments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 366);
            this.Controls.Add(this.customButtonManageCashDrawers);
            this.Controls.Add(this.customButtonClose);
            this.Controls.Add(this.customDataGridViewCDAssignments);
            this.Controls.Add(this.labelHeading);
            this.Name = "ViewCashDrawerAssignments";
            this.Text = "ViewCashDrawerAssignments";
            this.Load += new System.EventHandler(this.ViewCashDrawerAssignments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewCDAssignments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomDataGridView customDataGridViewCDAssignments;
        private CustomButton customButtonClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashDrawerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Safe;
        private CustomButton customButtonManageCashDrawers;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}
