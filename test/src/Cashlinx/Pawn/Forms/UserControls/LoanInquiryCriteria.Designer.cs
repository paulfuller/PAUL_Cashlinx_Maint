using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls
{
    partial class LoanInquiryCriteria
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanInquiryCriteria));
            this.loaninquiryDataTypeCombobox = new System.Windows.Forms.ComboBox();
            this.loaninquiryDataNameCombobox = new System.Windows.Forms.ComboBox();
            this.loaninquirySearchTypeCombobox = new System.Windows.Forms.ComboBox();
            this.loaninquiryXLabel = new System.Windows.Forms.Label();
            this.loaninquiryCustomButtonAnd = new CustomButtonTiny();
            this.SuspendLayout();
            // 
            // loaninquiryDataTypeCombobox
            // 
            this.loaninquiryDataTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loaninquiryDataTypeCombobox.FormattingEnabled = true;
            this.loaninquiryDataTypeCombobox.IntegralHeight = false;
            this.loaninquiryDataTypeCombobox.Location = new System.Drawing.Point(31, 10);
            this.loaninquiryDataTypeCombobox.Name = "loaninquiryDataTypeCombobox";
            this.loaninquiryDataTypeCombobox.Size = new System.Drawing.Size(121, 21);
            this.loaninquiryDataTypeCombobox.TabIndex = 1;
            this.loaninquiryDataTypeCombobox.SelectedIndexChanged += new System.EventHandler(this.loaninquiryDataTypeCombobox_SelectedIndexChanged);
            // 
            // loaninquiryDataNameCombobox
            // 
            this.loaninquiryDataNameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loaninquiryDataNameCombobox.FormattingEnabled = true;
            this.loaninquiryDataNameCombobox.IntegralHeight = false;
            this.loaninquiryDataNameCombobox.Location = new System.Drawing.Point(180, 10);
            this.loaninquiryDataNameCombobox.Name = "loaninquiryDataNameCombobox";
            this.loaninquiryDataNameCombobox.Size = new System.Drawing.Size(147, 21);
            this.loaninquiryDataNameCombobox.TabIndex = 2;
            this.loaninquiryDataNameCombobox.SelectedIndexChanged += new System.EventHandler(this.loaninquiryDataNameCombobox_SelectedIndexChanged);
            // 
            // loaninquirySearchTypeCombobox
            // 
            this.loaninquirySearchTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loaninquirySearchTypeCombobox.FormattingEnabled = true;
            this.loaninquirySearchTypeCombobox.IntegralHeight = false;
            this.loaninquirySearchTypeCombobox.Location = new System.Drawing.Point(347, 10);
            this.loaninquirySearchTypeCombobox.Name = "loaninquirySearchTypeCombobox";
            this.loaninquirySearchTypeCombobox.Size = new System.Drawing.Size(122, 21);
            this.loaninquirySearchTypeCombobox.TabIndex = 3;
            // 
            // loaninquiryXLabel
            // 
            this.loaninquiryXLabel.AutoSize = true;
            this.loaninquiryXLabel.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loaninquiryXLabel.ForeColor = System.Drawing.Color.Red;
            this.loaninquiryXLabel.Location = new System.Drawing.Point(-1, 6);
            this.loaninquiryXLabel.Name = "loaninquiryXLabel";
            this.loaninquiryXLabel.Size = new System.Drawing.Size(27, 29);
            this.loaninquiryXLabel.TabIndex = 5;
            this.loaninquiryXLabel.Text = "X";
            this.loaninquiryXLabel.Click += new System.EventHandler(this.loaninquiryXLabel_Click);
            // 
            // loaninquiryCustomButtonAnd
            // 
            this.loaninquiryCustomButtonAnd.BackColor = System.Drawing.Color.Transparent;
            this.loaninquiryCustomButtonAnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loaninquiryCustomButtonAnd.BackgroundImage")));
            this.loaninquiryCustomButtonAnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loaninquiryCustomButtonAnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loaninquiryCustomButtonAnd.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.loaninquiryCustomButtonAnd.FlatAppearance.BorderSize = 0;
            this.loaninquiryCustomButtonAnd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.loaninquiryCustomButtonAnd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.loaninquiryCustomButtonAnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loaninquiryCustomButtonAnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loaninquiryCustomButtonAnd.ForeColor = System.Drawing.Color.White;
            this.loaninquiryCustomButtonAnd.Location = new System.Drawing.Point(824, 5);
            this.loaninquiryCustomButtonAnd.Margin = new System.Windows.Forms.Padding(0);
            this.loaninquiryCustomButtonAnd.MaximumSize = new System.Drawing.Size(75, 35);
            this.loaninquiryCustomButtonAnd.MinimumSize = new System.Drawing.Size(75, 35);
            this.loaninquiryCustomButtonAnd.Name = "loaninquiryCustomButtonAnd";
            this.loaninquiryCustomButtonAnd.Size = new System.Drawing.Size(75, 35);
            this.loaninquiryCustomButtonAnd.TabIndex = 6;
            this.loaninquiryCustomButtonAnd.Text = "Add   ";
            this.loaninquiryCustomButtonAnd.UseVisualStyleBackColor = false;
            this.loaninquiryCustomButtonAnd.Click += new System.EventHandler(this.loaninquiryCustomButtonAnd_Click);
            // 
            // LoanInquiryCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.loaninquiryCustomButtonAnd);
            this.Controls.Add(this.loaninquiryXLabel);
            this.Controls.Add(this.loaninquirySearchTypeCombobox);
            this.Controls.Add(this.loaninquiryDataNameCombobox);
            this.Controls.Add(this.loaninquiryDataTypeCombobox);
            this.Name = "LoanInquiryCriteria";
            this.Size = new System.Drawing.Size(899, 37);
            this.Load += new System.EventHandler(this.LoanInquiryCriteria_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox loaninquiryDataTypeCombobox;
        private System.Windows.Forms.ComboBox loaninquiryDataNameCombobox;
        private System.Windows.Forms.ComboBox loaninquirySearchTypeCombobox;
        private System.Windows.Forms.Label loaninquiryXLabel;
        private CustomButtonTiny loaninquiryCustomButtonAnd;

    }
}
