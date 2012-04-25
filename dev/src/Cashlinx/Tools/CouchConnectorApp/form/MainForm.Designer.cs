namespace CouchConsoleApp.form
{
    public partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.clearConsoleButton = new System.Windows.Forms.Button();
            this.resultText = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serverNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dbNameLabel = new System.Windows.Forms.Label();
            this.archiveTab = new System.Windows.Forms.TabPage();
            this.stopButton = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.targetOrclDBName = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.targetDBPIC = new System.Windows.Forms.PictureBox();
            this.dbConnectButton = new System.Windows.Forms.Button();
            this.targetDBServerLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.targetCouchDBName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.couchConnButton = new System.Windows.Forms.Button();
            this.targetCouchPic = new System.Windows.Forms.PictureBox();
            this.targetCouchLBL = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.srcCouchDBName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.srcCouchLBL = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.getDocument = new System.Windows.Forms.Button();
            this.docIDText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.fileNameText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.testTab1 = new System.Windows.Forms.TabPage();
            this.reverArchButton = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.testTab2 = new System.Windows.Forms.TabPage();
            this.countProc = new System.Windows.Forms.Button();
            this.docid123 = new System.Windows.Forms.TextBox();
            this.dbNameText = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.AddCouchDB = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bgWorkerForJobAllocation = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerForDocPop = new System.ComponentModel.BackgroundWorker();
            this.archiveTab.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetDBPIC)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetCouchPic)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.testTab1.SuspendLayout();
            this.testTab2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(294, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Couch Connector 2.1";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(457, 12);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(93, 64);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // clearConsoleButton
            // 
            this.clearConsoleButton.Location = new System.Drawing.Point(317, 12);
            this.clearConsoleButton.Name = "clearConsoleButton";
            this.clearConsoleButton.Size = new System.Drawing.Size(93, 64);
            this.clearConsoleButton.TabIndex = 1;
            this.clearConsoleButton.Text = "Clear Console";
            this.clearConsoleButton.UseVisualStyleBackColor = true;
            this.clearConsoleButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // resultText
            // 
            this.resultText.Location = new System.Drawing.Point(12, 12);
            this.resultText.Name = "resultText";
            this.resultText.Size = new System.Drawing.Size(825, 235);
            this.resultText.TabIndex = 0;
            this.resultText.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Name:";
            // 
            // serverNameLabel
            // 
            this.serverNameLabel.AutoSize = true;
            this.serverNameLabel.Location = new System.Drawing.Point(135, 23);
            this.serverNameLabel.Name = "serverNameLabel";
            this.serverNameLabel.Size = new System.Drawing.Size(0, 13);
            this.serverNameLabel.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "DB Name:";
            // 
            // dbNameLabel
            // 
            this.dbNameLabel.AutoSize = true;
            this.dbNameLabel.Location = new System.Drawing.Point(124, 54);
            this.dbNameLabel.Name = "dbNameLabel";
            this.dbNameLabel.Size = new System.Drawing.Size(0, 13);
            this.dbNameLabel.TabIndex = 6;
            // 
            // archiveTab
            // 
            this.archiveTab.Controls.Add(this.stopButton);
            this.archiveTab.Controls.Add(this.goButton);
            this.archiveTab.Controls.Add(this.groupBox4);
            this.archiveTab.Controls.Add(this.groupBox3);
            this.archiveTab.Controls.Add(this.groupBox2);
            this.archiveTab.Controls.Add(this.groupBox1);
            this.archiveTab.Location = new System.Drawing.Point(4, 22);
            this.archiveTab.Name = "archiveTab";
            this.archiveTab.Padding = new System.Windows.Forms.Padding(3);
            this.archiveTab.Size = new System.Drawing.Size(812, 261);
            this.archiveTab.TabIndex = 2;
            this.archiveTab.Text = "Archive Documents";
            this.archiveTab.UseVisualStyleBackColor = true;
            this.archiveTab.Click += new System.EventHandler(this.archiveTab_Click);
            // 
            // stopButton
            // 
            this.stopButton.AutoEllipsis = true;
            this.stopButton.Cursor = System.Windows.Forms.Cursors.No;
            this.stopButton.Location = new System.Drawing.Point(341, 155);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(93, 64);
            this.stopButton.TabIndex = 13;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Visible = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // goButton
            // 
            this.goButton.Enabled = false;
            this.goButton.Location = new System.Drawing.Point(341, 48);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(93, 64);
            this.goButton.TabIndex = 12;
            this.goButton.Text = "GO";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.Gobutton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(504, 151);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(232, 97);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Criteria";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(98, 55);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Closed";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 55);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Prod Status:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(98, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "90 Days";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Older than:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.targetOrclDBName);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.targetDBPIC);
            this.groupBox3.Controls.Add(this.dbConnectButton);
            this.groupBox3.Controls.Add(this.targetDBServerLbl);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(32, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 97);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cashlinx DataBase";
            // 
            // targetOrclDBName
            // 
            this.targetOrclDBName.AutoSize = true;
            this.targetOrclDBName.BackColor = System.Drawing.Color.Transparent;
            this.targetOrclDBName.Location = new System.Drawing.Point(93, 39);
            this.targetOrclDBName.Name = "targetOrclDBName";
            this.targetOrclDBName.Size = new System.Drawing.Size(69, 13);
            this.targetOrclDBName.TabIndex = 11;
            this.targetOrclDBName.Text = "Not Selected";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "DB Name:";
            // 
            // targetDBPIC
            // 
            this.targetDBPIC.Image = global::CouchConsoleApp.Properties.Resources.cross;
            this.targetDBPIC.InitialImage = global::CouchConsoleApp.Properties.Resources.cross;
            this.targetDBPIC.Location = new System.Drawing.Point(203, 39);
            this.targetDBPIC.Name = "targetDBPIC";
            this.targetDBPIC.Size = new System.Drawing.Size(23, 22);
            this.targetDBPIC.TabIndex = 8;
            this.targetDBPIC.TabStop = false;
            // 
            // dbConnectButton
            // 
            this.dbConnectButton.Location = new System.Drawing.Point(96, 59);
            this.dbConnectButton.Name = "dbConnectButton";
            this.dbConnectButton.Size = new System.Drawing.Size(58, 28);
            this.dbConnectButton.TabIndex = 6;
            this.dbConnectButton.Text = "Connect";
            this.dbConnectButton.UseVisualStyleBackColor = true;
            this.dbConnectButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // targetDBServerLbl
            // 
            this.targetDBServerLbl.AutoSize = true;
            this.targetDBServerLbl.BackColor = System.Drawing.Color.Transparent;
            this.targetDBServerLbl.Location = new System.Drawing.Point(93, 16);
            this.targetDBServerLbl.Name = "targetDBServerLbl";
            this.targetDBServerLbl.Size = new System.Drawing.Size(69, 13);
            this.targetDBServerLbl.TabIndex = 5;
            this.targetDBServerLbl.Text = "Not Selected";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Server Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.targetCouchDBName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.couchConnButton);
            this.groupBox2.Controls.Add(this.targetCouchPic);
            this.groupBox2.Controls.Add(this.targetCouchLBL);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(503, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target Couch";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // targetCouchDBName
            // 
            this.targetCouchDBName.AutoSize = true;
            this.targetCouchDBName.BackColor = System.Drawing.Color.Transparent;
            this.targetCouchDBName.Location = new System.Drawing.Point(99, 39);
            this.targetCouchDBName.Name = "targetCouchDBName";
            this.targetCouchDBName.Size = new System.Drawing.Size(69, 13);
            this.targetCouchDBName.TabIndex = 10;
            this.targetCouchDBName.Text = "Not Selected";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "DB Name:";
            // 
            // couchConnButton
            // 
            this.couchConnButton.Location = new System.Drawing.Point(102, 64);
            this.couchConnButton.Name = "couchConnButton";
            this.couchConnButton.Size = new System.Drawing.Size(58, 28);
            this.couchConnButton.TabIndex = 8;
            this.couchConnButton.Text = "Connect";
            this.couchConnButton.UseVisualStyleBackColor = true;
            this.couchConnButton.Click += new System.EventHandler(this.button6_Click);
            // 
            // targetCouchPic
            // 
            this.targetCouchPic.Image = global::CouchConsoleApp.Properties.Resources.cross;
            this.targetCouchPic.InitialImage = global::CouchConsoleApp.Properties.Resources.cross;
            this.targetCouchPic.Location = new System.Drawing.Point(203, 39);
            this.targetCouchPic.Name = "targetCouchPic";
            this.targetCouchPic.Size = new System.Drawing.Size(23, 22);
            this.targetCouchPic.TabIndex = 7;
            this.targetCouchPic.TabStop = false;
            // 
            // targetCouchLBL
            // 
            this.targetCouchLBL.AutoSize = true;
            this.targetCouchLBL.BackColor = System.Drawing.Color.Transparent;
            this.targetCouchLBL.Location = new System.Drawing.Point(99, 16);
            this.targetCouchLBL.Name = "targetCouchLBL";
            this.targetCouchLBL.Size = new System.Drawing.Size(69, 13);
            this.targetCouchLBL.TabIndex = 6;
            this.targetCouchLBL.Text = "Not Selected";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Server Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.srcCouchDBName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.srcCouchLBL);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(32, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Couch";
            // 
            // srcCouchDBName
            // 
            this.srcCouchDBName.AutoSize = true;
            this.srcCouchDBName.BackColor = System.Drawing.Color.Transparent;
            this.srcCouchDBName.Location = new System.Drawing.Point(93, 39);
            this.srcCouchDBName.Name = "srcCouchDBName";
            this.srcCouchDBName.Size = new System.Drawing.Size(69, 13);
            this.srcCouchDBName.TabIndex = 11;
            this.srcCouchDBName.Text = "Not Selected";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "DB Name:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CouchConsoleApp.Properties.Resources.tick;
            this.pictureBox1.InitialImage = global::CouchConsoleApp.Properties.Resources.tick;
            this.pictureBox1.Location = new System.Drawing.Point(203, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 22);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // srcCouchLBL
            // 
            this.srcCouchLBL.AutoSize = true;
            this.srcCouchLBL.BackColor = System.Drawing.Color.Transparent;
            this.srcCouchLBL.Location = new System.Drawing.Point(93, 16);
            this.srcCouchLBL.Name = "srcCouchLBL";
            this.srcCouchLBL.Size = new System.Drawing.Size(69, 13);
            this.srcCouchLBL.TabIndex = 5;
            this.srcCouchLBL.Text = "Not Selected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Server Name:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.getDocument);
            this.tabPage2.Controls.Add(this.docIDText);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(812, 261);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Get Document";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // getDocument
            // 
            this.getDocument.Location = new System.Drawing.Point(576, 108);
            this.getDocument.Name = "getDocument";
            this.getDocument.Size = new System.Drawing.Size(93, 64);
            this.getDocument.TabIndex = 11;
            this.getDocument.Text = "GET";
            this.getDocument.UseVisualStyleBackColor = true;
            this.getDocument.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // docIDText
            // 
            this.docIDText.Location = new System.Drawing.Point(245, 127);
            this.docIDText.Name = "docIDText";
            this.docIDText.Size = new System.Drawing.Size(272, 20);
            this.docIDText.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(103, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Document ID";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.archiveTab);
            this.tabControl.Controls.Add(this.testTab1);
            this.tabControl.Controls.Add(this.testTab2);
            this.tabControl.Location = new System.Drawing.Point(17, 87);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(820, 287);
            this.tabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.fileNameText);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(812, 261);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add Document";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(643, 82);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 65);
            this.button4.TabIndex = 3;
            this.button4.Text = "Add";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // fileNameText
            // 
            this.fileNameText.Location = new System.Drawing.Point(16, 105);
            this.fileNameText.Name = "fileNameText";
            this.fileNameText.Size = new System.Drawing.Size(430, 20);
            this.fileNameText.TabIndex = 2;
            this.fileNameText.TextChanged += new System.EventHandler(this.fileNameText_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(54, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(517, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 65);
            this.button2.TabIndex = 0;
            this.button2.Text = "Browse...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // testTab1
            // 
            this.testTab1.Controls.Add(this.reverArchButton);
            this.testTab1.Controls.Add(this.button15);
            this.testTab1.Controls.Add(this.button14);
            this.testTab1.Controls.Add(this.button13);
            this.testTab1.Controls.Add(this.button12);
            this.testTab1.Controls.Add(this.button11);
            this.testTab1.Controls.Add(this.button10);
            this.testTab1.Controls.Add(this.button9);
            this.testTab1.Controls.Add(this.button8);
            this.testTab1.Controls.Add(this.button7);
            this.testTab1.Location = new System.Drawing.Point(4, 22);
            this.testTab1.Name = "testTab1";
            this.testTab1.Padding = new System.Windows.Forms.Padding(3);
            this.testTab1.Size = new System.Drawing.Size(812, 261);
            this.testTab1.TabIndex = 3;
            this.testTab1.Text = "Test Tab";
            this.testTab1.UseVisualStyleBackColor = true;
            // 
            // reverArchButton
            // 
            this.reverArchButton.Location = new System.Drawing.Point(43, 160);
            this.reverArchButton.Name = "reverArchButton";
            this.reverArchButton.Size = new System.Drawing.Size(185, 23);
            this.reverArchButton.TabIndex = 28;
            this.reverArchButton.Text = "Reverse Arch";
            this.reverArchButton.UseVisualStyleBackColor = true;
            this.reverArchButton.Click += new System.EventHandler(this.reverseArchAction);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(567, 50);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(142, 23);
            this.button15.TabIndex = 27;
            this.button15.Text = "Get DOCProc";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(518, 114);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(178, 23);
            this.button14.TabIndex = 26;
            this.button14.Text = "Get DOC And Add";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(311, 114);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(155, 23);
            this.button13.TabIndex = 25;
            this.button13.Text = "AddAlterDocs";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.addDocumentClick);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(43, 114);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(194, 23);
            this.button12.TabIndex = 24;
            this.button12.Text = "Update PawnRegTest";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(438, 50);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 23;
            this.button11.Text = "Get DOC";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(43, 51);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 22;
            this.button10.Text = "SingleInsert";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(340, 51);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 21;
            this.button9.Text = "Date Parse";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(238, 51);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 20;
            this.button8.Text = "Delete DB";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(138, 51);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 19;
            this.button7.Text = "Create DB";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // testTab2
            // 
            this.testTab2.Controls.Add(this.countProc);
            this.testTab2.Controls.Add(this.docid123);
            this.testTab2.Controls.Add(this.dbNameText);
            this.testTab2.Controls.Add(this.button6);
            this.testTab2.Controls.Add(this.AddCouchDB);
            this.testTab2.Controls.Add(this.button3);
            this.testTab2.Controls.Add(this.button1);
            this.testTab2.Location = new System.Drawing.Point(4, 22);
            this.testTab2.Name = "testTab2";
            this.testTab2.Padding = new System.Windows.Forms.Padding(3);
            this.testTab2.Size = new System.Drawing.Size(812, 261);
            this.testTab2.TabIndex = 4;
            this.testTab2.Text = "Revert Arch";
            this.testTab2.UseVisualStyleBackColor = true;
            // 
            // countProc
            // 
            this.countProc.Location = new System.Drawing.Point(609, 20);
            this.countProc.Name = "countProc";
            this.countProc.Size = new System.Drawing.Size(129, 78);
            this.countProc.TabIndex = 35;
            this.countProc.Text = "Count Proc";
            this.countProc.UseVisualStyleBackColor = true;
            this.countProc.Click += new System.EventHandler(this.countProc_Click);
            // 
            // docid123
            // 
            this.docid123.Location = new System.Drawing.Point(16, 50);
            this.docid123.Name = "docid123";
            this.docid123.Size = new System.Drawing.Size(193, 20);
            this.docid123.TabIndex = 34;
            // 
            // dbNameText
            // 
            this.dbNameText.Location = new System.Drawing.Point(16, 146);
            this.dbNameText.Name = "dbNameText";
            this.dbNameText.Size = new System.Drawing.Size(182, 20);
            this.dbNameText.TabIndex = 33;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(436, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(129, 78);
            this.button6.TabIndex = 32;
            this.button6.Text = "Delete Docs";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // AddCouchDB
            // 
            this.AddCouchDB.Location = new System.Drawing.Point(260, 116);
            this.AddCouchDB.Name = "AddCouchDB";
            this.AddCouchDB.Size = new System.Drawing.Size(129, 78);
            this.AddCouchDB.TabIndex = 31;
            this.AddCouchDB.Text = "AddCouchDB";
            this.AddCouchDB.UseVisualStyleBackColor = true;
            this.AddCouchDB.Click += new System.EventHandler(this.createDBClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(260, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 78);
            this.button3.TabIndex = 30;
            this.button3.Text = "Add  Docs";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.addTempDocsAction);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(436, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 78);
            this.button1.TabIndex = 29;
            this.button1.Text = "DeleteCouchDB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 760);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(849, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusLabel2.Text = "In Progress...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resultText);
            this.splitContainer1.Size = new System.Drawing.Size(849, 760);
            this.splitContainer1.SplitterDistance = 378;
            this.splitContainer1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.clearConsoleButton);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 669);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 91);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dbNameLabel);
            this.panel2.Controls.Add(this.serverNameLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 81);
            this.panel2.TabIndex = 9;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(849, 782);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(865, 820);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Couch Connector 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppExitClick);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.archiveTab.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetDBPIC)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetCouchPic)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.testTab1.ResumeLayout(false);
            this.testTab2.ResumeLayout(false);
            this.testTab2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearConsoleButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label serverNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label dbNameLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TabPage archiveTab;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox targetDBPIC;
        private System.Windows.Forms.Button dbConnectButton;
        private System.Windows.Forms.Label targetDBServerLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button couchConnButton;
        private System.Windows.Forms.PictureBox targetCouchPic;
        private System.Windows.Forms.Label targetCouchLBL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label srcCouchLBL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button getDocument;
        private System.Windows.Forms.TextBox docIDText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox fileNameText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label targetCouchDBName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label targetOrclDBName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label srcCouchDBName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RichTextBox resultText;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker bgWorkerForJobAllocation;
        private System.Windows.Forms.TabPage testTab2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.ComponentModel.BackgroundWorker bgWorkerForDocPop;
        private System.Windows.Forms.Button AddCouchDB;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox dbNameText;
        private System.Windows.Forms.TextBox docid123;
        private System.Windows.Forms.Button countProc;
        private System.Windows.Forms.TabPage testTab1;
        private System.Windows.Forms.Button reverArchButton;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
    }
}

