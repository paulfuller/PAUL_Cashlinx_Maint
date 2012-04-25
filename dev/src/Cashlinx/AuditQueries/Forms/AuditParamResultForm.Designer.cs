namespace AuditQueries.Forms
{
    partial class AuditParamResultForm
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
            this.parameterGroupBox = new System.Windows.Forms.GroupBox();
            this.queryParamDataGrid = new System.Windows.Forms.DataGridView();
            this.paramNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paramTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.executeQueryButton = new System.Windows.Forms.Button();
            this.resultsGroupBox = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.queryResultDataGridView = new System.Windows.Forms.DataGridView();
            this.exitButton = new System.Windows.Forms.Button();
            this.parameterGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryParamDataGrid)).BeginInit();
            this.resultsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryResultDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // parameterGroupBox
            // 
            this.parameterGroupBox.Controls.Add(this.queryParamDataGrid);
            this.parameterGroupBox.Controls.Add(this.label1);
            this.parameterGroupBox.Controls.Add(this.executeQueryButton);
            this.parameterGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.parameterGroupBox.Location = new System.Drawing.Point(13, 12);
            this.parameterGroupBox.Name = "parameterGroupBox";
            this.parameterGroupBox.Size = new System.Drawing.Size(670, 213);
            this.parameterGroupBox.TabIndex = 0;
            this.parameterGroupBox.TabStop = false;
            this.parameterGroupBox.Text = "Parameters";
            // 
            // queryParamDataGrid
            // 
            this.queryParamDataGrid.AllowUserToAddRows = false;
            this.queryParamDataGrid.AllowUserToDeleteRows = false;
            this.queryParamDataGrid.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.queryParamDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.queryParamDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.queryParamDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.queryParamDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.queryParamDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.paramNameColumn,
            this.paramTypeColumn,
            this.Value});
            this.queryParamDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.queryParamDataGrid.GridColor = System.Drawing.Color.DimGray;
            this.queryParamDataGrid.Location = new System.Drawing.Point(33, 48);
            this.queryParamDataGrid.MultiSelect = false;
            this.queryParamDataGrid.Name = "queryParamDataGrid";
            this.queryParamDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.queryParamDataGrid.RowHeadersVisible = false;
            this.queryParamDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.queryParamDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.queryParamDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.queryParamDataGrid.ShowCellErrors = false;
            this.queryParamDataGrid.ShowCellToolTips = false;
            this.queryParamDataGrid.ShowEditingIcon = false;
            this.queryParamDataGrid.ShowRowErrors = false;
            this.queryParamDataGrid.Size = new System.Drawing.Size(603, 113);
            this.queryParamDataGrid.TabIndex = 3;
            this.queryParamDataGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.queryParamDataGrid_CellBeginEdit);
            this.queryParamDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.queryParamDataGrid_CellEndEdit);
            // 
            // paramNameColumn
            // 
            this.paramNameColumn.HeaderText = "Name";
            this.paramNameColumn.MinimumWidth = 100;
            this.paramNameColumn.Name = "paramNameColumn";
            this.paramNameColumn.ReadOnly = true;
            this.paramNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // paramTypeColumn
            // 
            this.paramTypeColumn.HeaderText = "Type";
            this.paramTypeColumn.MinimumWidth = 100;
            this.paramTypeColumn.Name = "paramTypeColumn";
            this.paramTypeColumn.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.MaxInputLength = 60;
            this.Value.MinimumWidth = 400;
            this.Value.Name = "Value";
            this.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Value.Width = 400;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please enter values for the parameters listed below:";
            // 
            // executeQueryButton
            // 
            this.executeQueryButton.Location = new System.Drawing.Point(288, 167);
            this.executeQueryButton.Name = "executeQueryButton";
            this.executeQueryButton.Size = new System.Drawing.Size(94, 40);
            this.executeQueryButton.TabIndex = 1;
            this.executeQueryButton.Text = "Execute Query";
            this.executeQueryButton.UseVisualStyleBackColor = true;
            this.executeQueryButton.Click += new System.EventHandler(this.executeQueryButton_Click);
            // 
            // resultsGroupBox
            // 
            this.resultsGroupBox.Controls.Add(this.saveButton);
            this.resultsGroupBox.Controls.Add(this.queryResultDataGridView);
            this.resultsGroupBox.Location = new System.Drawing.Point(13, 232);
            this.resultsGroupBox.Name = "resultsGroupBox";
            this.resultsGroupBox.Size = new System.Drawing.Size(670, 406);
            this.resultsGroupBox.TabIndex = 1;
            this.resultsGroupBox.TabStop = false;
            this.resultsGroupBox.Text = "Results";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(542, 355);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(94, 40);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save\r\nResults";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // queryResultDataGridView
            // 
            this.queryResultDataGridView.AllowUserToAddRows = false;
            this.queryResultDataGridView.AllowUserToDeleteRows = false;
            this.queryResultDataGridView.BackgroundColor = System.Drawing.Color.LightGray;
            this.queryResultDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.queryResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.queryResultDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.queryResultDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.queryResultDataGridView.EnableHeadersVisualStyles = false;
            this.queryResultDataGridView.GridColor = System.Drawing.Color.Black;
            this.queryResultDataGridView.Location = new System.Drawing.Point(30, 22);
            this.queryResultDataGridView.MultiSelect = false;
            this.queryResultDataGridView.Name = "queryResultDataGridView";
            this.queryResultDataGridView.ReadOnly = true;
            this.queryResultDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.queryResultDataGridView.RowHeadersVisible = false;
            this.queryResultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.queryResultDataGridView.ShowCellErrors = false;
            this.queryResultDataGridView.ShowCellToolTips = false;
            this.queryResultDataGridView.ShowEditingIcon = false;
            this.queryResultDataGridView.ShowRowErrors = false;
            this.queryResultDataGridView.Size = new System.Drawing.Size(606, 327);
            this.queryResultDataGridView.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(296, 656);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(94, 40);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // AuditParamResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(687, 700);
            this.ControlBox = false;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.resultsGroupBox);
            this.Controls.Add(this.parameterGroupBox);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AuditParamResultForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audit Parameters & Results";
            this.Load += new System.EventHandler(this.AuditParamResultForm_Load);
            this.parameterGroupBox.ResumeLayout(false);
            this.parameterGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryParamDataGrid)).EndInit();
            this.resultsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.queryResultDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox parameterGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button executeQueryButton;
        private System.Windows.Forms.GroupBox resultsGroupBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DataGridView queryResultDataGridView;
        private System.Windows.Forms.DataGridView queryParamDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn paramNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paramTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}