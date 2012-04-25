namespace CashlinxPawnSupportApp.Forms.ClearTempStatus
{
    partial class FrmDupMerch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.changePasswordHeaderLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.dgDuplicates = new System.Windows.Forms.DataGridView();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.merch_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.temp_lock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicates)).BeginInit();
            this.SuspendLayout();
            // 
            // changePasswordHeaderLabel
            // 
            this.changePasswordHeaderLabel.AutoSize = true;
            this.changePasswordHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.changePasswordHeaderLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePasswordHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.changePasswordHeaderLabel.Location = new System.Drawing.Point(283, 29);
            this.changePasswordHeaderLabel.Name = "changePasswordHeaderLabel";
            this.changePasswordHeaderLabel.Size = new System.Drawing.Size(188, 25);
            this.changePasswordHeaderLabel.TabIndex = 2;
            this.changePasswordHeaderLabel.Text = "Duplicate Item List";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(657, 419);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 38);
            this.button1.TabIndex = 15;
            this.button1.Text = "Continue";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(50, 419);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 38);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Back";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // dgDuplicates
            // 
            this.dgDuplicates.AllowUserToAddRows = false;
            this.dgDuplicates.AllowUserToDeleteRows = false;
            this.dgDuplicates.AllowUserToResizeColumns = false;
            this.dgDuplicates.AllowUserToResizeRows = false;
            this.dgDuplicates.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgDuplicates.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgDuplicates.CausesValidation = false;
            this.dgDuplicates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDuplicates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icn,
            this.merch_description,
            this.status,
            this.cost,
            this.temp_lock});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDuplicates.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgDuplicates.GridColor = System.Drawing.Color.Black;
            this.dgDuplicates.Location = new System.Drawing.Point(42, 143);
            this.dgDuplicates.MultiSelect = false;
            this.dgDuplicates.Name = "dgDuplicates";
            this.dgDuplicates.ReadOnly = true;
            this.dgDuplicates.RowHeadersVisible = false;
            this.dgDuplicates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgDuplicates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDuplicates.Size = new System.Drawing.Size(715, 223);
            this.dgDuplicates.TabIndex = 1;
            // 
            // icn
            // 
            this.icn.HeaderText = "ICN";
            this.icn.Name = "icn";
            this.icn.ReadOnly = true;
            this.icn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.icn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.icn.Width = 160;
            // 
            // merch_description
            // 
            this.merch_description.HeaderText = "Merchandise Description";
            this.merch_description.Name = "merch_description";
            this.merch_description.ReadOnly = true;
            this.merch_description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.merch_description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.merch_description.Width = 352;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cost
            // 
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = "0";
            this.cost.DefaultCellStyle = dataGridViewCellStyle1;
            this.cost.HeaderText = "Cost";
            this.cost.Name = "cost";
            this.cost.ReadOnly = true;
            this.cost.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // temp_lock
            // 
            this.temp_lock.HeaderText = "Temp Lock";
            this.temp_lock.Name = "temp_lock";
            this.temp_lock.ReadOnly = true;
            this.temp_lock.Visible = false;
            // 
            // FrmDupMerch
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_480;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.dgDuplicates);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changePasswordHeaderLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDupMerch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDupMerch";
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changePasswordHeaderLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridView dgDuplicates;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn merch_description;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn temp_lock;
    }
}