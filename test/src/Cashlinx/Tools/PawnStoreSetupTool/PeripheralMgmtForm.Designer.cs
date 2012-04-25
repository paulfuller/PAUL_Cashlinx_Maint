namespace PawnStoreSetupTool
{
    partial class PeripheralMgmtForm
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
            this.peripheralPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.addButton = new System.Windows.Forms.Button();
            this.peripheralListView = new System.Windows.Forms.ListView();
            this.peripheralNameColumn = new System.Windows.Forms.ColumnHeader();
            this.peripheralTypeColumn = new System.Windows.Forms.ColumnHeader();
            this.peripheralIPColumn = new System.Windows.Forms.ColumnHeader();
            this.peripheralPortColumn = new System.Windows.Forms.ColumnHeader();
            this.newPeripheralLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.doneButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.workstationListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.mapPeripheralButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.deMapButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // peripheralPropertyGrid
            // 
            this.peripheralPropertyGrid.BackColor = System.Drawing.Color.Gainsboro;
            this.peripheralPropertyGrid.CategoryForeColor = System.Drawing.Color.Black;
            this.peripheralPropertyGrid.CommandsVisibleIfAvailable = false;
            this.peripheralPropertyGrid.HelpBackColor = System.Drawing.Color.Gainsboro;
            this.peripheralPropertyGrid.HelpForeColor = System.Drawing.Color.Black;
            this.peripheralPropertyGrid.HelpVisible = false;
            this.peripheralPropertyGrid.LineColor = System.Drawing.Color.DarkGray;
            this.peripheralPropertyGrid.Location = new System.Drawing.Point(11, 201);
            this.peripheralPropertyGrid.MaximumSize = new System.Drawing.Size(260, 255);
            this.peripheralPropertyGrid.MinimumSize = new System.Drawing.Size(260, 255);
            this.peripheralPropertyGrid.Name = "peripheralPropertyGrid";
            this.peripheralPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.peripheralPropertyGrid.Size = new System.Drawing.Size(260, 255);
            this.peripheralPropertyGrid.TabIndex = 0;
            this.peripheralPropertyGrid.ToolbarVisible = false;
            this.peripheralPropertyGrid.ViewBackColor = System.Drawing.Color.White;
            this.peripheralPropertyGrid.ViewForeColor = System.Drawing.Color.Black;
            this.peripheralPropertyGrid.Click += new System.EventHandler(this.peripheralPropertyGrid_Click);
            this.peripheralPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.peripheralPropertyGrid_PropertyValueChanged);
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(11, 462);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(80, 44);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // peripheralListView
            // 
            this.peripheralListView.BackColor = System.Drawing.Color.White;
            this.peripheralListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.peripheralNameColumn,
            this.peripheralTypeColumn,
            this.peripheralIPColumn,
            this.peripheralPortColumn});
            this.peripheralListView.ForeColor = System.Drawing.Color.Black;
            this.peripheralListView.FullRowSelect = true;
            this.peripheralListView.GridLines = true;
            this.peripheralListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.peripheralListView.HideSelection = false;
            this.peripheralListView.Location = new System.Drawing.Point(11, 28);
            this.peripheralListView.MultiSelect = false;
            this.peripheralListView.Name = "peripheralListView";
            this.peripheralListView.ShowGroups = false;
            this.peripheralListView.Size = new System.Drawing.Size(574, 126);
            this.peripheralListView.TabIndex = 2;
            this.peripheralListView.UseCompatibleStateImageBehavior = false;
            this.peripheralListView.View = System.Windows.Forms.View.Details;
            this.peripheralListView.SelectedIndexChanged += new System.EventHandler(this.peripheralListView_SelectedIndexChanged);
            // 
            // peripheralNameColumn
            // 
            this.peripheralNameColumn.Text = "Name";
            this.peripheralNameColumn.Width = 139;
            // 
            // peripheralTypeColumn
            // 
            this.peripheralTypeColumn.Text = "Type";
            this.peripheralTypeColumn.Width = 154;
            // 
            // peripheralIPColumn
            // 
            this.peripheralIPColumn.Text = "IP";
            this.peripheralIPColumn.Width = 181;
            // 
            // peripheralPortColumn
            // 
            this.peripheralPortColumn.Text = "Port";
            this.peripheralPortColumn.Width = 95;
            // 
            // newPeripheralLabel
            // 
            this.newPeripheralLabel.AutoSize = true;
            this.newPeripheralLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newPeripheralLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPeripheralLabel.Location = new System.Drawing.Point(12, 182);
            this.newPeripheralLabel.Name = "newPeripheralLabel";
            this.newPeripheralLabel.Size = new System.Drawing.Size(105, 16);
            this.newPeripheralLabel.TabIndex = 3;
            this.newPeripheralLabel.Text = "New Peripheral";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Peripherals";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(231, 524);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(133, 44);
            this.doneButton.TabIndex = 5;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(9, 168);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(576, 2);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(576, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(576, 2);
            this.pictureBox1.TabIndex = 88;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(6, 511);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.MaximumSize = new System.Drawing.Size(582, 2);
            this.pictureBox2.MinimumSize = new System.Drawing.Size(582, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(582, 2);
            this.pictureBox2.TabIndex = 89;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox3.InitialImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(296, 201);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox3.MaximumSize = new System.Drawing.Size(2, 255);
            this.pictureBox3.MinimumSize = new System.Drawing.Size(2, 255);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(2, 255);
            this.pictureBox3.TabIndex = 91;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(442, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 16);
            this.label1.TabIndex = 92;
            this.label1.Text = "Workstation Mapping";
            // 
            // workstationListView
            // 
            this.workstationListView.BackColor = System.Drawing.Color.White;
            this.workstationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.workstationListView.ForeColor = System.Drawing.Color.Black;
            this.workstationListView.FullRowSelect = true;
            this.workstationListView.GridLines = true;
            this.workstationListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.workstationListView.HideSelection = false;
            this.workstationListView.Location = new System.Drawing.Point(322, 201);
            this.workstationListView.MaximumSize = new System.Drawing.Size(260, 255);
            this.workstationListView.MinimumSize = new System.Drawing.Size(260, 255);
            this.workstationListView.MultiSelect = false;
            this.workstationListView.Name = "workstationListView";
            this.workstationListView.ShowGroups = false;
            this.workstationListView.Size = new System.Drawing.Size(260, 255);
            this.workstationListView.TabIndex = 93;
            this.workstationListView.UseCompatibleStateImageBehavior = false;
            this.workstationListView.View = System.Windows.Forms.View.Details;
            this.workstationListView.SelectedIndexChanged += new System.EventHandler(this.workstationListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Workstations";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "# of Peripherals";
            this.columnHeader2.Width = 115;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(95, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 94;
            this.label3.Text = "(Select to Edit Or Map)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mapPeripheralButton
            // 
            this.mapPeripheralButton.Enabled = false;
            this.mapPeripheralButton.Location = new System.Drawing.Point(479, 462);
            this.mapPeripheralButton.Name = "mapPeripheralButton";
            this.mapPeripheralButton.Size = new System.Drawing.Size(103, 44);
            this.mapPeripheralButton.TabIndex = 95;
            this.mapPeripheralButton.Text = "Map";
            this.mapPeripheralButton.UseVisualStyleBackColor = true;
            this.mapPeripheralButton.Click += new System.EventHandler(this.mapButton_Click);
            // 
            // editButton
            // 
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(101, 462);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(80, 44);
            this.editButton.TabIndex = 96;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(191, 462);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(80, 44);
            this.removeButton.TabIndex = 97;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // deMapButton
            // 
            this.deMapButton.Enabled = false;
            this.deMapButton.Location = new System.Drawing.Point(322, 462);
            this.deMapButton.Name = "deMapButton";
            this.deMapButton.Size = new System.Drawing.Size(103, 44);
            this.deMapButton.TabIndex = 98;
            this.deMapButton.Text = "DeMap ™";
            this.deMapButton.UseVisualStyleBackColor = true;
            this.deMapButton.Click += new System.EventHandler(this.deMapButton_Click);
            // 
            // PeripheralMgmtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(594, 576);
            this.ControlBox = false;
            this.Controls.Add(this.deMapButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.mapPeripheralButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.workstationListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newPeripheralLabel);
            this.Controls.Add(this.peripheralListView);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.peripheralPropertyGrid);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "PeripheralMgmtForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Peripheral Management";
            this.Load += new System.EventHandler(this.PeripheralMgmtForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid peripheralPropertyGrid;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView peripheralListView;
        private System.Windows.Forms.Label newPeripheralLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader peripheralNameColumn;
        private System.Windows.Forms.ColumnHeader peripheralTypeColumn;
        private System.Windows.Forms.ColumnHeader peripheralIPColumn;
        private System.Windows.Forms.ColumnHeader peripheralPortColumn;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView workstationListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mapPeripheralButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button deMapButton;

    }
}