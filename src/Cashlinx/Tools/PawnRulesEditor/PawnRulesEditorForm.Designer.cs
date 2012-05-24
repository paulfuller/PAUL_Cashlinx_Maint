namespace PawnRulesEditor
{
    partial class PawnRulesEditor
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("AliasChildName");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("AliasName", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Aliases", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Rule Name");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Component Name");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Component Type");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("BusinessRuleComponentRef", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("BusinessRule", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Component Name");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Component Alias");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Component Type");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Business Rule Component", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("BusinessRules", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode8,
            treeNode12});
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainRulesEditorStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainRulesEngineProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.rulesEditorMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRulesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRulesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rulesDataTreeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.commonRulesElementsGroupBox = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.aliasComboBox = new System.Windows.Forms.ComboBox();
            this.AliasLabel = new System.Windows.Forms.Label();
            this.InterestRuleComponentGroupBox = new System.Windows.Forms.GroupBox();
            this.ParameterRuleComponentData = new System.Windows.Forms.GroupBox();
            this.FeeRuleComponentData = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ruleComponentNameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ruleComponentTypeBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.storeNumberComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.paramStateTextBox = new System.Windows.Forms.TextBox();
            this.isEditableCheckBox = new System.Windows.Forms.CheckBox();
            this.companyComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.IsCacheableCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.serviceAmountTextBox = new System.Windows.Forms.TextBox();
            this.serviceRateTextBox = new System.Windows.Forms.TextBox();
            this.interestLevelTextBox = new System.Windows.Forms.TextBox();
            this.interestTypeTextBox = new System.Windows.Forms.TextBox();
            this.feeValueTextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.feeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.feeLookupTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addComponent = new System.Windows.Forms.Button();
            this.editComponentButton = new System.Windows.Forms.Button();
            this.addRuleButton = new System.Windows.Forms.Button();
            this.removeComponentButton = new System.Windows.Forms.Button();
            this.editRuleButton = new System.Windows.Forms.Button();
            this.deleteRuleButton = new System.Windows.Forms.Button();
            this.componentModifyGroupBox = new System.Windows.Forms.GroupBox();
            this.ruleModifyGroupBox = new System.Windows.Forms.GroupBox();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateEntireTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateAliasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateComponentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.mainStatusStrip.SuspendLayout();
            this.rulesEditorMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.commonRulesElementsGroupBox.SuspendLayout();
            this.InterestRuleComponentGroupBox.SuspendLayout();
            this.ParameterRuleComponentData.SuspendLayout();
            this.FeeRuleComponentData.SuspendLayout();
            this.componentModifyGroupBox.SuspendLayout();
            this.ruleModifyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainRulesEditorStatusLabel,
            this.mainRulesEngineProgressBar});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 699);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1190, 22);
            this.mainStatusStrip.TabIndex = 0;
            // 
            // mainRulesEditorStatusLabel
            // 
            this.mainRulesEditorStatusLabel.Name = "mainRulesEditorStatusLabel";
            this.mainRulesEditorStatusLabel.Size = new System.Drawing.Size(139, 17);
            this.mainRulesEditorStatusLabel.Text = "Main Rules Engine Status";
            // 
            // mainRulesEngineProgressBar
            // 
            this.mainRulesEngineProgressBar.Name = "mainRulesEngineProgressBar";
            this.mainRulesEngineProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // rulesEditorMenuStrip
            // 
            this.rulesEditorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.validateToolStripMenuItem});
            this.rulesEditorMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.rulesEditorMenuStrip.Name = "rulesEditorMenuStrip";
            this.rulesEditorMenuStrip.Size = new System.Drawing.Size(1190, 24);
            this.rulesEditorMenuStrip.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRulesFileToolStripMenuItem,
            this.saveRulesFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadRulesFileToolStripMenuItem
            // 
            this.loadRulesFileToolStripMenuItem.Name = "loadRulesFileToolStripMenuItem";
            this.loadRulesFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadRulesFileToolStripMenuItem.Text = "&Load Rules File...";
            this.loadRulesFileToolStripMenuItem.Click += new System.EventHandler(this.loadRulesFileToolStripMenuItem_Click);
            // 
            // saveRulesFileToolStripMenuItem
            // 
            this.saveRulesFileToolStripMenuItem.Name = "saveRulesFileToolStripMenuItem";
            this.saveRulesFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveRulesFileToolStripMenuItem.Text = "&Save Rules File...";
            this.saveRulesFileToolStripMenuItem.Click += new System.EventHandler(this.saveRulesFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ruleModifyGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.componentModifyGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.rulesDataTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.commonRulesElementsGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(1190, 675);
            this.splitContainer1.SplitterDistance = 463;
            this.splitContainer1.TabIndex = 2;
            // 
            // rulesDataTreeView
            // 
            this.rulesDataTreeView.HideSelection = false;
            this.rulesDataTreeView.Location = new System.Drawing.Point(16, 27);
            this.rulesDataTreeView.Name = "rulesDataTreeView";
            treeNode1.Name = "AliasChildNameNode";
            treeNode1.Text = "AliasChildName";
            treeNode2.Name = "AliasNameNode";
            treeNode2.Text = "AliasName";
            treeNode3.Name = "AliasesNode";
            treeNode3.Text = "Aliases";
            treeNode3.ToolTipText = "Aliases allowed to be assigned to business rules";
            treeNode4.Name = "BusinessRuleNameNode";
            treeNode4.Text = "Rule Name";
            treeNode5.Name = "BusinessRuleComponentRefName";
            treeNode5.Text = "Component Name";
            treeNode5.ToolTipText = "Name of the rule component";
            treeNode6.Name = "BusinessRuleComponentRefTypeNode";
            treeNode6.Text = "Component Type";
            treeNode6.ToolTipText = "Type of the business rule component";
            treeNode7.Name = "BusinessRuleComponentRefNode";
            treeNode7.Text = "BusinessRuleComponentRef";
            treeNode7.ToolTipText = "A rule component reference for business rule structure definition";
            treeNode8.Name = "BusinessRuleNode";
            treeNode8.Text = "BusinessRule";
            treeNode9.Name = "BusinessRuleComponentNameNode";
            treeNode9.Text = "Component Name";
            treeNode10.Name = "BusinessRuleComponentAliasNode";
            treeNode10.Text = "Component Alias";
            treeNode11.Name = "BusinessRuleComponentTypeNode";
            treeNode11.Text = "Component Type";
            treeNode12.Name = "BusinessRuleComponentNode";
            treeNode12.Text = "Business Rule Component";
            treeNode13.Name = "BusinessRulesNode";
            treeNode13.Text = "BusinessRules";
            this.rulesDataTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13});
            this.rulesDataTreeView.ShowNodeToolTips = true;
            this.rulesDataTreeView.Size = new System.Drawing.Size(433, 555);
            this.rulesDataTreeView.TabIndex = 0;
            this.rulesDataTreeView.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.rulesDataTreeView_NodeMouseHover);
            this.rulesDataTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.rulesDataTreeView_AfterSelect);
            this.rulesDataTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.rulesDataTreeView_NodeMouseClick);
            this.rulesDataTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.rulesDataTreeView_NodeMouseDoubleClick);
            this.rulesDataTreeView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rulesDataTreeView_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rules Data";
            // 
            // commonRulesElementsGroupBox
            // 
            this.commonRulesElementsGroupBox.Controls.Add(this.textBox6);
            this.commonRulesElementsGroupBox.Controls.Add(this.textBox1);
            this.commonRulesElementsGroupBox.Controls.Add(this.label23);
            this.commonRulesElementsGroupBox.Controls.Add(this.label22);
            this.commonRulesElementsGroupBox.Controls.Add(this.cancelButton);
            this.commonRulesElementsGroupBox.Controls.Add(this.submitButton);
            this.commonRulesElementsGroupBox.Controls.Add(this.isEditableCheckBox);
            this.commonRulesElementsGroupBox.Controls.Add(this.ruleComponentTypeBox);
            this.commonRulesElementsGroupBox.Controls.Add(this.label5);
            this.commonRulesElementsGroupBox.Controls.Add(this.ruleComponentNameBox);
            this.commonRulesElementsGroupBox.Controls.Add(this.label4);
            this.commonRulesElementsGroupBox.Controls.Add(this.FeeRuleComponentData);
            this.commonRulesElementsGroupBox.Controls.Add(this.ParameterRuleComponentData);
            this.commonRulesElementsGroupBox.Controls.Add(this.InterestRuleComponentGroupBox);
            this.commonRulesElementsGroupBox.Controls.Add(this.AliasLabel);
            this.commonRulesElementsGroupBox.Controls.Add(this.aliasComboBox);
            this.commonRulesElementsGroupBox.Controls.Add(this.label3);
            this.commonRulesElementsGroupBox.Controls.Add(this.dateTimePicker2);
            this.commonRulesElementsGroupBox.Controls.Add(this.label2);
            this.commonRulesElementsGroupBox.Controls.Add(this.dateTimePicker1);
            this.commonRulesElementsGroupBox.Enabled = false;
            this.commonRulesElementsGroupBox.Location = new System.Drawing.Point(3, 8);
            this.commonRulesElementsGroupBox.Name = "commonRulesElementsGroupBox";
            this.commonRulesElementsGroupBox.Size = new System.Drawing.Size(708, 658);
            this.commonRulesElementsGroupBox.TabIndex = 0;
            this.commonRulesElementsGroupBox.TabStop = false;
            this.commonRulesElementsGroupBox.Text = "Rule Component Data";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(72, 53);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "From Date:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(333, 53);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "To Date:";
            // 
            // aliasComboBox
            // 
            this.aliasComboBox.FormattingEnabled = true;
            this.aliasComboBox.Items.AddRange(new object[] {
            "All",
            "TX"});
            this.aliasComboBox.Location = new System.Drawing.Point(71, 82);
            this.aliasComboBox.Name = "aliasComboBox";
            this.aliasComboBox.Size = new System.Drawing.Size(200, 21);
            this.aliasComboBox.TabIndex = 4;
            // 
            // AliasLabel
            // 
            this.AliasLabel.AutoSize = true;
            this.AliasLabel.Location = new System.Drawing.Point(6, 86);
            this.AliasLabel.Name = "AliasLabel";
            this.AliasLabel.Size = new System.Drawing.Size(32, 13);
            this.AliasLabel.TabIndex = 5;
            this.AliasLabel.Text = "Alias:";
            // 
            // InterestRuleComponentGroupBox
            // 
            this.InterestRuleComponentGroupBox.Controls.Add(this.interestTypeTextBox);
            this.InterestRuleComponentGroupBox.Controls.Add(this.interestLevelTextBox);
            this.InterestRuleComponentGroupBox.Controls.Add(this.serviceRateTextBox);
            this.InterestRuleComponentGroupBox.Controls.Add(this.serviceAmountTextBox);
            this.InterestRuleComponentGroupBox.Controls.Add(this.textBox5);
            this.InterestRuleComponentGroupBox.Controls.Add(this.textBox4);
            this.InterestRuleComponentGroupBox.Controls.Add(this.textBox3);
            this.InterestRuleComponentGroupBox.Controls.Add(this.textBox2);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label18);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label17);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label16);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label15);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label14);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label13);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label12);
            this.InterestRuleComponentGroupBox.Controls.Add(this.label11);
            this.InterestRuleComponentGroupBox.Enabled = false;
            this.InterestRuleComponentGroupBox.Location = new System.Drawing.Point(6, 147);
            this.InterestRuleComponentGroupBox.Name = "InterestRuleComponentGroupBox";
            this.InterestRuleComponentGroupBox.Size = new System.Drawing.Size(676, 160);
            this.InterestRuleComponentGroupBox.TabIndex = 6;
            this.InterestRuleComponentGroupBox.TabStop = false;
            this.InterestRuleComponentGroupBox.Text = "Interest Rule Component Data";
            // 
            // ParameterRuleComponentData
            // 
            this.ParameterRuleComponentData.Controls.Add(this.IsCacheableCheckBox);
            this.ParameterRuleComponentData.Controls.Add(this.companyComboBox);
            this.ParameterRuleComponentData.Controls.Add(this.label10);
            this.ParameterRuleComponentData.Controls.Add(this.paramStateTextBox);
            this.ParameterRuleComponentData.Controls.Add(this.label9);
            this.ParameterRuleComponentData.Controls.Add(this.storeNumberComboBox);
            this.ParameterRuleComponentData.Controls.Add(this.label8);
            this.ParameterRuleComponentData.Controls.Add(this.valueTextBox);
            this.ParameterRuleComponentData.Controls.Add(this.label7);
            this.ParameterRuleComponentData.Controls.Add(this.dataTypeComboBox);
            this.ParameterRuleComponentData.Controls.Add(this.label6);
            this.ParameterRuleComponentData.Enabled = false;
            this.ParameterRuleComponentData.Location = new System.Drawing.Point(6, 313);
            this.ParameterRuleComponentData.Name = "ParameterRuleComponentData";
            this.ParameterRuleComponentData.Size = new System.Drawing.Size(676, 160);
            this.ParameterRuleComponentData.TabIndex = 7;
            this.ParameterRuleComponentData.TabStop = false;
            this.ParameterRuleComponentData.Text = "Param Rule Component Data";
            // 
            // FeeRuleComponentData
            // 
            this.FeeRuleComponentData.Controls.Add(this.feeLookupTypeComboBox);
            this.FeeRuleComponentData.Controls.Add(this.label21);
            this.FeeRuleComponentData.Controls.Add(this.feeTypeComboBox);
            this.FeeRuleComponentData.Controls.Add(this.label20);
            this.FeeRuleComponentData.Controls.Add(this.feeValueTextBox);
            this.FeeRuleComponentData.Controls.Add(this.label19);
            this.FeeRuleComponentData.Enabled = false;
            this.FeeRuleComponentData.Location = new System.Drawing.Point(6, 479);
            this.FeeRuleComponentData.Name = "FeeRuleComponentData";
            this.FeeRuleComponentData.Size = new System.Drawing.Size(349, 173);
            this.FeeRuleComponentData.TabIndex = 8;
            this.FeeRuleComponentData.TabStop = false;
            this.FeeRuleComponentData.Text = "Fees Rule Component Data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name:";
            // 
            // ruleComponentNameBox
            // 
            this.ruleComponentNameBox.Location = new System.Drawing.Point(69, 21);
            this.ruleComponentNameBox.Name = "ruleComponentNameBox";
            this.ruleComponentNameBox.Size = new System.Drawing.Size(464, 20);
            this.ruleComponentNameBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(293, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Type:";
            // 
            // ruleComponentTypeBox
            // 
            this.ruleComponentTypeBox.FormattingEnabled = true;
            this.ruleComponentTypeBox.Items.AddRange(new object[] {
            "PARAM",
            "INTEREST",
            "FEES"});
            this.ruleComponentTypeBox.Location = new System.Drawing.Point(333, 83);
            this.ruleComponentTypeBox.Name = "ruleComponentTypeBox";
            this.ruleComponentTypeBox.Size = new System.Drawing.Size(200, 21);
            this.ruleComponentTypeBox.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Data Type:";
            // 
            // dataTypeComboBox
            // 
            this.dataTypeComboBox.FormattingEnabled = true;
            this.dataTypeComboBox.Location = new System.Drawing.Point(121, 26);
            this.dataTypeComboBox.Name = "dataTypeComboBox";
            this.dataTypeComboBox.Size = new System.Drawing.Size(200, 21);
            this.dataTypeComboBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Value:";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(118, 55);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(203, 20);
            this.valueTextBox.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Store Number:";
            // 
            // storeNumberComboBox
            // 
            this.storeNumberComboBox.FormattingEnabled = true;
            this.storeNumberComboBox.Location = new System.Drawing.Point(118, 83);
            this.storeNumberComboBox.Name = "storeNumberComboBox";
            this.storeNumberComboBox.Size = new System.Drawing.Size(204, 21);
            this.storeNumberComboBox.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "State:";
            // 
            // paramStateTextBox
            // 
            this.paramStateTextBox.Location = new System.Drawing.Point(117, 109);
            this.paramStateTextBox.Name = "paramStateTextBox";
            this.paramStateTextBox.Size = new System.Drawing.Size(205, 20);
            this.paramStateTextBox.TabIndex = 11;
            // 
            // isEditableCheckBox
            // 
            this.isEditableCheckBox.AutoSize = true;
            this.isEditableCheckBox.Location = new System.Drawing.Point(539, 58);
            this.isEditableCheckBox.Name = "isEditableCheckBox";
            this.isEditableCheckBox.Size = new System.Drawing.Size(81, 17);
            this.isEditableCheckBox.TabIndex = 13;
            this.isEditableCheckBox.Text = "Is Editable?";
            this.isEditableCheckBox.UseVisualStyleBackColor = true;
            // 
            // companyComboBox
            // 
            this.companyComboBox.FormattingEnabled = true;
            this.companyComboBox.Items.AddRange(new object[] {
            "Cash America"});
            this.companyComboBox.Location = new System.Drawing.Point(474, 26);
            this.companyComboBox.Name = "companyComboBox";
            this.companyComboBox.Size = new System.Drawing.Size(196, 21);
            this.companyComboBox.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(387, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Company:";
            // 
            // IsCacheableCheckBox
            // 
            this.IsCacheableCheckBox.AutoSize = true;
            this.IsCacheableCheckBox.Location = new System.Drawing.Point(117, 135);
            this.IsCacheableCheckBox.Name = "IsCacheableCheckBox";
            this.IsCacheableCheckBox.Size = new System.Drawing.Size(94, 17);
            this.IsCacheableCheckBox.TabIndex = 14;
            this.IsCacheableCheckBox.Text = "Is Cacheable?";
            this.IsCacheableCheckBox.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Min Amount:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Max Amount:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Interest Rate:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 109);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Interest Amount:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(356, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Service Amount:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(369, 54);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Service Rate:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(367, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Interest Level:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(369, 109);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "Interest Type:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(118, 25);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 20);
            this.textBox2.TabIndex = 8;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(118, 51);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 20);
            this.textBox3.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(118, 77);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(200, 20);
            this.textBox4.TabIndex = 10;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(118, 106);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(200, 20);
            this.textBox5.TabIndex = 11;
            // 
            // serviceAmountTextBox
            // 
            this.serviceAmountTextBox.Location = new System.Drawing.Point(474, 25);
            this.serviceAmountTextBox.Name = "serviceAmountTextBox";
            this.serviceAmountTextBox.Size = new System.Drawing.Size(196, 20);
            this.serviceAmountTextBox.TabIndex = 12;
            // 
            // serviceRateTextBox
            // 
            this.serviceRateTextBox.Location = new System.Drawing.Point(474, 51);
            this.serviceRateTextBox.Name = "serviceRateTextBox";
            this.serviceRateTextBox.Size = new System.Drawing.Size(196, 20);
            this.serviceRateTextBox.TabIndex = 13;
            // 
            // interestLevelTextBox
            // 
            this.interestLevelTextBox.Location = new System.Drawing.Point(474, 77);
            this.interestLevelTextBox.Name = "interestLevelTextBox";
            this.interestLevelTextBox.Size = new System.Drawing.Size(196, 20);
            this.interestLevelTextBox.TabIndex = 14;
            // 
            // interestTypeTextBox
            // 
            this.interestTypeTextBox.Location = new System.Drawing.Point(474, 106);
            this.interestTypeTextBox.Name = "interestTypeTextBox";
            this.interestTypeTextBox.Size = new System.Drawing.Size(196, 20);
            this.interestTypeTextBox.TabIndex = 15;
            // 
            // feeValueTextBox
            // 
            this.feeValueTextBox.Location = new System.Drawing.Point(117, 75);
            this.feeValueTextBox.Name = "feeValueTextBox";
            this.feeValueTextBox.Size = new System.Drawing.Size(201, 20);
            this.feeValueTextBox.TabIndex = 13;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(53, 78);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "Fee Value:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(60, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 13);
            this.label20.TabIndex = 14;
            this.label20.Text = "Fee Type:";
            // 
            // feeTypeComboBox
            // 
            this.feeTypeComboBox.FormattingEnabled = true;
            this.feeTypeComboBox.Location = new System.Drawing.Point(117, 48);
            this.feeTypeComboBox.Name = "feeTypeComboBox";
            this.feeTypeComboBox.Size = new System.Drawing.Size(200, 21);
            this.feeTypeComboBox.TabIndex = 15;
            // 
            // feeLookupTypeComboBox
            // 
            this.feeLookupTypeComboBox.FormattingEnabled = true;
            this.feeLookupTypeComboBox.Location = new System.Drawing.Point(118, 102);
            this.feeLookupTypeComboBox.Name = "feeLookupTypeComboBox";
            this.feeLookupTypeComboBox.Size = new System.Drawing.Size(200, 21);
            this.feeLookupTypeComboBox.TabIndex = 17;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(17, 105);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 13);
            this.label21.TabIndex = 16;
            this.label21.Text = "Fee Lookup Type:";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(546, 603);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 39);
            this.submitButton.TabIndex = 14;
            this.submitButton.Text = "Submit\r\nComponent";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(627, 603);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 39);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addComponent
            // 
            this.addComponent.Location = new System.Drawing.Point(6, 19);
            this.addComponent.Name = "addComponent";
            this.addComponent.Size = new System.Drawing.Size(75, 39);
            this.addComponent.TabIndex = 2;
            this.addComponent.Text = "Add \r\nComponent";
            this.addComponent.UseVisualStyleBackColor = true;
            this.addComponent.Click += new System.EventHandler(this.addComponent_Click);
            // 
            // editComponentButton
            // 
            this.editComponentButton.Location = new System.Drawing.Point(87, 19);
            this.editComponentButton.Name = "editComponentButton";
            this.editComponentButton.Size = new System.Drawing.Size(75, 39);
            this.editComponentButton.TabIndex = 3;
            this.editComponentButton.Text = "Edit \r\nComponent";
            this.editComponentButton.UseVisualStyleBackColor = true;
            this.editComponentButton.Click += new System.EventHandler(this.editComponentButton_Click);
            // 
            // addRuleButton
            // 
            this.addRuleButton.Location = new System.Drawing.Point(10, 18);
            this.addRuleButton.Name = "addRuleButton";
            this.addRuleButton.Size = new System.Drawing.Size(50, 40);
            this.addRuleButton.TabIndex = 4;
            this.addRuleButton.Text = "Add \r\nRule";
            this.addRuleButton.UseVisualStyleBackColor = true;
            this.addRuleButton.Click += new System.EventHandler(this.addRuleButton_Click);
            // 
            // removeComponentButton
            // 
            this.removeComponentButton.Location = new System.Drawing.Point(168, 19);
            this.removeComponentButton.Name = "removeComponentButton";
            this.removeComponentButton.Size = new System.Drawing.Size(75, 39);
            this.removeComponentButton.TabIndex = 5;
            this.removeComponentButton.Text = "Remove Component";
            this.removeComponentButton.UseVisualStyleBackColor = true;
            this.removeComponentButton.Click += new System.EventHandler(this.removeComponentButton_Click);
            // 
            // editRuleButton
            // 
            this.editRuleButton.Location = new System.Drawing.Point(66, 18);
            this.editRuleButton.Name = "editRuleButton";
            this.editRuleButton.Size = new System.Drawing.Size(50, 40);
            this.editRuleButton.TabIndex = 6;
            this.editRuleButton.Text = "Edit \r\nRule";
            this.editRuleButton.UseVisualStyleBackColor = true;
            this.editRuleButton.Click += new System.EventHandler(this.editRuleButton_Click);
            // 
            // deleteRuleButton
            // 
            this.deleteRuleButton.Location = new System.Drawing.Point(122, 18);
            this.deleteRuleButton.Name = "deleteRuleButton";
            this.deleteRuleButton.Size = new System.Drawing.Size(50, 40);
            this.deleteRuleButton.TabIndex = 7;
            this.deleteRuleButton.Text = "Delete\r\nRule";
            this.deleteRuleButton.UseVisualStyleBackColor = true;
            this.deleteRuleButton.Click += new System.EventHandler(this.deleteRuleButton_Click);
            // 
            // componentModifyGroupBox
            // 
            this.componentModifyGroupBox.Controls.Add(this.addComponent);
            this.componentModifyGroupBox.Controls.Add(this.editComponentButton);
            this.componentModifyGroupBox.Controls.Add(this.removeComponentButton);
            this.componentModifyGroupBox.Enabled = false;
            this.componentModifyGroupBox.Location = new System.Drawing.Point(200, 592);
            this.componentModifyGroupBox.Name = "componentModifyGroupBox";
            this.componentModifyGroupBox.Size = new System.Drawing.Size(249, 68);
            this.componentModifyGroupBox.TabIndex = 8;
            this.componentModifyGroupBox.TabStop = false;
            // 
            // ruleModifyGroupBox
            // 
            this.ruleModifyGroupBox.Controls.Add(this.deleteRuleButton);
            this.ruleModifyGroupBox.Controls.Add(this.editRuleButton);
            this.ruleModifyGroupBox.Controls.Add(this.addRuleButton);
            this.ruleModifyGroupBox.Enabled = false;
            this.ruleModifyGroupBox.Location = new System.Drawing.Point(16, 592);
            this.ruleModifyGroupBox.Name = "ruleModifyGroupBox";
            this.ruleModifyGroupBox.Size = new System.Drawing.Size(178, 68);
            this.ruleModifyGroupBox.TabIndex = 9;
            this.ruleModifyGroupBox.TabStop = false;
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validateEntireTreeToolStripMenuItem,
            this.validateAliasesToolStripMenuItem,
            this.validateComponentsToolStripMenuItem});
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.validateToolStripMenuItem.Text = "&Validate";
            // 
            // validateEntireTreeToolStripMenuItem
            // 
            this.validateEntireTreeToolStripMenuItem.Name = "validateEntireTreeToolStripMenuItem";
            this.validateEntireTreeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.validateEntireTreeToolStripMenuItem.Text = "Validate Entire Tree...";
            // 
            // validateAliasesToolStripMenuItem
            // 
            this.validateAliasesToolStripMenuItem.Name = "validateAliasesToolStripMenuItem";
            this.validateAliasesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.validateAliasesToolStripMenuItem.Text = "Validate Aliases...";
            // 
            // validateComponentsToolStripMenuItem
            // 
            this.validateComponentsToolStripMenuItem.Name = "validateComponentsToolStripMenuItem";
            this.validateComponentsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.validateComponentsToolStripMenuItem.Text = "Validate Components...";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(361, 530);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 13);
            this.label22.TabIndex = 16;
            this.label22.Text = "Node Parent Id:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(362, 557);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 13);
            this.label23.TabIndex = 17;
            this.label23.Text = "Node Id: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(449, 527);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(253, 20);
            this.textBox1.TabIndex = 18;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(449, 554);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(253, 20);
            this.textBox6.TabIndex = 19;
            // 
            // PawnRulesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 721);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.rulesEditorMenuStrip);
            this.MainMenuStrip = this.rulesEditorMenuStrip;
            this.Name = "PawnRulesEditor";
            this.Text = "PawnRulesEditor";
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.rulesEditorMenuStrip.ResumeLayout(false);
            this.rulesEditorMenuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.commonRulesElementsGroupBox.ResumeLayout(false);
            this.commonRulesElementsGroupBox.PerformLayout();
            this.InterestRuleComponentGroupBox.ResumeLayout(false);
            this.InterestRuleComponentGroupBox.PerformLayout();
            this.ParameterRuleComponentData.ResumeLayout(false);
            this.ParameterRuleComponentData.PerformLayout();
            this.FeeRuleComponentData.ResumeLayout(false);
            this.FeeRuleComponentData.PerformLayout();
            this.componentModifyGroupBox.ResumeLayout(false);
            this.ruleModifyGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel mainRulesEditorStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar mainRulesEngineProgressBar;
        private System.Windows.Forms.MenuStrip rulesEditorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRulesFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRulesFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView rulesDataTreeView;
        private System.Windows.Forms.GroupBox commonRulesElementsGroupBox;
        private System.Windows.Forms.Label AliasLabel;
        private System.Windows.Forms.ComboBox aliasComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox ruleComponentNameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox FeeRuleComponentData;
        private System.Windows.Forms.GroupBox ParameterRuleComponentData;
        private System.Windows.Forms.GroupBox InterestRuleComponentGroupBox;
        private System.Windows.Forms.ComboBox ruleComponentTypeBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox dataTypeComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox isEditableCheckBox;
        private System.Windows.Forms.CheckBox IsCacheableCheckBox;
        private System.Windows.Forms.ComboBox companyComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox paramStateTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox storeNumberComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox interestTypeTextBox;
        private System.Windows.Forms.TextBox interestLevelTextBox;
        private System.Windows.Forms.TextBox serviceRateTextBox;
        private System.Windows.Forms.TextBox serviceAmountTextBox;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox feeLookupTypeComboBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox feeTypeComboBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox feeValueTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button addRuleButton;
        private System.Windows.Forms.Button editComponentButton;
        private System.Windows.Forms.Button addComponent;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button removeComponentButton;
        private System.Windows.Forms.GroupBox ruleModifyGroupBox;
        private System.Windows.Forms.Button deleteRuleButton;
        private System.Windows.Forms.Button editRuleButton;
        private System.Windows.Forms.GroupBox componentModifyGroupBox;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateEntireTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateAliasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateComponentsToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
    }
}

