namespace Pawn.Forms.ReleaseFingerprints
{
    partial class LoanBuyLookup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanBuyLookup));
            this.LoanBuyLookupFormLabel = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.LoanNumberRadioButton = new System.Windows.Forms.RadioButton();
            this.BuyNumberRadioButton = new System.Windows.Forms.RadioButton();
            this.LoanStoreText = new System.Windows.Forms.TextBox();
            this.BuyStoreText = new System.Windows.Forms.TextBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.LoanNumberText = new Common.Libraries.Forms.Components.CustomTextBox();
            this.BuyNumberText = new Common.Libraries.Forms.Components.CustomTextBox();
            this.SuspendLayout();
            // 
            // LoanBuyLookupFormLabel
            // 
            this.LoanBuyLookupFormLabel.AutoSize = true;
            this.LoanBuyLookupFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoanBuyLookupFormLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.LoanBuyLookupFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanBuyLookupFormLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.LoanBuyLookupFormLabel.Location = new System.Drawing.Point(199, 25);
            this.LoanBuyLookupFormLabel.Name = "LoanBuyLookupFormLabel";
            this.LoanBuyLookupFormLabel.Size = new System.Drawing.Size(142, 19);
            this.LoanBuyLookupFormLabel.TabIndex = 7;
            this.LoanBuyLookupFormLabel.Text = "Loan / Buy Lookup";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCancel.BackgroundImage")));
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(45, 205);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 141;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LoanNumberRadioButton
            // 
            this.LoanNumberRadioButton.AutoSize = true;
            this.LoanNumberRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.LoanNumberRadioButton.Checked = true;
            this.LoanNumberRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanNumberRadioButton.Location = new System.Drawing.Point(93, 118);
            this.LoanNumberRadioButton.Name = "LoanNumberRadioButton";
            this.LoanNumberRadioButton.Size = new System.Drawing.Size(100, 17);
            this.LoanNumberRadioButton.TabIndex = 142;
            this.LoanNumberRadioButton.TabStop = true;
            this.LoanNumberRadioButton.Text = "Loan Number";
            this.LoanNumberRadioButton.UseVisualStyleBackColor = false;
            this.LoanNumberRadioButton.CheckedChanged += new System.EventHandler(this.LoanNumberRadioButton_CheckedChanged);
            // 
            // BuyNumberRadioButton
            // 
            this.BuyNumberRadioButton.AutoSize = true;
            this.BuyNumberRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.BuyNumberRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuyNumberRadioButton.Location = new System.Drawing.Point(93, 159);
            this.BuyNumberRadioButton.Name = "BuyNumberRadioButton";
            this.BuyNumberRadioButton.Size = new System.Drawing.Size(93, 17);
            this.BuyNumberRadioButton.TabIndex = 143;
            this.BuyNumberRadioButton.Text = "Buy Number";
            this.BuyNumberRadioButton.UseVisualStyleBackColor = false;
            this.BuyNumberRadioButton.CheckedChanged += new System.EventHandler(this.LoanNumberRadioButton_CheckedChanged);
            // 
            // LoanStoreText
            // 
            this.LoanStoreText.Enabled = false;
            this.LoanStoreText.Location = new System.Drawing.Point(203, 118);
            this.LoanStoreText.Name = "LoanStoreText";
            this.LoanStoreText.Size = new System.Drawing.Size(68, 20);
            this.LoanStoreText.TabIndex = 144;
            // 
            // BuyStoreText
            // 
            this.BuyStoreText.Enabled = false;
            this.BuyStoreText.Location = new System.Drawing.Point(203, 159);
            this.BuyStoreText.Name = "BuyStoreText";
            this.BuyStoreText.Size = new System.Drawing.Size(68, 20);
            this.BuyStoreText.TabIndex = 145;
            // 
            // buttonContinue
            // 
            this.buttonContinue.BackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonContinue.BackgroundImage")));
            this.buttonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonContinue.CausesValidation = false;
            this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonContinue.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.buttonContinue.ForeColor = System.Drawing.Color.White;
            this.buttonContinue.Location = new System.Drawing.Point(367, 205);
            this.buttonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(100, 50);
            this.buttonContinue.TabIndex = 148;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = false;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.BackColor = System.Drawing.Color.Transparent;
            this.labelErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(111, 81);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(88, 13);
            this.labelErrorMessage.TabIndex = 149;
            this.labelErrorMessage.Text = "Error Message";
            this.labelErrorMessage.Visible = false;
            // 
            // LoanNumberText
            // 
            this.LoanNumberText.AllowOnlyNumbers = true;
            this.LoanNumberText.CausesValidation = false;
            this.LoanNumberText.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.LoanNumberText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanNumberText.Location = new System.Drawing.Point(277, 118);
            this.LoanNumberText.Name = "LoanNumberText";
            this.LoanNumberText.Required = true;
            this.LoanNumberText.Size = new System.Drawing.Size(122, 21);
            this.LoanNumberText.TabIndex = 150;
            this.LoanNumberText.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // BuyNumberText
            // 
            this.BuyNumberText.AllowOnlyNumbers = true;
            this.BuyNumberText.CausesValidation = false;
            this.BuyNumberText.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.BuyNumberText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuyNumberText.Location = new System.Drawing.Point(277, 159);
            this.BuyNumberText.Name = "BuyNumberText";
            this.BuyNumberText.Required = true;
            this.BuyNumberText.Size = new System.Drawing.Size(122, 21);
            this.BuyNumberText.TabIndex = 151;
            this.BuyNumberText.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // LoanBuyLookup
            // 
            this.AcceptButton = this.buttonContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(519, 286);
            this.Controls.Add(this.BuyNumberText);
            this.Controls.Add(this.LoanNumberText);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.BuyStoreText);
            this.Controls.Add(this.LoanStoreText);
            this.Controls.Add(this.BuyNumberRadioButton);
            this.Controls.Add(this.LoanNumberRadioButton);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.LoanBuyLookupFormLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoanBuyLookup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoanBuyLookup";
            this.Load += new System.EventHandler(this.LoanBuyLookup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoanBuyLookupFormLabel;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton LoanNumberRadioButton;
        private System.Windows.Forms.RadioButton BuyNumberRadioButton;
        private System.Windows.Forms.TextBox LoanStoreText;
        private System.Windows.Forms.TextBox BuyStoreText;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Label labelErrorMessage;
        private Common.Libraries.Forms.Components.CustomTextBox LoanNumberText;
        private Common.Libraries.Forms.Components.CustomTextBox BuyNumberText;
    }
}
