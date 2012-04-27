namespace CashlinxDesktopLoadTester
{
    partial class Phase2LoadTestForm
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
            this.components = new System.ComponentModel.Container();
            this.dbLoadStatusStrip = new System.Windows.Forms.StatusStrip();
            this.dbStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dbTestStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.executionTimeToolStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.executionTimeDataField = new System.Windows.Forms.ToolStripStatusLabel();
            this.loadTestMenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberUserLabel = new System.Windows.Forms.Label();
            this.loadTestMessageListBox = new System.Windows.Forms.ListBox();
            this.inputDataLabel = new System.Windows.Forms.Label();
            this.inputBrowseButton = new System.Windows.Forms.Button();
            this.executeFlowComboBox = new System.Windows.Forms.ComboBox();
            this.flowLabel = new System.Windows.Forms.Label();
            this.progressionLabel = new System.Windows.Forms.Label();
            this.progressionRateComboBox = new System.Windows.Forms.ComboBox();
            this.messageBoxLabel = new System.Windows.Forms.Label();
            this.mainInputGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.delayUserTextBox = new System.Windows.Forms.TextBox();
            this.numberIterationsTextBox = new System.Windows.Forms.TextBox();
            this.numberOfIterationsLabel = new System.Windows.Forms.Label();
            this.delayBetweenUserLabel = new System.Windows.Forms.Label();
            this.numberUsersComboBox = new System.Windows.Forms.ComboBox();
            this.inputFilenameTextBox = new System.Windows.Forms.TextBox();
            this.inputGroupBox = new System.Windows.Forms.GroupBox();
            this.inputObjectPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.openInputFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.monitorGroupBox = new System.Windows.Forms.GroupBox();
            this.physicalMemTextBox = new System.Windows.Forms.TextBox();
            this.physicalMemoryLabel = new System.Windows.Forms.Label();
            this.privateMemTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numThreadsTextBox = new System.Windows.Forms.TextBox();
            this.numThreadsLabel = new System.Windows.Forms.Label();
            this.virtMemTextBox = new System.Windows.Forms.TextBox();
            this.virtMemLabel = new System.Windows.Forms.Label();
            this.userCpuTimeTextBox = new System.Windows.Forms.TextBox();
            this.userCpuTimeLabel = new System.Windows.Forms.Label();
            this.totalProcTimeTextbox = new System.Windows.Forms.TextBox();
            this.privCpuTimeTextBox = new System.Windows.Forms.TextBox();
            this.peakPhysicalMemTextBox = new System.Windows.Forms.TextBox();
            this.peakVirtualMemTextBox = new System.Windows.Forms.TextBox();
            this.peakPagedMemTextBox = new System.Windows.Forms.TextBox();
            this.pagedSysMemTextBox = new System.Windows.Forms.TextBox();
            this.pagedMemTextBox = new System.Windows.Forms.TextBox();
            this.nonPagedMemTextBox = new System.Windows.Forms.TextBox();
            this.totalProcTimeLabel = new System.Windows.Forms.Label();
            this.pagedMemoryLabel = new System.Windows.Forms.Label();
            this.peakPhysicalMemLabel = new System.Windows.Forms.Label();
            this.peakVirtualMemLabel = new System.Windows.Forms.Label();
            this.peakPageMemLabel = new System.Windows.Forms.Label();
            this.pagedSysMemLabel = new System.Windows.Forms.Label();
            this.privProcTimeLabel = new System.Windows.Forms.Label();
            this.memTotalBytesHeapLabel = new System.Windows.Forms.Label();
            this.controlGroupBox = new System.Windows.Forms.GroupBox();
            this.pauseButton = new System.Windows.Forms.Button();
            this.stopLoadTest = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.uiThreadsCheckBox = new System.Windows.Forms.CheckBox();
            this.cpuUtilPerformanceCounter = new System.Diagnostics.PerformanceCounter();
            this.cpuUsrPerformanceCounter = new System.Diagnostics.PerformanceCounter();
            this.netBytesRecPerformanceCounter = new System.Diagnostics.PerformanceCounter();
            this.netBytesSentPerformanceCounter = new System.Diagnostics.PerformanceCounter();
            this.statusChecker = new System.ComponentModel.BackgroundWorker();
            this.procDataUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.dbLoadStatusStrip.SuspendLayout();
            this.loadTestMenuStrip1.SuspendLayout();
            this.mainInputGroupBox.SuspendLayout();
            this.inputGroupBox.SuspendLayout();
            this.monitorGroupBox.SuspendLayout();
            this.controlGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUtilPerformanceCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsrPerformanceCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.netBytesRecPerformanceCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.netBytesSentPerformanceCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // dbLoadStatusStrip
            // 
            this.dbLoadStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbStripStatusLabel,
            this.dbTestStripProgressBar,
            this.toolStripStatusLabel2,
            this.executionTimeToolStripLabel,
            this.executionTimeDataField});
            this.dbLoadStatusStrip.Location = new System.Drawing.Point(0, 593);
            this.dbLoadStatusStrip.Name = "dbLoadStatusStrip";
            this.dbLoadStatusStrip.Size = new System.Drawing.Size(873, 22);
            this.dbLoadStatusStrip.TabIndex = 0;
            this.dbLoadStatusStrip.Text = "dbLoadStatusStrip";
            // 
            // dbStripStatusLabel
            // 
            this.dbStripStatusLabel.Name = "dbStripStatusLabel";
            this.dbStripStatusLabel.Size = new System.Drawing.Size(54, 17);
            this.dbStripStatusLabel.Tag = "dbStatusLabel";
            this.dbStripStatusLabel.Text = "DB Status";
            this.dbStripStatusLabel.ToolTipText = "dbStatus";
            this.dbStripStatusLabel.Visible = false;
            // 
            // dbTestStripProgressBar
            // 
            this.dbTestStripProgressBar.Name = "dbTestStripProgressBar";
            this.dbTestStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.dbTestStripProgressBar.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(858, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "                               ";
            // 
            // executionTimeToolStripLabel
            // 
            this.executionTimeToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.executionTimeToolStripLabel.Name = "executionTimeToolStripLabel";
            this.executionTimeToolStripLabel.Size = new System.Drawing.Size(83, 17);
            this.executionTimeToolStripLabel.Text = "Execution Time:";
            this.executionTimeToolStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.executionTimeToolStripLabel.Visible = false;
            // 
            // executionTimeDataField
            // 
            this.executionTimeDataField.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.executionTimeDataField.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.executionTimeDataField.Name = "executionTimeDataField";
            this.executionTimeDataField.Size = new System.Drawing.Size(89, 17);
            this.executionTimeDataField.Text = "HH:MM:SS.mmm";
            this.executionTimeDataField.Visible = false;
            // 
            // loadTestMenuStrip1
            // 
            this.loadTestMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem});
            this.loadTestMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.loadTestMenuStrip1.Name = "loadTestMenuStrip1";
            this.loadTestMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.loadTestMenuStrip1.Size = new System.Drawing.Size(873, 24);
            this.loadTestMenuStrip1.TabIndex = 1;
            this.loadTestMenuStrip1.Text = "loadTestMenuStrip";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.systemToolStripMenuItem.Text = "&System";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // numberUserLabel
            // 
            this.numberUserLabel.AutoSize = true;
            this.numberUserLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberUserLabel.Location = new System.Drawing.Point(39, 23);
            this.numberUserLabel.Name = "numberUserLabel";
            this.numberUserLabel.Size = new System.Drawing.Size(74, 13);
            this.numberUserLabel.TabIndex = 2;
            this.numberUserLabel.Text = "Number Users";
            // 
            // loadTestMessageListBox
            // 
            this.loadTestMessageListBox.BackColor = System.Drawing.Color.White;
            this.loadTestMessageListBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadTestMessageListBox.Location = new System.Drawing.Point(12, 482);
            this.loadTestMessageListBox.Name = "loadTestMessageListBox";
            this.loadTestMessageListBox.ScrollAlwaysVisible = true;
            this.loadTestMessageListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.loadTestMessageListBox.Size = new System.Drawing.Size(849, 108);
            this.loadTestMessageListBox.TabIndex = 4;
            // 
            // inputDataLabel
            // 
            this.inputDataLabel.AutoSize = true;
            this.inputDataLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputDataLabel.Location = new System.Drawing.Point(34, 77);
            this.inputDataLabel.Name = "inputDataLabel";
            this.inputDataLabel.Size = new System.Drawing.Size(78, 13);
            this.inputDataLabel.TabIndex = 5;
            this.inputDataLabel.Text = "Input Filename";
            this.inputDataLabel.Visible = false;
            // 
            // inputBrowseButton
            // 
            this.inputBrowseButton.BackColor = System.Drawing.Color.LightGray;
            this.inputBrowseButton.Enabled = false;
            this.inputBrowseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.inputBrowseButton.Location = new System.Drawing.Point(275, 72);
            this.inputBrowseButton.Name = "inputBrowseButton";
            this.inputBrowseButton.Size = new System.Drawing.Size(99, 23);
            this.inputBrowseButton.TabIndex = 6;
            this.inputBrowseButton.Text = "Browse...";
            this.inputBrowseButton.UseVisualStyleBackColor = false;
            this.inputBrowseButton.Visible = false;
            this.inputBrowseButton.Click += new System.EventHandler(this.inputBrowseButton_Click);
            // 
            // executeFlowComboBox
            // 
            this.executeFlowComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.executeFlowComboBox.ForeColor = System.Drawing.Color.Black;
            this.executeFlowComboBox.FormattingEnabled = true;
            this.executeFlowComboBox.Items.AddRange(new object[] {
            "New Loan (Existing Customer)"});
            this.executeFlowComboBox.Location = new System.Drawing.Point(119, 101);
            this.executeFlowComboBox.Name = "executeFlowComboBox";
            this.executeFlowComboBox.Size = new System.Drawing.Size(151, 21);
            this.executeFlowComboBox.TabIndex = 7;
            this.executeFlowComboBox.SelectedIndexChanged += new System.EventHandler(this.executeFlowComboBox_SelectedIndexChanged);
            // 
            // flowLabel
            // 
            this.flowLabel.AutoSize = true;
            this.flowLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLabel.Location = new System.Drawing.Point(27, 104);
            this.flowLabel.Name = "flowLabel";
            this.flowLabel.Size = new System.Drawing.Size(86, 13);
            this.flowLabel.TabIndex = 8;
            this.flowLabel.Text = "Flow To Execute";
            // 
            // progressionLabel
            // 
            this.progressionLabel.AutoSize = true;
            this.progressionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressionLabel.Location = new System.Drawing.Point(23, 131);
            this.progressionLabel.Name = "progressionLabel";
            this.progressionLabel.Size = new System.Drawing.Size(89, 13);
            this.progressionLabel.TabIndex = 9;
            this.progressionLabel.Text = "Progression Rate";
            // 
            // progressionRateComboBox
            // 
            this.progressionRateComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.progressionRateComboBox.ForeColor = System.Drawing.Color.Black;
            this.progressionRateComboBox.FormattingEnabled = true;
            this.progressionRateComboBox.Items.AddRange(new object[] {
            "Arithmetic",
            "Geometric",
            "Random"});
            this.progressionRateComboBox.Location = new System.Drawing.Point(119, 128);
            this.progressionRateComboBox.Name = "progressionRateComboBox";
            this.progressionRateComboBox.Size = new System.Drawing.Size(151, 21);
            this.progressionRateComboBox.TabIndex = 10;
            this.progressionRateComboBox.SelectedIndexChanged += new System.EventHandler(this.progressionRateComboBox_SelectedIndexChanged);
            // 
            // messageBoxLabel
            // 
            this.messageBoxLabel.AutoSize = true;
            this.messageBoxLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageBoxLabel.Location = new System.Drawing.Point(13, 397);
            this.messageBoxLabel.Name = "messageBoxLabel";
            this.messageBoxLabel.Size = new System.Drawing.Size(54, 13);
            this.messageBoxLabel.TabIndex = 11;
            this.messageBoxLabel.Text = "Messages";
            // 
            // mainInputGroupBox
            // 
            this.mainInputGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.mainInputGroupBox.Controls.Add(this.label1);
            this.mainInputGroupBox.Controls.Add(this.delayUserTextBox);
            this.mainInputGroupBox.Controls.Add(this.numberIterationsTextBox);
            this.mainInputGroupBox.Controls.Add(this.numberOfIterationsLabel);
            this.mainInputGroupBox.Controls.Add(this.delayBetweenUserLabel);
            this.mainInputGroupBox.Controls.Add(this.numberUsersComboBox);
            this.mainInputGroupBox.Controls.Add(this.inputFilenameTextBox);
            this.mainInputGroupBox.Controls.Add(this.inputBrowseButton);
            this.mainInputGroupBox.Controls.Add(this.progressionRateComboBox);
            this.mainInputGroupBox.Controls.Add(this.progressionLabel);
            this.mainInputGroupBox.Controls.Add(this.executeFlowComboBox);
            this.mainInputGroupBox.Controls.Add(this.flowLabel);
            this.mainInputGroupBox.Controls.Add(this.numberUserLabel);
            this.mainInputGroupBox.Controls.Add(this.inputDataLabel);
            this.mainInputGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainInputGroupBox.Location = new System.Drawing.Point(16, 37);
            this.mainInputGroupBox.Name = "mainInputGroupBox";
            this.mainInputGroupBox.Size = new System.Drawing.Size(428, 199);
            this.mainInputGroupBox.TabIndex = 12;
            this.mainInputGroupBox.TabStop = false;
            this.mainInputGroupBox.Text = "Load Parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "(ms)";
            // 
            // delayUserTextBox
            // 
            this.delayUserTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.delayUserTextBox.ForeColor = System.Drawing.Color.Black;
            this.delayUserTextBox.Location = new System.Drawing.Point(118, 155);
            this.delayUserTextBox.MaxLength = 5;
            this.delayUserTextBox.Name = "delayUserTextBox";
            this.delayUserTextBox.Size = new System.Drawing.Size(151, 21);
            this.delayUserTextBox.TabIndex = 19;
            // 
            // numberIterationsTextBox
            // 
            this.numberIterationsTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.numberIterationsTextBox.ForeColor = System.Drawing.Color.Black;
            this.numberIterationsTextBox.Location = new System.Drawing.Point(119, 47);
            this.numberIterationsTextBox.MaxLength = 3;
            this.numberIterationsTextBox.Name = "numberIterationsTextBox";
            this.numberIterationsTextBox.Size = new System.Drawing.Size(151, 21);
            this.numberIterationsTextBox.TabIndex = 18;
            this.numberIterationsTextBox.Text = "1";
            this.numberIterationsTextBox.TextChanged += new System.EventHandler(this.numberIterationsTextBox_TextChanged);
            // 
            // numberOfIterationsLabel
            // 
            this.numberOfIterationsLabel.AutoSize = true;
            this.numberOfIterationsLabel.Location = new System.Drawing.Point(34, 50);
            this.numberOfIterationsLabel.Name = "numberOfIterationsLabel";
            this.numberOfIterationsLabel.Size = new System.Drawing.Size(78, 13);
            this.numberOfIterationsLabel.TabIndex = 17;
            this.numberOfIterationsLabel.Text = "# of Iterations";
            // 
            // delayBetweenUserLabel
            // 
            this.delayBetweenUserLabel.AutoSize = true;
            this.delayBetweenUserLabel.Location = new System.Drawing.Point(3, 157);
            this.delayBetweenUserLabel.Name = "delayBetweenUserLabel";
            this.delayBetweenUserLabel.Size = new System.Drawing.Size(109, 13);
            this.delayBetweenUserLabel.TabIndex = 16;
            this.delayBetweenUserLabel.Text = "Delay Between Users";
            // 
            // numberUsersComboBox
            // 
            this.numberUsersComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.numberUsersComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.numberUsersComboBox.ForeColor = System.Drawing.Color.Black;
            this.numberUsersComboBox.FormattingEnabled = true;
            this.numberUsersComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10",
            "25",
            "50",
            "100",
            "250",
            "500",
            "1000",
            "2500",
            "5000"});
            this.numberUsersComboBox.Location = new System.Drawing.Point(119, 20);
            this.numberUsersComboBox.Name = "numberUsersComboBox";
            this.numberUsersComboBox.Size = new System.Drawing.Size(151, 21);
            this.numberUsersComboBox.TabIndex = 15;
            this.numberUsersComboBox.SelectedIndexChanged += new System.EventHandler(this.numberUsersComboBox_SelectedIndexChanged);
            // 
            // inputFilenameTextBox
            // 
            this.inputFilenameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.inputFilenameTextBox.Enabled = false;
            this.inputFilenameTextBox.ForeColor = System.Drawing.Color.Black;
            this.inputFilenameTextBox.Location = new System.Drawing.Point(119, 74);
            this.inputFilenameTextBox.Name = "inputFilenameTextBox";
            this.inputFilenameTextBox.ReadOnly = true;
            this.inputFilenameTextBox.Size = new System.Drawing.Size(151, 21);
            this.inputFilenameTextBox.TabIndex = 14;
            this.inputFilenameTextBox.Visible = false;
            // 
            // inputGroupBox
            // 
            this.inputGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.inputGroupBox.Controls.Add(this.inputObjectPropertyGrid);
            this.inputGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputGroupBox.Location = new System.Drawing.Point(16, 242);
            this.inputGroupBox.Name = "inputGroupBox";
            this.inputGroupBox.Size = new System.Drawing.Size(282, 234);
            this.inputGroupBox.TabIndex = 13;
            this.inputGroupBox.TabStop = false;
            this.inputGroupBox.Text = "Application Inputs";
            // 
            // inputObjectPropertyGrid
            // 
            this.inputObjectPropertyGrid.Location = new System.Drawing.Point(6, 20);
            this.inputObjectPropertyGrid.Name = "inputObjectPropertyGrid";
            this.inputObjectPropertyGrid.Size = new System.Drawing.Size(264, 208);
            this.inputObjectPropertyGrid.TabIndex = 0;
            this.inputObjectPropertyGrid.ToolbarVisible = false;
            // 
            // openInputFileDialog
            // 
            this.openInputFileDialog.FileName = "openInputFileDialog";
            this.openInputFileDialog.Filter = "\"CSV Files|*.csv|All Files|*.*\"";
            this.openInputFileDialog.InitialDirectory = "c:\\";
            this.openInputFileDialog.Title = "Open LoadTest Input File";
            // 
            // monitorGroupBox
            // 
            this.monitorGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.monitorGroupBox.Controls.Add(this.physicalMemTextBox);
            this.monitorGroupBox.Controls.Add(this.physicalMemoryLabel);
            this.monitorGroupBox.Controls.Add(this.privateMemTextBox);
            this.monitorGroupBox.Controls.Add(this.label2);
            this.monitorGroupBox.Controls.Add(this.numThreadsTextBox);
            this.monitorGroupBox.Controls.Add(this.numThreadsLabel);
            this.monitorGroupBox.Controls.Add(this.virtMemTextBox);
            this.monitorGroupBox.Controls.Add(this.virtMemLabel);
            this.monitorGroupBox.Controls.Add(this.userCpuTimeTextBox);
            this.monitorGroupBox.Controls.Add(this.userCpuTimeLabel);
            this.monitorGroupBox.Controls.Add(this.totalProcTimeTextbox);
            this.monitorGroupBox.Controls.Add(this.privCpuTimeTextBox);
            this.monitorGroupBox.Controls.Add(this.peakPhysicalMemTextBox);
            this.monitorGroupBox.Controls.Add(this.peakVirtualMemTextBox);
            this.monitorGroupBox.Controls.Add(this.peakPagedMemTextBox);
            this.monitorGroupBox.Controls.Add(this.pagedSysMemTextBox);
            this.monitorGroupBox.Controls.Add(this.pagedMemTextBox);
            this.monitorGroupBox.Controls.Add(this.nonPagedMemTextBox);
            this.monitorGroupBox.Controls.Add(this.totalProcTimeLabel);
            this.monitorGroupBox.Controls.Add(this.pagedMemoryLabel);
            this.monitorGroupBox.Controls.Add(this.peakPhysicalMemLabel);
            this.monitorGroupBox.Controls.Add(this.peakVirtualMemLabel);
            this.monitorGroupBox.Controls.Add(this.peakPageMemLabel);
            this.monitorGroupBox.Controls.Add(this.pagedSysMemLabel);
            this.monitorGroupBox.Controls.Add(this.privProcTimeLabel);
            this.monitorGroupBox.Controls.Add(this.memTotalBytesHeapLabel);
            this.monitorGroupBox.Location = new System.Drawing.Point(450, 37);
            this.monitorGroupBox.Name = "monitorGroupBox";
            this.monitorGroupBox.Size = new System.Drawing.Size(411, 354);
            this.monitorGroupBox.TabIndex = 14;
            this.monitorGroupBox.TabStop = false;
            this.monitorGroupBox.Text = "Performance Monitor";
            // 
            // physicalMemTextBox
            // 
            this.physicalMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.physicalMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.physicalMemTextBox.Location = new System.Drawing.Point(146, 21);
            this.physicalMemTextBox.Name = "physicalMemTextBox";
            this.physicalMemTextBox.ReadOnly = true;
            this.physicalMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.physicalMemTextBox.TabIndex = 25;
            this.physicalMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.physicalMemTextBox.WordWrap = false;
            // 
            // physicalMemoryLabel
            // 
            this.physicalMemoryLabel.AutoSize = true;
            this.physicalMemoryLabel.Location = new System.Drawing.Point(54, 24);
            this.physicalMemoryLabel.Name = "physicalMemoryLabel";
            this.physicalMemoryLabel.Size = new System.Drawing.Size(86, 13);
            this.physicalMemoryLabel.TabIndex = 24;
            this.physicalMemoryLabel.Text = "Physical Memory";
            this.physicalMemoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // privateMemTextBox
            // 
            this.privateMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.privateMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.privateMemTextBox.Location = new System.Drawing.Point(146, 42);
            this.privateMemTextBox.Name = "privateMemTextBox";
            this.privateMemTextBox.ReadOnly = true;
            this.privateMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.privateMemTextBox.TabIndex = 23;
            this.privateMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.privateMemTextBox.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Private Memory";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numThreadsTextBox
            // 
            this.numThreadsTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.numThreadsTextBox.ForeColor = System.Drawing.Color.Black;
            this.numThreadsTextBox.Location = new System.Drawing.Point(146, 276);
            this.numThreadsTextBox.Name = "numThreadsTextBox";
            this.numThreadsTextBox.ReadOnly = true;
            this.numThreadsTextBox.Size = new System.Drawing.Size(207, 21);
            this.numThreadsTextBox.TabIndex = 21;
            this.numThreadsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numThreadsTextBox.WordWrap = false;
            // 
            // numThreadsLabel
            // 
            this.numThreadsLabel.AutoSize = true;
            this.numThreadsLabel.Location = new System.Drawing.Point(54, 279);
            this.numThreadsLabel.Name = "numThreadsLabel";
            this.numThreadsLabel.Size = new System.Drawing.Size(86, 13);
            this.numThreadsLabel.TabIndex = 20;
            this.numThreadsLabel.Text = "Number Threads";
            this.numThreadsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // virtMemTextBox
            // 
            this.virtMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.virtMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.virtMemTextBox.Location = new System.Drawing.Point(146, 63);
            this.virtMemTextBox.Name = "virtMemTextBox";
            this.virtMemTextBox.ReadOnly = true;
            this.virtMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.virtMemTextBox.TabIndex = 19;
            this.virtMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.virtMemTextBox.WordWrap = false;
            // 
            // virtMemLabel
            // 
            this.virtMemLabel.AutoSize = true;
            this.virtMemLabel.Location = new System.Drawing.Point(61, 66);
            this.virtMemLabel.Name = "virtMemLabel";
            this.virtMemLabel.Size = new System.Drawing.Size(78, 13);
            this.virtMemLabel.TabIndex = 18;
            this.virtMemLabel.Text = "Virtual Memory";
            this.virtMemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // userCpuTimeTextBox
            // 
            this.userCpuTimeTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.userCpuTimeTextBox.ForeColor = System.Drawing.Color.Black;
            this.userCpuTimeTextBox.Location = new System.Drawing.Point(146, 254);
            this.userCpuTimeTextBox.Name = "userCpuTimeTextBox";
            this.userCpuTimeTextBox.ReadOnly = true;
            this.userCpuTimeTextBox.Size = new System.Drawing.Size(207, 21);
            this.userCpuTimeTextBox.TabIndex = 17;
            this.userCpuTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.userCpuTimeTextBox.WordWrap = false;
            // 
            // userCpuTimeLabel
            // 
            this.userCpuTimeLabel.AutoSize = true;
            this.userCpuTimeLabel.Location = new System.Drawing.Point(63, 257);
            this.userCpuTimeLabel.Name = "userCpuTimeLabel";
            this.userCpuTimeLabel.Size = new System.Drawing.Size(77, 13);
            this.userCpuTimeLabel.TabIndex = 16;
            this.userCpuTimeLabel.Text = "User CPU Time";
            this.userCpuTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalProcTimeTextbox
            // 
            this.totalProcTimeTextbox.BackColor = System.Drawing.Color.Gainsboro;
            this.totalProcTimeTextbox.ForeColor = System.Drawing.Color.Black;
            this.totalProcTimeTextbox.Location = new System.Drawing.Point(146, 232);
            this.totalProcTimeTextbox.Name = "totalProcTimeTextbox";
            this.totalProcTimeTextbox.ReadOnly = true;
            this.totalProcTimeTextbox.Size = new System.Drawing.Size(207, 21);
            this.totalProcTimeTextbox.TabIndex = 15;
            this.totalProcTimeTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.totalProcTimeTextbox.WordWrap = false;
            // 
            // privCpuTimeTextBox
            // 
            this.privCpuTimeTextBox.AcceptsReturn = true;
            this.privCpuTimeTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.privCpuTimeTextBox.ForeColor = System.Drawing.Color.Black;
            this.privCpuTimeTextBox.Location = new System.Drawing.Point(146, 211);
            this.privCpuTimeTextBox.Name = "privCpuTimeTextBox";
            this.privCpuTimeTextBox.ReadOnly = true;
            this.privCpuTimeTextBox.Size = new System.Drawing.Size(207, 21);
            this.privCpuTimeTextBox.TabIndex = 14;
            this.privCpuTimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.privCpuTimeTextBox.WordWrap = false;
            // 
            // peakPhysicalMemTextBox
            // 
            this.peakPhysicalMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.peakPhysicalMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.peakPhysicalMemTextBox.Location = new System.Drawing.Point(146, 189);
            this.peakPhysicalMemTextBox.Name = "peakPhysicalMemTextBox";
            this.peakPhysicalMemTextBox.ReadOnly = true;
            this.peakPhysicalMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.peakPhysicalMemTextBox.TabIndex = 13;
            this.peakPhysicalMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.peakPhysicalMemTextBox.WordWrap = false;
            // 
            // peakVirtualMemTextBox
            // 
            this.peakVirtualMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.peakVirtualMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.peakVirtualMemTextBox.Location = new System.Drawing.Point(146, 168);
            this.peakVirtualMemTextBox.Name = "peakVirtualMemTextBox";
            this.peakVirtualMemTextBox.ReadOnly = true;
            this.peakVirtualMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.peakVirtualMemTextBox.TabIndex = 12;
            this.peakVirtualMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.peakVirtualMemTextBox.WordWrap = false;
            // 
            // peakPagedMemTextBox
            // 
            this.peakPagedMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.peakPagedMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.peakPagedMemTextBox.Location = new System.Drawing.Point(146, 147);
            this.peakPagedMemTextBox.Name = "peakPagedMemTextBox";
            this.peakPagedMemTextBox.ReadOnly = true;
            this.peakPagedMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.peakPagedMemTextBox.TabIndex = 11;
            this.peakPagedMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.peakPagedMemTextBox.WordWrap = false;
            // 
            // pagedSysMemTextBox
            // 
            this.pagedSysMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.pagedSysMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.pagedSysMemTextBox.Location = new System.Drawing.Point(146, 126);
            this.pagedSysMemTextBox.Name = "pagedSysMemTextBox";
            this.pagedSysMemTextBox.ReadOnly = true;
            this.pagedSysMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.pagedSysMemTextBox.TabIndex = 10;
            this.pagedSysMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pagedSysMemTextBox.WordWrap = false;
            // 
            // pagedMemTextBox
            // 
            this.pagedMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.pagedMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.pagedMemTextBox.Location = new System.Drawing.Point(146, 105);
            this.pagedMemTextBox.Name = "pagedMemTextBox";
            this.pagedMemTextBox.ReadOnly = true;
            this.pagedMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.pagedMemTextBox.TabIndex = 9;
            this.pagedMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pagedMemTextBox.WordWrap = false;
            // 
            // nonPagedMemTextBox
            // 
            this.nonPagedMemTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.nonPagedMemTextBox.ForeColor = System.Drawing.Color.Black;
            this.nonPagedMemTextBox.Location = new System.Drawing.Point(146, 84);
            this.nonPagedMemTextBox.Name = "nonPagedMemTextBox";
            this.nonPagedMemTextBox.ReadOnly = true;
            this.nonPagedMemTextBox.Size = new System.Drawing.Size(207, 21);
            this.nonPagedMemTextBox.TabIndex = 8;
            this.nonPagedMemTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nonPagedMemTextBox.WordWrap = false;
            // 
            // totalProcTimeLabel
            // 
            this.totalProcTimeLabel.AutoSize = true;
            this.totalProcTimeLabel.Location = new System.Drawing.Point(60, 235);
            this.totalProcTimeLabel.Name = "totalProcTimeLabel";
            this.totalProcTimeLabel.Size = new System.Drawing.Size(79, 13);
            this.totalProcTimeLabel.TabIndex = 7;
            this.totalProcTimeLabel.Text = "Total CPU Time";
            this.totalProcTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pagedMemoryLabel
            // 
            this.pagedMemoryLabel.AutoSize = true;
            this.pagedMemoryLabel.Location = new System.Drawing.Point(61, 108);
            this.pagedMemoryLabel.Name = "pagedMemoryLabel";
            this.pagedMemoryLabel.Size = new System.Drawing.Size(78, 13);
            this.pagedMemoryLabel.TabIndex = 6;
            this.pagedMemoryLabel.Text = "Paged Memory";
            this.pagedMemoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // peakPhysicalMemLabel
            // 
            this.peakPhysicalMemLabel.AutoSize = true;
            this.peakPhysicalMemLabel.Location = new System.Drawing.Point(28, 192);
            this.peakPhysicalMemLabel.Name = "peakPhysicalMemLabel";
            this.peakPhysicalMemLabel.Size = new System.Drawing.Size(112, 13);
            this.peakPhysicalMemLabel.TabIndex = 5;
            this.peakPhysicalMemLabel.Text = "Peak Physical Memory";
            this.peakPhysicalMemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // peakVirtualMemLabel
            // 
            this.peakVirtualMemLabel.AutoSize = true;
            this.peakVirtualMemLabel.Location = new System.Drawing.Point(36, 171);
            this.peakVirtualMemLabel.Name = "peakVirtualMemLabel";
            this.peakVirtualMemLabel.Size = new System.Drawing.Size(104, 13);
            this.peakVirtualMemLabel.TabIndex = 4;
            this.peakVirtualMemLabel.Text = "Peak Virtual Memory";
            this.peakVirtualMemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // peakPageMemLabel
            // 
            this.peakPageMemLabel.AutoSize = true;
            this.peakPageMemLabel.Location = new System.Drawing.Point(35, 150);
            this.peakPageMemLabel.Name = "peakPageMemLabel";
            this.peakPageMemLabel.Size = new System.Drawing.Size(104, 13);
            this.peakPageMemLabel.TabIndex = 3;
            this.peakPageMemLabel.Text = "Peak Paged Memory";
            this.peakPageMemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pagedSysMemLabel
            // 
            this.pagedSysMemLabel.AutoSize = true;
            this.pagedSysMemLabel.Location = new System.Drawing.Point(24, 129);
            this.pagedSysMemLabel.Name = "pagedSysMemLabel";
            this.pagedSysMemLabel.Size = new System.Drawing.Size(116, 13);
            this.pagedSysMemLabel.TabIndex = 2;
            this.pagedSysMemLabel.Text = "Paged System Memory";
            this.pagedSysMemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // privProcTimeLabel
            // 
            this.privProcTimeLabel.AutoSize = true;
            this.privProcTimeLabel.Location = new System.Drawing.Point(32, 214);
            this.privProcTimeLabel.Name = "privProcTimeLabel";
            this.privProcTimeLabel.Size = new System.Drawing.Size(107, 13);
            this.privProcTimeLabel.TabIndex = 1;
            this.privProcTimeLabel.Text = "Priviledged CPU Time";
            this.privProcTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // memTotalBytesHeapLabel
            // 
            this.memTotalBytesHeapLabel.AutoSize = true;
            this.memTotalBytesHeapLabel.Location = new System.Drawing.Point(39, 87);
            this.memTotalBytesHeapLabel.Name = "memTotalBytesHeapLabel";
            this.memTotalBytesHeapLabel.Size = new System.Drawing.Size(100, 13);
            this.memTotalBytesHeapLabel.TabIndex = 0;
            this.memTotalBytesHeapLabel.Text = "Non Paged Memory";
            this.memTotalBytesHeapLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // controlGroupBox
            // 
            this.controlGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.controlGroupBox.Controls.Add(this.pauseButton);
            this.controlGroupBox.Controls.Add(this.stopLoadTest);
            this.controlGroupBox.Controls.Add(this.startButton);
            this.controlGroupBox.Location = new System.Drawing.Point(304, 243);
            this.controlGroupBox.Name = "controlGroupBox";
            this.controlGroupBox.Size = new System.Drawing.Size(140, 233);
            this.controlGroupBox.TabIndex = 15;
            this.controlGroupBox.TabStop = false;
            this.controlGroupBox.Text = "Controls";
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pauseButton.Location = new System.Drawing.Point(15, 69);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(102, 23);
            this.pauseButton.TabIndex = 2;
            this.pauseButton.Text = "Pause Load Test";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Visible = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopLoadTest
            // 
            this.stopLoadTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.stopLoadTest.Location = new System.Drawing.Point(15, 104);
            this.stopLoadTest.Name = "stopLoadTest";
            this.stopLoadTest.Size = new System.Drawing.Size(102, 23);
            this.stopLoadTest.TabIndex = 1;
            this.stopLoadTest.Text = "Stop Load Test";
            this.stopLoadTest.UseVisualStyleBackColor = true;
            this.stopLoadTest.Click += new System.EventHandler(this.stopLoadTest_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.LightGray;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.startButton.Location = new System.Drawing.Point(15, 34);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(102, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Load Test";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.optionsGroupBox.Controls.Add(this.uiThreadsCheckBox);
            this.optionsGroupBox.Location = new System.Drawing.Point(450, 397);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(411, 79);
            this.optionsGroupBox.TabIndex = 16;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            this.optionsGroupBox.Visible = false;
            // 
            // uiThreadsCheckBox
            // 
            this.uiThreadsCheckBox.AutoSize = true;
            this.uiThreadsCheckBox.Checked = true;
            this.uiThreadsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uiThreadsCheckBox.Enabled = false;
            this.uiThreadsCheckBox.Location = new System.Drawing.Point(16, 20);
            this.uiThreadsCheckBox.Name = "uiThreadsCheckBox";
            this.uiThreadsCheckBox.Size = new System.Drawing.Size(114, 17);
            this.uiThreadsCheckBox.TabIndex = 0;
            this.uiThreadsCheckBox.Text = "Enable UI Threads";
            this.uiThreadsCheckBox.UseVisualStyleBackColor = true;
            this.uiThreadsCheckBox.Visible = false;
            // 
            // cpuUtilPerformanceCounter
            // 
            this.cpuUtilPerformanceCounter.CategoryName = "Process";
            this.cpuUtilPerformanceCounter.CounterName = "% Processor Time";
            this.cpuUtilPerformanceCounter.InstanceName = "CashlinxDesktopLoadTester.vshost";
            // 
            // cpuUsrPerformanceCounter
            // 
            this.cpuUsrPerformanceCounter.CategoryName = "Process";
            this.cpuUsrPerformanceCounter.CounterName = "% User Time";
            this.cpuUsrPerformanceCounter.InstanceName = "CashlinxDesktopLoadTester.vshost";
            // 
            // netBytesRecPerformanceCounter
            // 
            this.netBytesRecPerformanceCounter.CategoryName = ".NET CLR Networking";
            this.netBytesRecPerformanceCounter.CounterName = "Bytes Received";
            this.netBytesRecPerformanceCounter.InstanceName = "CashlinxDesktopLoadTester.vshost";
            // 
            // netBytesSentPerformanceCounter
            // 
            this.netBytesSentPerformanceCounter.CategoryName = ".NET CLR Networking";
            this.netBytesSentPerformanceCounter.CounterName = "Bytes Sent";
            this.netBytesSentPerformanceCounter.InstanceName = "CashlinxDesktopLoadTester.vshost";
            // 
            // statusChecker
            // 
            this.statusChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.statusChecker_DoWork);
            // 
            // procDataUpdateTimer
            // 
            this.procDataUpdateTimer.Interval = 500;
            this.procDataUpdateTimer.Tick += new System.EventHandler(this.procDataUpdateTimer_Tick);
            // 
            // Phase2LoadTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(873, 615);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.controlGroupBox);
            this.Controls.Add(this.monitorGroupBox);
            this.Controls.Add(this.inputGroupBox);
            this.Controls.Add(this.messageBoxLabel);
            this.Controls.Add(this.loadTestMessageListBox);
            this.Controls.Add(this.dbLoadStatusStrip);
            this.Controls.Add(this.loadTestMenuStrip1);
            this.Controls.Add(this.mainInputGroupBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.loadTestMenuStrip1;
            this.MaximumSize = new System.Drawing.Size(879, 640);
            this.MinimumSize = new System.Drawing.Size(879, 640);
            this.Name = "Phase2LoadTestForm";
            this.Text = "CashlinxDesktop Load Tester";
            this.Load += new System.EventHandler(this.Phase2LoadTestForm_Load);
            this.dbLoadStatusStrip.ResumeLayout(false);
            this.dbLoadStatusStrip.PerformLayout();
            this.loadTestMenuStrip1.ResumeLayout(false);
            this.loadTestMenuStrip1.PerformLayout();
            this.mainInputGroupBox.ResumeLayout(false);
            this.mainInputGroupBox.PerformLayout();
            this.inputGroupBox.ResumeLayout(false);
            this.monitorGroupBox.ResumeLayout(false);
            this.monitorGroupBox.PerformLayout();
            this.controlGroupBox.ResumeLayout(false);
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUtilPerformanceCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuUsrPerformanceCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.netBytesRecPerformanceCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.netBytesSentPerformanceCounter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip dbLoadStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel dbStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar dbTestStripProgressBar;
        private System.Windows.Forms.MenuStrip loadTestMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label numberUserLabel;
        private System.Windows.Forms.ListBox loadTestMessageListBox;
        private System.Windows.Forms.Label inputDataLabel;
        private System.Windows.Forms.Button inputBrowseButton;
        private System.Windows.Forms.ComboBox executeFlowComboBox;
        private System.Windows.Forms.Label flowLabel;
        private System.Windows.Forms.Label progressionLabel;
        private System.Windows.Forms.ComboBox progressionRateComboBox;
        private System.Windows.Forms.Label messageBoxLabel;
        private System.Windows.Forms.GroupBox mainInputGroupBox;
        private System.Windows.Forms.ComboBox numberUsersComboBox;
        private System.Windows.Forms.TextBox inputFilenameTextBox;
        private System.Windows.Forms.GroupBox inputGroupBox;
        private System.Windows.Forms.OpenFileDialog openInputFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel executionTimeToolStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel executionTimeDataField;
        private System.Windows.Forms.PropertyGrid inputObjectPropertyGrid;
        private System.Windows.Forms.GroupBox monitorGroupBox;
        private System.Windows.Forms.Label memTotalBytesHeapLabel;
        private System.Windows.Forms.GroupBox controlGroupBox;
        private System.Windows.Forms.Label peakPageMemLabel;
        private System.Windows.Forms.Label pagedSysMemLabel;
        private System.Windows.Forms.Label privProcTimeLabel;
        private System.Windows.Forms.Label peakPhysicalMemLabel;
        private System.Windows.Forms.Label peakVirtualMemLabel;
        private System.Windows.Forms.Button stopLoadTest;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.Label pagedMemoryLabel;
        private System.Windows.Forms.Label totalProcTimeLabel;
        private System.Windows.Forms.TextBox totalProcTimeTextbox;
        private System.Windows.Forms.TextBox privCpuTimeTextBox;
        private System.Windows.Forms.TextBox peakPhysicalMemTextBox;
        private System.Windows.Forms.TextBox peakVirtualMemTextBox;
        private System.Windows.Forms.TextBox peakPagedMemTextBox;
        private System.Windows.Forms.TextBox pagedSysMemTextBox;
        private System.Windows.Forms.TextBox pagedMemTextBox;
        private System.Windows.Forms.TextBox nonPagedMemTextBox;
        private System.Windows.Forms.Label delayBetweenUserLabel;
        private System.Windows.Forms.Button pauseButton;
        private System.Diagnostics.PerformanceCounter cpuUtilPerformanceCounter;
        private System.Diagnostics.PerformanceCounter cpuUsrPerformanceCounter;
        private System.Diagnostics.PerformanceCounter netBytesRecPerformanceCounter;
        private System.Diagnostics.PerformanceCounter netBytesSentPerformanceCounter;
        private System.Windows.Forms.Label numberOfIterationsLabel;
        private System.Windows.Forms.TextBox numberIterationsTextBox;
        private System.Windows.Forms.TextBox delayUserTextBox;
        private System.Windows.Forms.CheckBox uiThreadsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker statusChecker;
        private System.Windows.Forms.TextBox numThreadsTextBox;
        private System.Windows.Forms.Label numThreadsLabel;
        private System.Windows.Forms.TextBox virtMemTextBox;
        private System.Windows.Forms.Label virtMemLabel;
        private System.Windows.Forms.TextBox userCpuTimeTextBox;
        private System.Windows.Forms.Label userCpuTimeLabel;
        private System.Windows.Forms.TextBox privateMemTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox physicalMemTextBox;
        private System.Windows.Forms.Label physicalMemoryLabel;
        private System.Windows.Forms.Timer procDataUpdateTimer;
    }
}

