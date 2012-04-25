using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Tender.Base
{
    partial class TenderEntryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TenderEntryForm));
            this.okButton = new Common.Libraries.Forms.Components.CustomButton();
            this.cancelButton = new Common.Libraries.Forms.Components.CustomButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.refNumLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.creditLabel = new System.Windows.Forms.Label();
            this.refNumTextBox = new System.Windows.Forms.TextBox();
            this.tenderTypeComboBox = new System.Windows.Forms.ComboBox();
            this.debitTypeListBox = new System.Windows.Forms.ComboBox();
            this.creditTypeListBox = new System.Windows.Forms.ComboBox();
            this.amountEntryTextBox = new Common.Libraries.Forms.Components.CustomTextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("okButton.BackgroundImage")));
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(371, 246);
            this.okButton.Margin = new System.Windows.Forms.Padding(0);
            this.okButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.okButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 50);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(9, 246);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(190, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tender Entry";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tender Type:";
            // 
            // refNumLabel
            // 
            this.refNumLabel.AutoSize = true;
            this.refNumLabel.BackColor = System.Drawing.Color.Transparent;
            this.refNumLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refNumLabel.Location = new System.Drawing.Point(48, 133);
            this.refNumLabel.Name = "refNumLabel";
            this.refNumLabel.Size = new System.Drawing.Size(133, 16);
            this.refNumLabel.TabIndex = 4;
            this.refNumLabel.Text = "Reference Number:";
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.BackColor = System.Drawing.Color.Transparent;
            this.amountLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountLabel.Location = new System.Drawing.Point(117, 164);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(64, 16);
            this.amountLabel.TabIndex = 5;
            this.amountLabel.Text = "Amount:";
            // 
            // creditLabel
            // 
            this.creditLabel.AutoSize = true;
            this.creditLabel.BackColor = System.Drawing.Color.Transparent;
            this.creditLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditLabel.Location = new System.Drawing.Point(103, 198);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(78, 16);
            this.creditLabel.TabIndex = 6;
            this.creditLabel.Text = "Card Type:";
            // 
            // refNumTextBox
            // 
            this.refNumTextBox.BackColor = System.Drawing.Color.White;
            this.refNumTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refNumTextBox.ForeColor = System.Drawing.Color.Black;
            this.refNumTextBox.Location = new System.Drawing.Point(207, 131);
            this.refNumTextBox.MaxLength = 20;
            this.refNumTextBox.Name = "refNumTextBox";
            this.refNumTextBox.Size = new System.Drawing.Size(225, 22);
            this.refNumTextBox.TabIndex = 8;
            this.refNumTextBox.TextChanged += new System.EventHandler(this.refNumTextBox_TextChanged);
            // 
            // tenderTypeComboBox
            // 
            this.tenderTypeComboBox.BackColor = System.Drawing.Color.White;
            this.tenderTypeComboBox.Enabled = false;
            this.tenderTypeComboBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderTypeComboBox.ForeColor = System.Drawing.Color.Black;
            this.tenderTypeComboBox.FormattingEnabled = true;
            this.tenderTypeComboBox.Items.AddRange(new object[] {
            "Cash",
            "Check",
            "Credit Card",
            "Debit Card",
            "Coupon",
            "PayPal",
            "Bill To AP",
            "Store Credit"});
            this.tenderTypeComboBox.Location = new System.Drawing.Point(207, 103);
            this.tenderTypeComboBox.Name = "tenderTypeComboBox";
            this.tenderTypeComboBox.Size = new System.Drawing.Size(225, 24);
            this.tenderTypeComboBox.TabIndex = 11;
            // 
            // debitTypeListBox
            // 
            this.debitTypeListBox.BackColor = System.Drawing.Color.White;
            this.debitTypeListBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debitTypeListBox.ForeColor = System.Drawing.Color.Black;
            this.debitTypeListBox.FormattingEnabled = true;
            this.debitTypeListBox.Items.AddRange(new object[] {
            "VISA",
            "MasterCard",
            "AMEX",
            "Discover"});
            this.debitTypeListBox.Location = new System.Drawing.Point(207, 197);
            this.debitTypeListBox.MaxDropDownItems = 2;
            this.debitTypeListBox.Name = "debitTypeListBox";
            this.debitTypeListBox.Size = new System.Drawing.Size(225, 24);
            this.debitTypeListBox.TabIndex = 12;
            this.debitTypeListBox.Text = "Select Debit Type";
            this.debitTypeListBox.SelectedIndexChanged += new System.EventHandler(this.debitTypeListBox_SelectedIndexChanged);
            // 
            // creditTypeListBox
            // 
            this.creditTypeListBox.BackColor = System.Drawing.Color.White;
            this.creditTypeListBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditTypeListBox.ForeColor = System.Drawing.Color.Black;
            this.creditTypeListBox.FormattingEnabled = true;
            this.creditTypeListBox.Items.AddRange(new object[] {
            "VISA",
            "MasterCard",
            "AMEX",
            "Discover"});
            this.creditTypeListBox.Location = new System.Drawing.Point(207, 198);
            this.creditTypeListBox.MaxDropDownItems = 3;
            this.creditTypeListBox.Name = "creditTypeListBox";
            this.creditTypeListBox.Size = new System.Drawing.Size(225, 24);
            this.creditTypeListBox.TabIndex = 10;
            this.creditTypeListBox.Text = "Select Credit Type";
            this.creditTypeListBox.SelectedIndexChanged += new System.EventHandler(this.creditTypeListBox_SelectedIndexChanged);
            // 
            // amountEntryTextBox
            // 
            this.amountEntryTextBox.AllowDecimalNumbers = true;
            this.amountEntryTextBox.CausesValidation = false;
            this.amountEntryTextBox.ErrorMessage = "";
            this.amountEntryTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountEntryTextBox.Location = new System.Drawing.Point(207, 164);
            this.amountEntryTextBox.Name = "amountEntryTextBox";
            this.amountEntryTextBox.Size = new System.Drawing.Size(225, 21);
            this.amountEntryTextBox.TabIndex = 9;
            this.amountEntryTextBox.ValidationExpression = "";
            this.amountEntryTextBox.TextChanged += new System.EventHandler(this.amountEntryTextBox_TextChanged);
            // 
            // TenderEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(480, 320);
            this.Controls.Add(this.amountEntryTextBox);
            this.Controls.Add(this.creditTypeListBox);
            this.Controls.Add(this.debitTypeListBox);
            this.Controls.Add(this.tenderTypeComboBox);
            this.Controls.Add(this.refNumTextBox);
            this.Controls.Add(this.creditLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.refNumLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "TenderEntryForm";
            this.Text = "TenderEntryForm";
            this.Load += new System.EventHandler(this.TenderEntryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton okButton;
        private CustomButton cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label refNumLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.Label creditLabel;
        private System.Windows.Forms.TextBox refNumTextBox;
        private System.Windows.Forms.ComboBox tenderTypeComboBox;
        private System.Windows.Forms.ComboBox debitTypeListBox;
        private System.Windows.Forms.ComboBox creditTypeListBox;
        private CustomTextBox amountEntryTextBox;
    }
}