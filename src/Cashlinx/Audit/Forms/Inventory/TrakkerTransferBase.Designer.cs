using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class TrakkerTransferBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrakkerTransferBase));
            this.btnCancel = new CustomButton();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.lblStatusValue2 = new System.Windows.Forms.Label();
            this.btnContinue = new CustomButton();
            this.lblStatusMessage2 = new System.Windows.Forms.Label();
            this.lblStatusMessage1 = new System.Windows.Forms.Label();
            this.btnAdditionalTracker = new CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatusValue1 = new System.Windows.Forms.Label();
            this.lnkInstructions = new System.Windows.Forms.LinkLabel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(6, 296);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusMessage.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatusMessage.Location = new System.Drawing.Point(3, 60);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(577, 23);
            this.lblStatusMessage.TabIndex = 8;
            this.lblStatusMessage.Text = "Click Continue when Trakker ready to receive";
            this.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusValue2
            // 
            this.lblStatusValue2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusValue2.AutoSize = true;
            this.lblStatusValue2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatusValue2.Location = new System.Drawing.Point(274, 38);
            this.lblStatusValue2.Name = "lblStatusValue2";
            this.lblStatusValue2.Size = new System.Drawing.Size(31, 13);
            this.lblStatusValue2.TabIndex = 3;
            this.lblStatusValue2.Text = "2500";
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.BackColor = System.Drawing.Color.Transparent;
            this.btnContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnContinue.BackgroundImage")));
            this.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnContinue.Location = new System.Drawing.Point(480, 296);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(0);
            this.btnContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(100, 50);
            this.btnContinue.TabIndex = 14;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            // 
            // lblStatusMessage2
            // 
            this.lblStatusMessage2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusMessage2.AutoSize = true;
            this.lblStatusMessage2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatusMessage2.Location = new System.Drawing.Point(3, 38);
            this.lblStatusMessage2.Name = "lblStatusMessage2";
            this.lblStatusMessage2.Size = new System.Drawing.Size(175, 13);
            this.lblStatusMessage2.TabIndex = 2;
            this.lblStatusMessage2.Text = "Records Downloaded to Trakker 50";
            // 
            // lblStatusMessage1
            // 
            this.lblStatusMessage1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusMessage1.AutoSize = true;
            this.lblStatusMessage1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatusMessage1.Location = new System.Drawing.Point(3, 8);
            this.lblStatusMessage1.Name = "lblStatusMessage1";
            this.lblStatusMessage1.Size = new System.Drawing.Size(136, 13);
            this.lblStatusMessage1.TabIndex = 0;
            this.lblStatusMessage1.Text = "Records to be Downloaded";
            // 
            // btnAdditionalTracker
            // 
            this.btnAdditionalTracker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdditionalTracker.BackColor = System.Drawing.Color.Transparent;
            this.btnAdditionalTracker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdditionalTracker.BackgroundImage")));
            this.btnAdditionalTracker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdditionalTracker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdditionalTracker.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAdditionalTracker.FlatAppearance.BorderSize = 0;
            this.btnAdditionalTracker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdditionalTracker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdditionalTracker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdditionalTracker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdditionalTracker.ForeColor = System.Drawing.Color.White;
            this.btnAdditionalTracker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdditionalTracker.Location = new System.Drawing.Point(380, 296);
            this.btnAdditionalTracker.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdditionalTracker.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnAdditionalTracker.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnAdditionalTracker.Name = "btnAdditionalTracker";
            this.btnAdditionalTracker.Size = new System.Drawing.Size(100, 50);
            this.btnAdditionalTracker.TabIndex = 15;
            this.btnAdditionalTracker.Text = "Addt\'l Trakker";
            this.btnAdditionalTracker.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.77054F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.22946F));
            this.tableLayoutPanel1.Controls.Add(this.lblStatusValue2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStatusMessage2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStatusValue1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStatusMessage1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(115, 135);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(353, 60);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // lblStatusValue1
            // 
            this.lblStatusValue1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusValue1.AutoSize = true;
            this.lblStatusValue1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatusValue1.Location = new System.Drawing.Point(274, 8);
            this.lblStatusValue1.Name = "lblStatusValue1";
            this.lblStatusValue1.Size = new System.Drawing.Size(31, 13);
            this.lblStatusValue1.TabIndex = 1;
            this.lblStatusValue1.Text = "2500";
            // 
            // lnkInstructions
            // 
            this.lnkInstructions.ActiveLinkColor = System.Drawing.Color.Red;
            this.lnkInstructions.AutoSize = true;
            this.lnkInstructions.BackColor = System.Drawing.Color.Transparent;
            this.lnkInstructions.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.lnkInstructions.ForeColor = System.Drawing.Color.Red;
            this.lnkInstructions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lnkInstructions.LinkColor = System.Drawing.Color.Red;
            this.lnkInstructions.Location = new System.Drawing.Point(12, 107);
            this.lnkInstructions.Name = "lnkInstructions";
            this.lnkInstructions.Size = new System.Drawing.Size(74, 16);
            this.lnkInstructions.TabIndex = 9;
            this.lnkInstructions.TabStop = true;
            this.lnkInstructions.Text = "Instructions";
            this.lnkInstructions.VisitedLinkColor = System.Drawing.Color.Red;
            this.lnkInstructions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkInstructions_LinkClicked);
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(12, 24);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(561, 23);
            this.lblHeader.TabIndex = 16;
            this.lblHeader.Text = "label1";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrakkerTransferBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audit.Properties.Resources.newDialog_480_BlueScale;
            this.ClientSize = new System.Drawing.Size(585, 360);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblStatusMessage);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnAdditionalTracker);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lnkInstructions);
            this.Name = "TrakkerTransferBase";
            this.Text = "DownloadToTrakker";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatusMessage;
        private CustomButton btnCancel;
        private System.Windows.Forms.Label lblStatusValue2;
        private System.Windows.Forms.Label lblStatusMessage2;
        private System.Windows.Forms.Label lblStatusMessage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblStatusValue1;
        private System.Windows.Forms.LinkLabel lnkInstructions;
        private System.Windows.Forms.Label lblHeader;
        public CustomButton btnContinue;
        public CustomButton btnAdditionalTracker;


    }
}