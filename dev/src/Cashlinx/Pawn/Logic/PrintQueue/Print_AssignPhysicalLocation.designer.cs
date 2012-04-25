using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Logic.PrintQueue
{
    partial class Print_AssignPhysicalLocation
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
            this.userColumnLabel = new System.Windows.Forms.Label();
            this.logoTitleImage = new System.Windows.Forms.PictureBox();
            this.reportTitleLabel = new System.Windows.Forms.Label();
            this.ticketNoColumnLabel = new System.Windows.Forms.Label();
            this.ticketDescriptionColumnLabel = new System.Windows.Forms.Label();
            this.aisleColumnLabel = new System.Windows.Forms.Label();
            this.shelfColumnLabel = new System.Windows.Forms.Label();
            this.otherColumnLabel = new System.Windows.Forms.Label();
            this.gunNoColumnLabel = new System.Windows.Forms.Label();
            this.tlpRecords = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoTitleImage)).BeginInit();
            this.tlpRecords.SuspendLayout();
            this.SuspendLayout();
            // 
            // storeValueLabel
            // 
            this.storeValueLabel.AutoSize = true;
            this.storeValueLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeValueLabel.Location = new System.Drawing.Point(81, 55);
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
            // userColumnLabel
            // 
            this.userColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userColumnLabel.Location = new System.Drawing.Point(0, 0);
            this.userColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.userColumnLabel.Name = "userColumnLabel";
            this.userColumnLabel.Size = new System.Drawing.Size(35, 20);
            this.userColumnLabel.TabIndex = 37;
            this.userColumnLabel.Text = "User";
            this.userColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.reportTitleLabel.Text = "MERCHANDISE PENDING LOCATION";
            this.reportTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ticketNoColumnLabel
            // 
            this.ticketNoColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticketNoColumnLabel.Location = new System.Drawing.Point(65, 0);
            this.ticketNoColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ticketNoColumnLabel.Name = "ticketNoColumnLabel";
            this.ticketNoColumnLabel.Size = new System.Drawing.Size(77, 20);
            this.ticketNoColumnLabel.TabIndex = 40;
            this.ticketNoColumnLabel.Text = "Ticket No.";
            this.ticketNoColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ticketDescriptionColumnLabel
            // 
            this.ticketDescriptionColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticketDescriptionColumnLabel.Location = new System.Drawing.Point(145, 0);
            this.ticketDescriptionColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ticketDescriptionColumnLabel.Name = "ticketDescriptionColumnLabel";
            this.ticketDescriptionColumnLabel.Size = new System.Drawing.Size(133, 20);
            this.ticketDescriptionColumnLabel.TabIndex = 41;
            this.ticketDescriptionColumnLabel.Text = "Ticket Description";
            this.ticketDescriptionColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // aisleColumnLabel
            // 
            this.aisleColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aisleColumnLabel.Location = new System.Drawing.Point(495, 0);
            this.aisleColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.aisleColumnLabel.Name = "aisleColumnLabel";
            this.aisleColumnLabel.Size = new System.Drawing.Size(42, 20);
            this.aisleColumnLabel.TabIndex = 42;
            this.aisleColumnLabel.Text = "Aisle";
            this.aisleColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // shelfColumnLabel
            // 
            this.shelfColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shelfColumnLabel.Location = new System.Drawing.Point(540, 0);
            this.shelfColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.shelfColumnLabel.Name = "shelfColumnLabel";
            this.shelfColumnLabel.Size = new System.Drawing.Size(42, 20);
            this.shelfColumnLabel.TabIndex = 43;
            this.shelfColumnLabel.Text = "Shelf";
            this.shelfColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // otherColumnLabel
            // 
            this.otherColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otherColumnLabel.Location = new System.Drawing.Point(585, 0);
            this.otherColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.otherColumnLabel.Name = "otherColumnLabel";
            this.otherColumnLabel.Size = new System.Drawing.Size(42, 20);
            this.otherColumnLabel.TabIndex = 44;
            this.otherColumnLabel.Text = "Other";
            this.otherColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gunNoColumnLabel
            // 
            this.gunNoColumnLabel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunNoColumnLabel.Location = new System.Drawing.Point(695, 0);
            this.gunNoColumnLabel.Margin = new System.Windows.Forms.Padding(0);
            this.gunNoColumnLabel.Name = "gunNoColumnLabel";
            this.gunNoColumnLabel.Size = new System.Drawing.Size(56, 20);
            this.gunNoColumnLabel.TabIndex = 45;
            this.gunNoColumnLabel.Text = "Gun No.";
            this.gunNoColumnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpRecords
            // 
            this.tlpRecords.AutoSize = true;
            this.tlpRecords.ColumnCount = 7;
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpRecords.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpRecords.Controls.Add(this.ticketNoColumnLabel, 1, 0);
            this.tlpRecords.Controls.Add(this.gunNoColumnLabel, 6, 0);
            this.tlpRecords.Controls.Add(this.ticketDescriptionColumnLabel, 2, 0);
            this.tlpRecords.Controls.Add(this.otherColumnLabel, 5, 0);
            this.tlpRecords.Controls.Add(this.aisleColumnLabel, 3, 0);
            this.tlpRecords.Controls.Add(this.shelfColumnLabel, 4, 0);
            this.tlpRecords.Controls.Add(this.userColumnLabel, 0, 0);
            this.tlpRecords.Location = new System.Drawing.Point(13, 128);
            this.tlpRecords.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRecords.Name = "tlpRecords";
            this.tlpRecords.RowCount = 1;
            this.tlpRecords.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRecords.Size = new System.Drawing.Size(795, 20);
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
            // Print_AssignPhysicalLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(798, 150);
            this.ControlBox = false;
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
            this.Name = "Print_AssignPhysicalLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Print_AssignPhysicalLocation_Load);
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
        private System.Windows.Forms.Label userColumnLabel;
        private System.Windows.Forms.PictureBox logoTitleImage;
        private System.Windows.Forms.Label reportTitleLabel;
        private System.Windows.Forms.Label ticketNoColumnLabel;
        private System.Windows.Forms.Label ticketDescriptionColumnLabel;
        private System.Windows.Forms.Label aisleColumnLabel;
        private System.Windows.Forms.Label shelfColumnLabel;
        private System.Windows.Forms.Label otherColumnLabel;
        private System.Windows.Forms.Label gunNoColumnLabel;
        private System.Windows.Forms.TableLayoutPanel tlpRecords;
        private System.Windows.Forms.Label label1;
    }
}