namespace Support.Forms.HardwareConfig
{
    partial class FrmConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeading = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.llSelect = new System.Windows.Forms.LinkLabel();
            this.llDeselect = new System.Windows.Forms.LinkLabel();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblIP2 = new System.Windows.Forms.Label();
            this.cbTOPrinter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgWorkstations = new System.Windows.Forms.DataGridView();
            this.wrkStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sp_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblIP = new System.Windows.Forms.Label();
            this.cbPrinterName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.rbPawn = new System.Windows.Forms.RadioButton();
            this.rbPDL = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgWorkstations1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgDevices1 = new System.Windows.Forms.DataGridView();
            this.pawnpdl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Device_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.peripheralid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose2 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnClose3 = new System.Windows.Forms.Button();
            this.dgDevices2 = new System.Windows.Forms.DataGridView();
            this.pdl_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgWorkstations2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workstation_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorkstations)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorkstations1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDevices1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDevices2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorkstations2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(276, 30);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(210, 23);
            this.lblHeading.TabIndex = 3;
            this.lblHeading.Text = "Hardware Configuration";
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
            this.cancelButton.Location = new System.Drawing.Point(25, 443);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(91, 32);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Close";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(6, 78);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 516);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.TabStop = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.BackgroundImage = global::Support.Properties.Resources.tab_back_800;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.llSelect);
            this.tabPage1.Controls.Add(this.llDeselect);
            this.tabPage1.Controls.Add(this.btnApply);
            this.tabPage1.Controls.Add(this.lblIP2);
            this.tabPage1.Controls.Add(this.cbTOPrinter);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dgWorkstations);
            this.tabPage1.Controls.Add(this.lblIP);
            this.tabPage1.Controls.Add(this.cbPrinterName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblSearch);
            this.tabPage1.Controls.Add(this.rbPawn);
            this.tabPage1.Controls.Add(this.rbPDL);
            this.tabPage1.Controls.Add(this.cancelButton);
            this.tabPage1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(780, 490);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Redirect Printer";
            // 
            // llSelect
            // 
            this.llSelect.ActiveLinkColor = System.Drawing.Color.Maroon;
            this.llSelect.AutoSize = true;
            this.llSelect.BackColor = System.Drawing.Color.Transparent;
            this.llSelect.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.llSelect.LinkColor = System.Drawing.Color.Maroon;
            this.llSelect.Location = new System.Drawing.Point(394, 378);
            this.llSelect.Name = "llSelect";
            this.llSelect.Size = new System.Drawing.Size(50, 13);
            this.llSelect.TabIndex = 30;
            this.llSelect.TabStop = true;
            this.llSelect.Text = "Select All";
            this.llSelect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSelect_LinkClicked);
            // 
            // llDeselect
            // 
            this.llDeselect.ActiveLinkColor = System.Drawing.Color.Maroon;
            this.llDeselect.AutoSize = true;
            this.llDeselect.BackColor = System.Drawing.Color.Transparent;
            this.llDeselect.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.llDeselect.LinkColor = System.Drawing.Color.Maroon;
            this.llDeselect.Location = new System.Drawing.Point(308, 378);
            this.llDeselect.Name = "llDeselect";
            this.llDeselect.Size = new System.Drawing.Size(62, 13);
            this.llDeselect.TabIndex = 29;
            this.llDeselect.TabStop = true;
            this.llDeselect.Text = "Deselect All";
            this.llDeselect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDeselect_LinkClicked);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.Transparent;
            this.btnApply.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(663, 443);
            this.btnApply.Margin = new System.Windows.Forms.Padding(0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(91, 32);
            this.btnApply.TabIndex = 26;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblIP2
            // 
            this.lblIP2.AutoSize = true;
            this.lblIP2.BackColor = System.Drawing.Color.Transparent;
            this.lblIP2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP2.Location = new System.Drawing.Point(492, 416);
            this.lblIP2.Name = "lblIP2";
            this.lblIP2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblIP2.Size = new System.Drawing.Size(24, 16);
            this.lblIP2.TabIndex = 25;
            this.lblIP2.Text = "IP:";
            this.lblIP2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbTOPrinter
            // 
            this.cbTOPrinter.BackColor = System.Drawing.Color.White;
            this.cbTOPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTOPrinter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTOPrinter.ForeColor = System.Drawing.Color.Black;
            this.cbTOPrinter.FormattingEnabled = true;
            this.cbTOPrinter.Location = new System.Drawing.Point(301, 413);
            this.cbTOPrinter.Name = "cbTOPrinter";
            this.cbTOPrinter.Size = new System.Drawing.Size(167, 24);
            this.cbTOPrinter.TabIndex = 24;
            this.cbTOPrinter.SelectedIndexChanged += new System.EventHandler(this.cbTOPrinter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(263, 421);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 19);
            this.label3.TabIndex = 23;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(187, 416);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "TO printer:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dgWorkstations
            // 
            this.dgWorkstations.AllowUserToAddRows = false;
            this.dgWorkstations.AllowUserToDeleteRows = false;
            this.dgWorkstations.AllowUserToResizeColumns = false;
            this.dgWorkstations.AllowUserToResizeRows = false;
            this.dgWorkstations.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgWorkstations.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgWorkstations.CausesValidation = false;
            this.dgWorkstations.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgWorkstations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgWorkstations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.wrkStation,
            this.sp_id});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgWorkstations.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgWorkstations.GridColor = System.Drawing.Color.Black;
            this.dgWorkstations.Location = new System.Drawing.Point(301, 118);
            this.dgWorkstations.Name = "dgWorkstations";
            this.dgWorkstations.ReadOnly = true;
            this.dgWorkstations.RowHeadersVisible = false;
            this.dgWorkstations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgWorkstations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWorkstations.Size = new System.Drawing.Size(155, 249);
            this.dgWorkstations.TabIndex = 21;
            // 
            // wrkStation
            // 
            this.wrkStation.HeaderText = "Workstation";
            this.wrkStation.Name = "wrkStation";
            this.wrkStation.ReadOnly = true;
            this.wrkStation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.wrkStation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.wrkStation.Width = 152;
            // 
            // sp_id
            // 
            this.sp_id.HeaderText = "sp_id";
            this.sp_id.Name = "sp_id";
            this.sp_id.ReadOnly = true;
            this.sp_id.Visible = false;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.BackColor = System.Drawing.Color.Transparent;
            this.lblIP.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(492, 72);
            this.lblIP.Name = "lblIP";
            this.lblIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblIP.Size = new System.Drawing.Size(24, 16);
            this.lblIP.TabIndex = 20;
            this.lblIP.Text = "IP:";
            this.lblIP.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbPrinterName
            // 
            this.cbPrinterName.BackColor = System.Drawing.Color.White;
            this.cbPrinterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrinterName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrinterName.ForeColor = System.Drawing.Color.Black;
            this.cbPrinterName.FormattingEnabled = true;
            this.cbPrinterName.Location = new System.Drawing.Point(301, 69);
            this.cbPrinterName.Name = "cbPrinterName";
            this.cbPrinterName.Size = new System.Drawing.Size(167, 24);
            this.cbPrinterName.TabIndex = 19;
            this.cbPrinterName.SelectedIndexChanged += new System.EventHandler(this.cbPrinterName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(262, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 19);
            this.label1.TabIndex = 18;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(44, 71);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSearch.Size = new System.Drawing.Size(215, 16);
            this.lblSearch.TabIndex = 17;
            this.lblSearch.Text = "Redirect all print jobs FROM printer:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // rbPawn
            // 
            this.rbPawn.AutoSize = true;
            this.rbPawn.BackColor = System.Drawing.Color.Transparent;
            this.rbPawn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPawn.Location = new System.Drawing.Point(396, 21);
            this.rbPawn.Name = "rbPawn";
            this.rbPawn.Size = new System.Drawing.Size(172, 20);
            this.rbPawn.TabIndex = 6;
            this.rbPawn.TabStop = true;
            this.rbPawn.Text = "For Pawn Transactions";
            this.rbPawn.UseVisualStyleBackColor = false;
            this.rbPawn.CheckedChanged += new System.EventHandler(this.rbPawn_CheckedChanged);
            // 
            // rbPDL
            // 
            this.rbPDL.AutoSize = true;
            this.rbPDL.BackColor = System.Drawing.Color.Transparent;
            this.rbPDL.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPDL.Location = new System.Drawing.Point(184, 21);
            this.rbPDL.Name = "rbPDL";
            this.rbPDL.Size = new System.Drawing.Size(161, 20);
            this.rbPDL.TabIndex = 5;
            this.rbPDL.TabStop = true;
            this.rbPDL.Text = "For PDL Transactions";
            this.rbPDL.UseVisualStyleBackColor = false;
            this.rbPDL.CheckedChanged += new System.EventHandler(this.rbPDL_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.BackgroundImage = global::Support.Properties.Resources.tab_back_800;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.dgWorkstations1);
            this.tabPage2.Controls.Add(this.dgDevices1);
            this.tabPage2.Controls.Add(this.btnClose2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 490);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Device View";
            // 
            // dgWorkstations1
            // 
            this.dgWorkstations1.AllowUserToAddRows = false;
            this.dgWorkstations1.AllowUserToDeleteRows = false;
            this.dgWorkstations1.AllowUserToResizeColumns = false;
            this.dgWorkstations1.AllowUserToResizeRows = false;
            this.dgWorkstations1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgWorkstations1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgWorkstations1.CausesValidation = false;
            this.dgWorkstations1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgWorkstations1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgWorkstations1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgWorkstations1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgWorkstations1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgWorkstations1.Enabled = false;
            this.dgWorkstations1.GridColor = System.Drawing.Color.Black;
            this.dgWorkstations1.Location = new System.Drawing.Point(635, 27);
            this.dgWorkstations1.Name = "dgWorkstations1";
            this.dgWorkstations1.ReadOnly = true;
            this.dgWorkstations1.RowHeadersVisible = false;
            this.dgWorkstations1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgWorkstations1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWorkstations1.Size = new System.Drawing.Size(128, 448);
            this.dgWorkstations1.TabIndex = 23;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Workstation";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dgDevices1
            // 
            this.dgDevices1.AllowUserToAddRows = false;
            this.dgDevices1.AllowUserToDeleteRows = false;
            this.dgDevices1.AllowUserToResizeColumns = false;
            this.dgDevices1.AllowUserToResizeRows = false;
            this.dgDevices1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgDevices1.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgDevices1.CausesValidation = false;
            this.dgDevices1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDevices1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pawnpdl,
            this.Device_Name,
            this.type,
            this.model,
            this.ip_address,
            this.peripheralid,
            this.serialnum});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDevices1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgDevices1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgDevices1.GridColor = System.Drawing.Color.Black;
            this.dgDevices1.Location = new System.Drawing.Point(6, 27);
            this.dgDevices1.MultiSelect = false;
            this.dgDevices1.Name = "dgDevices1";
            this.dgDevices1.ReadOnly = true;
            this.dgDevices1.RowHeadersVisible = false;
            this.dgDevices1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgDevices1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgDevices1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDevices1.Size = new System.Drawing.Size(595, 355);
            this.dgDevices1.TabIndex = 22;
            this.dgDevices1.SelectionChanged += new System.EventHandler(this.dgDevices1_SelectionChanged);
            // 
            // pawnpdl
            // 
            this.pawnpdl.HeaderText = "Pawn / PDL";
            this.pawnpdl.Name = "pawnpdl";
            this.pawnpdl.ReadOnly = true;
            this.pawnpdl.Width = 85;
            // 
            // Device_Name
            // 
            this.Device_Name.HeaderText = "Device Name";
            this.Device_Name.Name = "Device_Name";
            this.Device_Name.ReadOnly = true;
            this.Device_Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Device_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Device_Name.Width = 125;
            // 
            // type
            // 
            this.type.HeaderText = "Type";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 155;
            // 
            // model
            // 
            this.model.HeaderText = "Model";
            this.model.Name = "model";
            this.model.ReadOnly = true;
            this.model.Width = 120;
            // 
            // ip_address
            // 
            this.ip_address.HeaderText = "IP Address";
            this.ip_address.Name = "ip_address";
            this.ip_address.ReadOnly = true;
            this.ip_address.Width = 107;
            // 
            // peripheralid
            // 
            this.peripheralid.HeaderText = "peripheralid";
            this.peripheralid.Name = "peripheralid";
            this.peripheralid.ReadOnly = true;
            this.peripheralid.Visible = false;
            // 
            // serialnum
            // 
            this.serialnum.HeaderText = "serialnum";
            this.serialnum.Name = "serialnum";
            this.serialnum.ReadOnly = true;
            this.serialnum.Visible = false;
            // 
            // btnClose2
            // 
            this.btnClose2.BackColor = System.Drawing.Color.Transparent;
            this.btnClose2.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnClose2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose2.FlatAppearance.BorderSize = 0;
            this.btnClose2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose2.ForeColor = System.Drawing.Color.White;
            this.btnClose2.Location = new System.Drawing.Point(6, 443);
            this.btnClose2.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose2.Name = "btnClose2";
            this.btnClose2.Size = new System.Drawing.Size(91, 32);
            this.btnClose2.TabIndex = 5;
            this.btnClose2.Text = "Close";
            this.btnClose2.UseVisualStyleBackColor = false;
            this.btnClose2.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.BackgroundImage = global::Support.Properties.Resources.tab_back_800;
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.btnClose3);
            this.tabPage3.Controls.Add(this.dgDevices2);
            this.tabPage3.Controls.Add(this.dgWorkstations2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(780, 490);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Workstation View";
            // 
            // btnClose3
            // 
            this.btnClose3.BackColor = System.Drawing.Color.Transparent;
            this.btnClose3.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnClose3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose3.FlatAppearance.BorderSize = 0;
            this.btnClose3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose3.ForeColor = System.Drawing.Color.White;
            this.btnClose3.Location = new System.Drawing.Point(677, 443);
            this.btnClose3.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose3.Name = "btnClose3";
            this.btnClose3.Size = new System.Drawing.Size(91, 32);
            this.btnClose3.TabIndex = 27;
            this.btnClose3.Text = "Close";
            this.btnClose3.UseVisualStyleBackColor = false;
            this.btnClose3.Click += new System.EventHandler(this.btnClose3_Click);
            // 
            // dgDevices2
            // 
            this.dgDevices2.AllowUserToAddRows = false;
            this.dgDevices2.AllowUserToDeleteRows = false;
            this.dgDevices2.AllowUserToResizeColumns = false;
            this.dgDevices2.AllowUserToResizeRows = false;
            this.dgDevices2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgDevices2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgDevices2.CausesValidation = false;
            this.dgDevices2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDevices2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pdl_type,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDevices2.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgDevices2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgDevices2.Enabled = false;
            this.dgDevices2.GridColor = System.Drawing.Color.Black;
            this.dgDevices2.Location = new System.Drawing.Point(173, 27);
            this.dgDevices2.MultiSelect = false;
            this.dgDevices2.Name = "dgDevices2";
            this.dgDevices2.ReadOnly = true;
            this.dgDevices2.RowHeadersVisible = false;
            this.dgDevices2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgDevices2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgDevices2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDevices2.Size = new System.Drawing.Size(595, 355);
            this.dgDevices2.TabIndex = 25;
            // 
            // pdl_type
            // 
            this.pdl_type.HeaderText = "PDL / Pawn";
            this.pdl_type.Name = "pdl_type";
            this.pdl_type.ReadOnly = true;
            this.pdl_type.Width = 85;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Device Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Type";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 155;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Model";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "IP Address";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 107;
            // 
            // dgWorkstations2
            // 
            this.dgWorkstations2.AllowUserToAddRows = false;
            this.dgWorkstations2.AllowUserToDeleteRows = false;
            this.dgWorkstations2.AllowUserToResizeColumns = false;
            this.dgWorkstations2.AllowUserToResizeRows = false;
            this.dgWorkstations2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgWorkstations2.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgWorkstations2.CausesValidation = false;
            this.dgWorkstations2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgWorkstations2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgWorkstations2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.workstation_id});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgWorkstations2.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgWorkstations2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgWorkstations2.GridColor = System.Drawing.Color.Black;
            this.dgWorkstations2.Location = new System.Drawing.Point(6, 27);
            this.dgWorkstations2.MultiSelect = false;
            this.dgWorkstations2.Name = "dgWorkstations2";
            this.dgWorkstations2.ReadOnly = true;
            this.dgWorkstations2.RowHeadersVisible = false;
            this.dgWorkstations2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgWorkstations2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWorkstations2.Size = new System.Drawing.Size(138, 448);
            this.dgWorkstations2.TabIndex = 24;
            this.dgWorkstations2.SelectionChanged += new System.EventHandler(this.dgWorkstations2_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Workstation";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 135;
            // 
            // workstation_id
            // 
            this.workstation_id.HeaderText = "workstation_id";
            this.workstation_id.Name = "workstation_id";
            this.workstation_id.ReadOnly = true;
            this.workstation_id.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Workstation";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 153;
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_600;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmConfig";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorkstations)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWorkstations1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDevices1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDevices2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorkstations2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClose2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton rbPawn;
        private System.Windows.Forms.RadioButton rbPDL;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPrinterName;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.DataGridView dgWorkstations;
        private System.Windows.Forms.Label lblIP2;
        private System.Windows.Forms.ComboBox cbTOPrinter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.LinkLabel llSelect;
        private System.Windows.Forms.LinkLabel llDeselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn wrkStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn sp_id;
        private System.Windows.Forms.DataGridView dgWorkstations1;
        private System.Windows.Forms.DataGridView dgDevices1;
        private System.Windows.Forms.DataGridView dgDevices2;
        private System.Windows.Forms.DataGridView dgWorkstations2;
        private System.Windows.Forms.DataGridViewTextBoxColumn pawnpdl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Device_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn model;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip_address;
        private System.Windows.Forms.DataGridViewTextBoxColumn peripheralid;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn pdl_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn workstation_id;
        private System.Windows.Forms.Button btnClose3;
    }
}