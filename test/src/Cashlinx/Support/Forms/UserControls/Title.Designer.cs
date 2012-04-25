namespace Support.Forms.UserControls
{
    partial class Title
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
            this.custTitleList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // custTitleList
            // 
            this.custTitleList.CausesValidation = false;
            this.custTitleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custTitleList.FormattingEnabled = true;
            this.custTitleList.IntegralHeight = false;
            this.custTitleList.Location = new System.Drawing.Point(0, 0);
            this.custTitleList.Name = "custTitleList";
            this.custTitleList.Size = new System.Drawing.Size(60, 21);
            this.custTitleList.TabIndex = 0;
            // 
            // Title
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.custTitleList);
            this.DoubleBuffered = true;
            this.Name = "Title";
            this.Size = new System.Drawing.Size(61, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox custTitleList;
    }
}
