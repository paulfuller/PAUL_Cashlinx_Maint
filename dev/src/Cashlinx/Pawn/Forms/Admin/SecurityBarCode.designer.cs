using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Admin
{
    partial class SecurityBarCode
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
            this.label3 = new System.Windows.Forms.Label();
            this.gvEmployees = new System.Windows.Forms.DataGridView();
            this.colPrint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colEmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmployeeFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEncryptData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doneButton = new CustomButton();
            this.cancelCloseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(133, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 19);
            this.label3.TabIndex = 138;
            this.label3.Text = "Print Security Barcodes";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gvEmployees
            // 
            this.gvEmployees.AllowUserToAddRows = false;
            this.gvEmployees.AllowUserToResizeColumns = false;
            this.gvEmployees.AllowUserToResizeRows = false;
            this.gvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.gvEmployees.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvEmployees.BackgroundColor = System.Drawing.Color.White;
            this.gvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvEmployees.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEmployees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPrint,
            this.colEmpNo,
            this.colEmployeeFirstName,
            this.colEncryptData});
            this.gvEmployees.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255); ;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEmployees.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvEmployees.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvEmployees.GridColor = System.Drawing.Color.LightGray;
            this.gvEmployees.Location = new System.Drawing.Point(28, 90);
            this.gvEmployees.MultiSelect = false;
            this.gvEmployees.Name = "gvEmployees";
            this.gvEmployees.RowHeadersVisible = false;
            this.gvEmployees.RowHeadersWidth = 20;
            this.gvEmployees.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmployees.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvEmployees.RowTemplate.Height = 25;
            this.gvEmployees.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvEmployees.Size = new System.Drawing.Size(380, 272);
            this.gvEmployees.TabIndex = 148;
            this.gvEmployees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmployees_CellClick);
            // 
            // colPrint
            // 
            this.colPrint.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPrint.DataPropertyName = "colPrint";
            this.colPrint.HeaderText = "Print";
            this.colPrint.Name = "colPrint";
            this.colPrint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPrint.Text = "Print";
            this.colPrint.ToolTipText = "Click button to generate Employee Security Bar Code.";
            this.colPrint.UseColumnTextForButtonValue = true;
            // 
            // colEmpNo
            // 
            this.colEmpNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEmpNo.DataPropertyName = "colEmpNo";
            this.colEmpNo.FillWeight = 75F;
            this.colEmpNo.HeaderText = "Emp #";
            this.colEmpNo.Name = "colEmpNo";
            this.colEmpNo.ReadOnly = true;
            // 
            // colEmployeeFirstName
            // 
            this.colEmployeeFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEmployeeFirstName.DataPropertyName = "colEmployeeFirstName";
            this.colEmployeeFirstName.FillWeight = 189.6907F;
            this.colEmployeeFirstName.HeaderText = "Name";
            this.colEmployeeFirstName.Name = "colEmployeeFirstName";
            this.colEmployeeFirstName.ReadOnly = true;
            // 
            // colEncryptData
            // 
            this.colEncryptData.HeaderText = "";
            this.colEncryptData.Name = "colEncryptData";
            this.colEncryptData.Visible = false;
            this.colEncryptData.Width = 19;
            // 
            // doneButton
            // 
            this.doneButton.BackColor = System.Drawing.Color.Transparent;
            this.doneButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.doneButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.doneButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.doneButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.doneButton.FlatAppearance.BorderSize = 0;
            this.doneButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.doneButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.doneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doneButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doneButton.ForeColor = System.Drawing.Color.White;
            this.doneButton.Location = new System.Drawing.Point(308, 366);
            this.doneButton.Margin = new System.Windows.Forms.Padding(0);
            this.doneButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.doneButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(100, 50);
            this.doneButton.TabIndex = 149;
            this.doneButton.Text = "Close";
            this.doneButton.UseVisualStyleBackColor = false;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // cancelCloseButton
            // 
            this.cancelCloseButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelCloseButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelCloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelCloseButton.FlatAppearance.BorderSize = 0;
            this.cancelCloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelCloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelCloseButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelCloseButton.ForeColor = System.Drawing.Color.White;
            this.cancelCloseButton.Location = new System.Drawing.Point(28, 366);
            this.cancelCloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelCloseButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelCloseButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelCloseButton.Name = "cancelCloseButton";
            this.cancelCloseButton.Size = new System.Drawing.Size(100, 50);
            this.cancelCloseButton.TabIndex = 150;
            this.cancelCloseButton.Text = "Cancel";
            this.cancelCloseButton.UseVisualStyleBackColor = false;
            this.cancelCloseButton.Click += new System.EventHandler(this.cancelCloseButton_Click);
            // 
            // SecurityBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(436, 425);
            this.ControlBox = false;
            this.Controls.Add(this.cancelCloseButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.gvEmployees);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecurityBarCode";
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gvEmployees;
        private CustomButton doneButton;
        private System.Windows.Forms.Button cancelCloseButton;
        private System.Windows.Forms.DataGridViewButtonColumn colPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmployeeFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEncryptData;
    }
}