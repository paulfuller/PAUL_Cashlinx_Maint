namespace CouchConsoleApp.form
{
    partial class ArchProcessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchProcessForm));
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.totalCntLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CompletedLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PendingLbl = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timeElapLbl = new System.Windows.Forms.Label();
            this.statusLabelForProg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(135, 257);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Visible = false;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(320, 257);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Done";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(226, 257);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(134, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Total  Records:";
            // 
            // totalCntLbl
            // 
            this.totalCntLbl.AutoSize = true;
            this.totalCntLbl.BackColor = System.Drawing.Color.Transparent;
            this.totalCntLbl.Location = new System.Drawing.Point(244, 75);
            this.totalCntLbl.Name = "totalCntLbl";
            this.totalCntLbl.Size = new System.Drawing.Size(57, 13);
            this.totalCntLbl.TabIndex = 4;
            this.totalCntLbl.Text = "initializing..";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(134, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Completed:";
            // 
            // CompletedLbl
            // 
            this.CompletedLbl.AutoSize = true;
            this.CompletedLbl.BackColor = System.Drawing.Color.Transparent;
            this.CompletedLbl.Location = new System.Drawing.Point(244, 113);
            this.CompletedLbl.Name = "CompletedLbl";
            this.CompletedLbl.Size = new System.Drawing.Size(57, 13);
            this.CompletedLbl.TabIndex = 6;
            this.CompletedLbl.Text = "initializing..";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(134, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pending:";
            // 
            // PendingLbl
            // 
            this.PendingLbl.AutoSize = true;
            this.PendingLbl.BackColor = System.Drawing.Color.Transparent;
            this.PendingLbl.Location = new System.Drawing.Point(244, 157);
            this.PendingLbl.Name = "PendingLbl";
            this.PendingLbl.Size = new System.Drawing.Size(57, 13);
            this.PendingLbl.TabIndex = 8;
            this.PendingLbl.Text = "initializing..";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.statusLabelForProg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 295);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(519, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(135, 207);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(277, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // progText
            // 
            this.progText.AutoSize = true;
            this.progText.BackColor = System.Drawing.Color.Transparent;
            this.progText.Location = new System.Drawing.Point(428, 212);
            this.progText.Name = "progText";
            this.progText.Size = new System.Drawing.Size(0, 13);
            this.progText.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(321, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Time Elapsed:";
            // 
            // timeElapLbl
            // 
            this.timeElapLbl.AutoSize = true;
            this.timeElapLbl.BackColor = System.Drawing.Color.Transparent;
            this.timeElapLbl.Location = new System.Drawing.Point(401, 19);
            this.timeElapLbl.Name = "timeElapLbl";
            this.timeElapLbl.Size = new System.Drawing.Size(0, 13);
            this.timeElapLbl.TabIndex = 16;
            // 
            // statusLabelForProg
            // 
            this.statusLabelForProg.Name = "statusLabelForProg";
            this.statusLabelForProg.Size = new System.Drawing.Size(74, 17);
            this.statusLabelForProg.Text = "In Progress...";
            // 
            // ArchProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 317);
            this.Controls.Add(this.timeElapLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progText);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.PendingLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CompletedLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.totalCntLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArchProcessForm";
            this.Text = "Archival Process";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ArchProcessForm_FormClosed);
            this.Load += new System.EventHandler(this.ArchProcessForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalCntLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CompletedLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PendingLbl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label progText;
        public System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label timeElapLbl;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelForProg;
    }
}

