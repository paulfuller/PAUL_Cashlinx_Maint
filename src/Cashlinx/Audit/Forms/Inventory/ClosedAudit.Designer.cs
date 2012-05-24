using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class ClosedAudit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClosedAudit));
            this.panel1 = new System.Windows.Forms.Panel();
            this.documentsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.lblUploadFromTrakker = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAuditStartDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAuditScope = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAuditor = new System.Windows.Forms.Label();
            this.lblAuditCompleteDate = new System.Windows.Forms.Label();
            this.lblHeaderText = new System.Windows.Forms.Label();
            this.btnClose = new CustomButton();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.documentsPanel);
            this.panel1.Location = new System.Drawing.Point(7, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 195);
            this.panel1.TabIndex = 6;
            // 
            // documentsPanel
            // 
            this.documentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.documentsPanel.AutoScroll = true;
            this.documentsPanel.AutoSize = true;
            this.documentsPanel.ColumnCount = 3;
            this.documentsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.documentsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.documentsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.documentsPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.documentsPanel.Location = new System.Drawing.Point(3, 3);
            this.documentsPanel.Name = "documentsPanel";
            this.documentsPanel.RowCount = 1;
            this.documentsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.documentsPanel.Size = new System.Drawing.Size(663, 0);
            this.documentsPanel.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUploadFromTrakker, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAuditStartDate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAuditScope, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAuditor, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAuditCompleteDate, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(674, 107);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(38, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Upload from Trakker:";
            // 
            // lblUploadFromTrakker
            // 
            this.lblUploadFromTrakker.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUploadFromTrakker.AutoSize = true;
            this.lblUploadFromTrakker.Location = new System.Drawing.Point(171, 82);
            this.lblUploadFromTrakker.Name = "lblUploadFromTrakker";
            this.lblUploadFromTrakker.Size = new System.Drawing.Size(41, 13);
            this.lblUploadFromTrakker.TabIndex = 10;
            this.lblUploadFromTrakker.Text = "label12";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(374, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Audit Complete Date:";
            // 
            // lblAuditStartDate
            // 
            this.lblAuditStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditStartDate.AutoSize = true;
            this.lblAuditStartDate.Location = new System.Drawing.Point(171, 46);
            this.lblAuditStartDate.Name = "lblAuditStartDate";
            this.lblAuditStartDate.Size = new System.Drawing.Size(35, 13);
            this.lblAuditStartDate.TabIndex = 4;
            this.lblAuditStartDate.Text = "label6";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(63, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Audit Start Date:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(449, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Auditor:";
            // 
            // lblAuditScope
            // 
            this.lblAuditScope.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditScope.AutoSize = true;
            this.lblAuditScope.Location = new System.Drawing.Point(171, 11);
            this.lblAuditScope.Name = "lblAuditScope";
            this.lblAuditScope.Size = new System.Drawing.Size(35, 13);
            this.lblAuditScope.TabIndex = 1;
            this.lblAuditScope.Text = "label3";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Audit Scope:";
            // 
            // lblAuditor
            // 
            this.lblAuditor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditor.AutoSize = true;
            this.lblAuditor.Location = new System.Drawing.Point(507, 11);
            this.lblAuditor.Name = "lblAuditor";
            this.lblAuditor.Size = new System.Drawing.Size(41, 13);
            this.lblAuditor.TabIndex = 8;
            this.lblAuditor.Text = "label10";
            // 
            // lblAuditCompleteDate
            // 
            this.lblAuditCompleteDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditCompleteDate.AutoSize = true;
            this.lblAuditCompleteDate.Location = new System.Drawing.Point(507, 46);
            this.lblAuditCompleteDate.Name = "lblAuditCompleteDate";
            this.lblAuditCompleteDate.Size = new System.Drawing.Size(35, 13);
            this.lblAuditCompleteDate.TabIndex = 7;
            this.lblAuditCompleteDate.Text = "label9";
            // 
            // lblHeaderText
            // 
            this.lblHeaderText.AutoSize = true;
            this.lblHeaderText.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderText.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderText.ForeColor = System.Drawing.Color.White;
            this.lblHeaderText.Location = new System.Drawing.Point(254, 36);
            this.lblHeaderText.Name = "lblHeaderText";
            this.lblHeaderText.Size = new System.Drawing.Size(190, 18);
            this.lblHeaderText.TabIndex = 2;
            this.lblHeaderText.Text = "<Shop> Inventory Audit";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(589, 541);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnClose.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 50);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ClosedAudit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audit.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(698, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblHeaderText);
            this.Controls.Add(this.btnClose);
            this.Name = "ClosedAudit";
            this.Text = "AuditResults";
            this.Load += new System.EventHandler(this.AuditResults_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton btnClose;
        private System.Windows.Forms.Label lblHeaderText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAuditScope;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAuditStartDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAuditCompleteDate;
        private System.Windows.Forms.Label lblAuditor;
        private System.Windows.Forms.Label lblUploadFromTrakker;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel documentsPanel;
        private System.Windows.Forms.Panel panel1;
    }
}