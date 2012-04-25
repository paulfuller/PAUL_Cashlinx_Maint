using System.Windows.Forms;

namespace Pawn.Forms.Pawn.Services.Receipt
{
    partial class VoidReceiptSummary
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.loanSummaryPanel = new System.Windows.Forms.Panel();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.loanAmountTextBox = new System.Windows.Forms.TextBox();
            this.loanNumberTextBox = new System.Windows.Forms.TextBox();
            this.dueDateTextBox = new System.Windows.Forms.TextBox();
            this.orgDateTimeTextBox = new System.Windows.Forms.TextBox();
            this.userIdLabel = new System.Windows.Forms.Label();
            this.loanAmountLabel = new System.Windows.Forms.Label();
            this.loanNumberLabel = new System.Windows.Forms.Label();
            this.dueDateLabel = new System.Windows.Forms.Label();
            this.orgDateTimeLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.informationMessageLabel = new System.Windows.Forms.Label();
            this.itemDetailsTableHeaderPanel = new System.Windows.Forms.Panel();
            this.itemAmountLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.gunNumberLabel = new System.Windows.Forms.Label();
            this.icnLabel = new System.Windows.Forms.Label();
            this.pawnItemListView = new System.Windows.Forms.ListView();
            this.icnHeader = new System.Windows.Forms.ColumnHeader();
            this.gunHeader = new System.Windows.Forms.ColumnHeader();
            this.descriptionHeader = new System.Windows.Forms.ColumnHeader();
            this.itemAmountHeader = new System.Windows.Forms.ColumnHeader();
            this.loanSummaryPanel.SuspendLayout();
            this.itemDetailsTableHeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(295, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(170, 19);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Void Receipt Summary";
            // 
            // loanSummaryPanel
            // 
            this.loanSummaryPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loanSummaryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loanSummaryPanel.Controls.Add(this.userIdTextBox);
            this.loanSummaryPanel.Controls.Add(this.loanAmountTextBox);
            this.loanSummaryPanel.Controls.Add(this.loanNumberTextBox);
            this.loanSummaryPanel.Controls.Add(this.dueDateTextBox);
            this.loanSummaryPanel.Controls.Add(this.orgDateTimeTextBox);
            this.loanSummaryPanel.Controls.Add(this.userIdLabel);
            this.loanSummaryPanel.Controls.Add(this.loanAmountLabel);
            this.loanSummaryPanel.Controls.Add(this.loanNumberLabel);
            this.loanSummaryPanel.Controls.Add(this.dueDateLabel);
            this.loanSummaryPanel.Controls.Add(this.orgDateTimeLabel);
            this.loanSummaryPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanSummaryPanel.ForeColor = System.Drawing.Color.Black;
            this.loanSummaryPanel.Location = new System.Drawing.Point(13, 90);
            this.loanSummaryPanel.Name = "loanSummaryPanel";
            this.loanSummaryPanel.Size = new System.Drawing.Size(735, 121);
            this.loanSummaryPanel.TabIndex = 1;
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userIdTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userIdTextBox.ForeColor = System.Drawing.Color.Black;
            this.userIdTextBox.Location = new System.Drawing.Point(516, 77);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(150, 14);
            this.userIdTextBox.TabIndex = 905;
            // 
            // loanAmountTextBox
            // 
            this.loanAmountTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loanAmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loanAmountTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanAmountTextBox.ForeColor = System.Drawing.Color.Black;
            this.loanAmountTextBox.Location = new System.Drawing.Point(516, 53);
            this.loanAmountTextBox.Name = "loanAmountTextBox";
            this.loanAmountTextBox.Size = new System.Drawing.Size(150, 14);
            this.loanAmountTextBox.TabIndex = 904;
            // 
            // loanNumberTextBox
            // 
            this.loanNumberTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loanNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loanNumberTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanNumberTextBox.ForeColor = System.Drawing.Color.Black;
            this.loanNumberTextBox.Location = new System.Drawing.Point(516, 26);
            this.loanNumberTextBox.Name = "loanNumberTextBox";
            this.loanNumberTextBox.Size = new System.Drawing.Size(150, 14);
            this.loanNumberTextBox.TabIndex = 903;
            // 
            // dueDateTextBox
            // 
            this.dueDateTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dueDateTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dueDateTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dueDateTextBox.ForeColor = System.Drawing.Color.Black;
            this.dueDateTextBox.Location = new System.Drawing.Point(159, 53);
            this.dueDateTextBox.Name = "dueDateTextBox";
            this.dueDateTextBox.Size = new System.Drawing.Size(150, 14);
            this.dueDateTextBox.TabIndex = 902;
            // 
            // orgDateTimeTextBox
            // 
            this.orgDateTimeTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.orgDateTimeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.orgDateTimeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orgDateTimeTextBox.ForeColor = System.Drawing.Color.Black;
            this.orgDateTimeTextBox.Location = new System.Drawing.Point(159, 26);
            this.orgDateTimeTextBox.Name = "orgDateTimeTextBox";
            this.orgDateTimeTextBox.Size = new System.Drawing.Size(150, 14);
            this.orgDateTimeTextBox.TabIndex = 901;
            // 
            // userIdLabel
            // 
            this.userIdLabel.AutoSize = true;
            this.userIdLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userIdLabel.Location = new System.Drawing.Point(436, 79);
            this.userIdLabel.Name = "userIdLabel";
            this.userIdLabel.Size = new System.Drawing.Size(47, 13);
            this.userIdLabel.TabIndex = 4;
            this.userIdLabel.Text = "User ID:";
            // 
            // loanAmountLabel
            // 
            this.loanAmountLabel.AutoSize = true;
            this.loanAmountLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanAmountLabel.Location = new System.Drawing.Point(436, 54);
            this.loanAmountLabel.Name = "loanAmountLabel";
            this.loanAmountLabel.Size = new System.Drawing.Size(74, 13);
            this.loanAmountLabel.TabIndex = 3;
            this.loanAmountLabel.Text = "Loan Amount:";
            // 
            // loanNumberLabel
            // 
            this.loanNumberLabel.AutoSize = true;
            this.loanNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanNumberLabel.Location = new System.Drawing.Point(436, 27);
            this.loanNumberLabel.Name = "loanNumberLabel";
            this.loanNumberLabel.Size = new System.Drawing.Size(74, 13);
            this.loanNumberLabel.TabIndex = 2;
            this.loanNumberLabel.Text = "Loan Number:";
            // 
            // dueDateLabel
            // 
            this.dueDateLabel.AutoSize = true;
            this.dueDateLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dueDateLabel.Location = new System.Drawing.Point(39, 54);
            this.dueDateLabel.Name = "dueDateLabel";
            this.dueDateLabel.Size = new System.Drawing.Size(56, 13);
            this.dueDateLabel.TabIndex = 0;
            this.dueDateLabel.Text = "Due Date:";
            // 
            // orgDateTimeLabel
            // 
            this.orgDateTimeLabel.AutoSize = true;
            this.orgDateTimeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orgDateTimeLabel.Location = new System.Drawing.Point(39, 27);
            this.orgDateTimeLabel.Name = "orgDateTimeLabel";
            this.orgDateTimeLabel.Size = new System.Drawing.Size(115, 13);
            this.orgDateTimeLabel.TabIndex = 900;
            this.orgDateTimeLabel.Text = "Origination Date/Time:";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(12, 352);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(648, 352);
            this.submitButton.Margin = new System.Windows.Forms.Padding(0);
            this.submitButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.submitButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 50);
            this.submitButton.TabIndex = 1;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // informationMessageLabel
            // 
            this.informationMessageLabel.AutoSize = true;
            this.informationMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.informationMessageLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.informationMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.informationMessageLabel.Location = new System.Drawing.Point(16, 71);
            this.informationMessageLabel.Name = "informationMessageLabel";
            this.informationMessageLabel.Size = new System.Drawing.Size(203, 16);
            this.informationMessageLabel.TabIndex = 4;
            this.informationMessageLabel.Text = "Information Message Goes Here...";
            this.informationMessageLabel.Visible = false;
            // 
            // itemDetailsTableHeaderPanel
            // 
            this.itemDetailsTableHeaderPanel.BackColor = System.Drawing.Color.DarkBlue;
            this.itemDetailsTableHeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemDetailsTableHeaderPanel.Controls.Add(this.itemAmountLabel);
            this.itemDetailsTableHeaderPanel.Controls.Add(this.descriptionLabel);
            this.itemDetailsTableHeaderPanel.Controls.Add(this.gunNumberLabel);
            this.itemDetailsTableHeaderPanel.Controls.Add(this.icnLabel);
            this.itemDetailsTableHeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemDetailsTableHeaderPanel.Location = new System.Drawing.Point(12, 210);
            this.itemDetailsTableHeaderPanel.Name = "itemDetailsTableHeaderPanel";
            this.itemDetailsTableHeaderPanel.Size = new System.Drawing.Size(736, 27);
            this.itemDetailsTableHeaderPanel.TabIndex = 5;
            // 
            // itemAmountLabel
            // 
            this.itemAmountLabel.AutoSize = true;
            this.itemAmountLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemAmountLabel.ForeColor = System.Drawing.Color.White;
            this.itemAmountLabel.Location = new System.Drawing.Point(637, 7);
            this.itemAmountLabel.Name = "itemAmountLabel";
            this.itemAmountLabel.Size = new System.Drawing.Size(82, 16);
            this.itemAmountLabel.TabIndex = 3;
            this.itemAmountLabel.Text = "Item Amount";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.Color.White;
            this.descriptionLabel.Location = new System.Drawing.Point(392, 7);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(71, 16);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Description";
            // 
            // gunNumberLabel
            // 
            this.gunNumberLabel.AutoSize = true;
            this.gunNumberLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gunNumberLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunNumberLabel.ForeColor = System.Drawing.Color.White;
            this.gunNumberLabel.Location = new System.Drawing.Point(271, 7);
            this.gunNumberLabel.Name = "gunNumberLabel";
            this.gunNumberLabel.Size = new System.Drawing.Size(53, 16);
            this.gunNumberLabel.TabIndex = 1;
            this.gunNumberLabel.Text = "Gun No.";
            // 
            // icnLabel
            // 
            this.icnLabel.AutoSize = true;
            this.icnLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.icnLabel.ForeColor = System.Drawing.Color.White;
            this.icnLabel.Location = new System.Drawing.Point(68, 7);
            this.icnLabel.Name = "icnLabel";
            this.icnLabel.Size = new System.Drawing.Size(41, 16);
            this.icnLabel.TabIndex = 0;
            this.icnLabel.Text = "ICN #";
            // 
            // pawnItemListView
            // 
            this.pawnItemListView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pawnItemListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pawnItemListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.icnHeader,
            this.gunHeader,
            this.descriptionHeader,
            this.itemAmountHeader});
            this.pawnItemListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pawnItemListView.ForeColor = System.Drawing.Color.Black;
            this.pawnItemListView.FullRowSelect = true;
            this.pawnItemListView.GridLines = true;
            this.pawnItemListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.pawnItemListView.HideSelection = false;
            this.pawnItemListView.Location = new System.Drawing.Point(12, 236);
            this.pawnItemListView.MultiSelect = false;
            this.pawnItemListView.Name = "pawnItemListView";
            this.pawnItemListView.Size = new System.Drawing.Size(736, 110);
            this.pawnItemListView.TabIndex = 6;
            this.pawnItemListView.TabStop = false;
            this.pawnItemListView.UseCompatibleStateImageBehavior = false;
            this.pawnItemListView.View = System.Windows.Forms.View.Details;
            // 
            // icnHeader
            // 
            this.icnHeader.Text = "";
            this.icnHeader.Width = 275;
            // 
            // gunHeader
            // 
            this.gunHeader.Text = "";
            this.gunHeader.Width = 42;
            // 
            // descriptionHeader
            // 
            this.descriptionHeader.Text = "";
            this.descriptionHeader.Width = 284;
            // 
            // itemAmountHeader
            // 
            this.itemAmountHeader.Text = "";
            this.itemAmountHeader.Width = 127;
            // 
            // VoidReceiptSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(761, 414);
            this.Controls.Add(this.pawnItemListView);
            this.Controls.Add(this.itemDetailsTableHeaderPanel);
            this.Controls.Add(this.informationMessageLabel);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loanSummaryPanel);
            this.Controls.Add(this.titleLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VoidReceiptSummary";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VoidReceiptSummary";
            this.Load += new System.EventHandler(this.VoidReceiptSummary_Load);
            this.loanSummaryPanel.ResumeLayout(false);
            this.loanSummaryPanel.PerformLayout();
            this.itemDetailsTableHeaderPanel.ResumeLayout(false);
            this.itemDetailsTableHeaderPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel loanSummaryPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Label informationMessageLabel;
        private System.Windows.Forms.TextBox dueDateTextBox;
        private System.Windows.Forms.TextBox orgDateTimeTextBox;
        private System.Windows.Forms.Label userIdLabel;
        private System.Windows.Forms.Label loanAmountLabel;
        private System.Windows.Forms.Label loanNumberLabel;
        private System.Windows.Forms.Label dueDateLabel;
        private System.Windows.Forms.Label orgDateTimeLabel;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.TextBox loanAmountTextBox;
        private System.Windows.Forms.TextBox loanNumberTextBox;
        private System.Windows.Forms.Panel itemDetailsTableHeaderPanel;
        private System.Windows.Forms.Label itemAmountLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label gunNumberLabel;
        private System.Windows.Forms.Label icnLabel;
        private ListView pawnItemListView;
        private ColumnHeader icnHeader;
        private ColumnHeader gunHeader;
        private ColumnHeader descriptionHeader;
        private ColumnHeader itemAmountHeader;
    }
}