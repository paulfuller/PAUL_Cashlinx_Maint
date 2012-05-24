using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Logic.PrintQueue
{
    partial class Print_PfiRefurbList
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
            this.storeValueLabel = new System.Windows.Forms.Label();
            this.storeLabel = new System.Windows.Forms.Label();
            this.runValueLabel = new System.Windows.Forms.Label();
            this.runLabel = new System.Windows.Forms.Label();
            this.asOfValueLabel = new System.Windows.Forms.Label();
            this.asOfLabel = new System.Windows.Forms.Label();
            this.pageLabel = new System.Windows.Forms.Label();
            this.refurbColumnLabel = new System.Windows.Forms.Label();
            this.logoTitleImage = new System.Windows.Forms.PictureBox();
            this.reportTitleLabel = new System.Windows.Forms.Label();
            this.LoanNoColumnLabel = new System.Windows.Forms.Label();
            this.ticketDescriptionColumnLabel = new System.Windows.Forms.Label();
            this.noTagsColumnLabel = new System.Windows.Forms.Label();
            this.costColumnLabel = new System.Windows.Forms.Label();
            this.retailColumnLabel = new System.Windows.Forms.Label();
            this.reasonColumnLabel = new System.Windows.Forms.Label();
            this.tlpRecords = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tagsValueLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.totalAmountValueLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoTitleImage)).BeginInit();
            this.tlpRecords.SuspendLayout();
            this.SuspendLayout();
            // 
            // storeValueLabel
            // 
            this.storeValueLabel.AutoSize = true;
            this.storeValueLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeValueLabel.Location = new System.Drawing.Point(107, 55);
            this.storeValueLabel.Name = "storeValueLabel";
            this.storeValueLabel.Size = new System.Drawing.Size(64, 16);
            this.storeValueLabel.TabIndex = 5;
            this.storeValueLabel.Text = "[01660]";
            this.storeValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // storeLabel
            // 
            this.storeLabel.AutoSize = true;
            this.storeLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeLabel.Location = new System.Drawing.Point(15, 55);
            this.storeLabel.Name = "storeLabel";
            this.storeLabel.Size = new System.Drawing.Size(56, 16);
            this.storeLabel.TabIndex = 4;
            this.storeLabel.Text = "Store:";
            this.storeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // runValueLabel
            // 
            this.runValueLabel.AutoSize = true;
            this.runValueLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runValueLabel.Location = new System.Drawing.Point(608, 55);
            this.runValueLabel.Name = "runValueLabel";
            this.runValueLabel.Size = new System.Drawing.Size(152, 16);
            this.runValueLabel.TabIndex = 15;
            this.runValueLabel.Text = "[mm/dd/yyyy hh:mm]";
            this.runValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // runLabel
            // 
            this.runLabel.AutoSize = true;
            this.runLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runLabel.Location = new System.Drawing.Point(562, 55);
            this.runLabel.Name = "runLabel";
            this.runLabel.Size = new System.Drawing.Size(40, 16);
            this.runLabel.TabIndex = 14;
            this.runLabel.Text = "Run:";
            this.runLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // asOfValueLabel
            // 
            this.asOfValueLabel.AutoSize = true;
            this.asOfValueLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asOfValueLabel.Location = new System.Drawing.Point(608, 70);
            this.asOfValueLabel.Name = "asOfValueLabel";
            this.asOfValueLabel.Size = new System.Drawing.Size(104, 16);
            this.asOfValueLabel.TabIndex = 17;
            this.asOfValueLabel.Text = "[mm/dd/yyyy]";
            this.asOfValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // asOfLabel
            // 
            this.asOfLabel.AutoSize = true;
            this.asOfLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asOfLabel.Location = new System.Drawing.Point(546, 70);
            this.asOfLabel.Name = "asOfLabel";
            this.asOfLabel.Size = new System.Drawing.Size(56, 16);
            this.asOfLabel.TabIndex = 16;
            this.asOfLabel.Text = "As Of:";
            this.asOfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageLabel.Location = new System.Drawing.Point(608, 85);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(112, 16);
            this.pageLabel.TabIndex = 19;
            this.pageLabel.Text = "[Page x of X]";
            this.pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // refurbColumnLabel
            // 
            this.refurbColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refurbColumnLabel.Location = new System.Drawing.Point(0, 0);
            this.refurbColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.refurbColumnLabel.Name = "refurbColumnLabel";
            this.refurbColumnLabel.Size = new System.Drawing.Size(60, 20);
            this.refurbColumnLabel.TabIndex = 37;
            this.refurbColumnLabel.Text = "Refurb#";
            this.refurbColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // logoTitleImage
            // 
            this.logoTitleImage.Image = global::Pawn.Properties.Resources.logo;
            this.logoTitleImage.Location = new System.Drawing.Point(12, 5);
            this.logoTitleImage.Name = "logoTitleImage";
            this.logoTitleImage.Size = new System.Drawing.Size(285, 46);
            this.logoTitleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoTitleImage.TabIndex = 38;
            this.logoTitleImage.TabStop = false;
            // 
            // reportTitleLabel
            // 
            this.reportTitleLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportTitleLabel.Location = new System.Drawing.Point(181, 106);
            this.reportTitleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.reportTitleLabel.Name = "reportTitleLabel";
            this.reportTitleLabel.Size = new System.Drawing.Size(424, 22);
            this.reportTitleLabel.TabIndex = 39;
            this.reportTitleLabel.Text = "PFI REFURB REPORT";
            this.reportTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoanNoColumnLabel
            // 
            this.LoanNoColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanNoColumnLabel.Location = new System.Drawing.Point(70, 0);
            this.LoanNoColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.LoanNoColumnLabel.Name = "LoanNoColumnLabel";
            this.LoanNoColumnLabel.Size = new System.Drawing.Size(60, 20);
            this.LoanNoColumnLabel.TabIndex = 40;
            this.LoanNoColumnLabel.Text = "Loan#";
            this.LoanNoColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ticketDescriptionColumnLabel
            // 
            this.ticketDescriptionColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticketDescriptionColumnLabel.Location = new System.Drawing.Point(140, 0);
            this.ticketDescriptionColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ticketDescriptionColumnLabel.Name = "ticketDescriptionColumnLabel";
            this.ticketDescriptionColumnLabel.Size = new System.Drawing.Size(133, 20);
            this.ticketDescriptionColumnLabel.TabIndex = 41;
            this.ticketDescriptionColumnLabel.Text = "Ticket Description";
            this.ticketDescriptionColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // noTagsColumnLabel
            // 
            this.noTagsColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noTagsColumnLabel.Location = new System.Drawing.Point(420, 0);
            this.noTagsColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.noTagsColumnLabel.Name = "noTagsColumnLabel";
            this.noTagsColumnLabel.Size = new System.Drawing.Size(60, 20);
            this.noTagsColumnLabel.TabIndex = 42;
            this.noTagsColumnLabel.Text = "#Tag";
            this.noTagsColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // costColumnLabel
            // 
            this.costColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costColumnLabel.Location = new System.Drawing.Point(480, 0);
            this.costColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.costColumnLabel.Name = "costColumnLabel";
            this.costColumnLabel.Size = new System.Drawing.Size(60, 20);
            this.costColumnLabel.TabIndex = 43;
            this.costColumnLabel.Text = "Cost";
            this.costColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // retailColumnLabel
            // 
            this.retailColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retailColumnLabel.Location = new System.Drawing.Point(540, 0);
            this.retailColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.retailColumnLabel.Name = "retailColumnLabel";
            this.retailColumnLabel.Size = new System.Drawing.Size(60, 20);
            this.retailColumnLabel.TabIndex = 44;
            this.retailColumnLabel.Text = "Retail";
            this.retailColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // reasonColumnLabel
            // 
            this.reasonColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reasonColumnLabel.Location = new System.Drawing.Point(600, 0);
            this.reasonColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.reasonColumnLabel.Name = "reasonColumnLabel";
            this.reasonColumnLabel.Size = new System.Drawing.Size(175, 20);
            this.reasonColumnLabel.TabIndex = 45;
            this.reasonColumnLabel.Text = "Reason";
            this.reasonColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpRecords
            // 
            this.tlpRecords.AutoSize = true;
            this.tlpRecords.ColumnCount = 7;
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tlpRecords.Controls.Add(this.LoanNoColumnLabel, 1, 0);
            this.tlpRecords.Controls.Add(this.reasonColumnLabel, 6, 0);
            this.tlpRecords.Controls.Add(this.ticketDescriptionColumnLabel, 2, 0);
            this.tlpRecords.Controls.Add(this.refurbColumnLabel, 0, 0);
            this.tlpRecords.Controls.Add(this.noTagsColumnLabel, 3, 0);
            this.tlpRecords.Controls.Add(this.costColumnLabel, 4, 0);
            this.tlpRecords.Controls.Add(this.retailColumnLabel, 5, 0);
            this.tlpRecords.Location = new System.Drawing.Point(13, 128);
            this.tlpRecords.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRecords.Name = "tlpRecords";
            this.tlpRecords.RowCount = 1;
            this.tlpRecords.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRecords.Size = new System.Drawing.Size(785, 20);
            this.tlpRecords.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(530, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 48;
            this.label1.Text = "Records:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tagsValueLabel
            // 
            this.tagsValueLabel.AutoSize = true;
            this.tagsValueLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagsValueLabel.Location = new System.Drawing.Point(107, 70);
            this.tagsValueLabel.Name = "tagsValueLabel";
            this.tagsValueLabel.Size = new System.Drawing.Size(32, 16);
            this.tagsValueLabel.TabIndex = 50;
            this.tagsValueLabel.Text = "[3]";
            this.tagsValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 49;
            this.label3.Text = "Tags:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalAmountValueLabel
            // 
            this.totalAmountValueLabel.AutoSize = true;
            this.totalAmountValueLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmountValueLabel.Location = new System.Drawing.Point(107, 85);
            this.totalAmountValueLabel.Name = "totalAmountValueLabel";
            this.totalAmountValueLabel.Size = new System.Drawing.Size(32, 16);
            this.totalAmountValueLabel.TabIndex = 52;
            this.totalAmountValueLabel.Text = "[1]";
            this.totalAmountValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 51;
            this.label4.Text = "Total Amt:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Print_PfiRefurbList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(798, 150);
            this.ControlBox = false;
            this.Controls.Add(this.totalAmountValueLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tagsValueLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tlpRecords);
            this.Controls.Add(this.reportTitleLabel);
            this.Controls.Add(this.logoTitleImage);
            this.Controls.Add(this.pageLabel);
            this.Controls.Add(this.asOfValueLabel);
            this.Controls.Add(this.asOfLabel);
            this.Controls.Add(this.runValueLabel);
            this.Controls.Add(this.runLabel);
            this.Controls.Add(this.storeValueLabel);
            this.Controls.Add(this.storeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Print_PfiRefurbList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Print_PfiRefurbList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoTitleImage)).EndInit();
            this.tlpRecords.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label storeValueLabel;
        private System.Windows.Forms.Label storeLabel;
        private System.Windows.Forms.Label runValueLabel;
        private System.Windows.Forms.Label runLabel;
        private System.Windows.Forms.Label asOfValueLabel;
        private System.Windows.Forms.Label asOfLabel;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Label refurbColumnLabel;
        private System.Windows.Forms.PictureBox logoTitleImage;
        private System.Windows.Forms.Label reportTitleLabel;
        private System.Windows.Forms.Label LoanNoColumnLabel;
        private System.Windows.Forms.Label ticketDescriptionColumnLabel;
        private System.Windows.Forms.Label noTagsColumnLabel;
        private System.Windows.Forms.Label costColumnLabel;
        private System.Windows.Forms.Label retailColumnLabel;
        private System.Windows.Forms.Label reasonColumnLabel;
        private System.Windows.Forms.TableLayoutPanel tlpRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label tagsValueLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label totalAmountValueLabel;
        private System.Windows.Forms.Label label4;
    }
}