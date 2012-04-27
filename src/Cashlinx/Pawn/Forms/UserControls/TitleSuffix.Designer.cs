namespace Pawn.Forms.UserControls
{
    partial class TitleSuffix
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
            this.custTitleSuffixList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // custTitleSuffixList
            // 
            this.custTitleSuffixList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custTitleSuffixList.FormattingEnabled = true;
            this.custTitleSuffixList.Location = new System.Drawing.Point(0, 2);
            this.custTitleSuffixList.Name = "custTitleSuffixList";
            this.custTitleSuffixList.Size = new System.Drawing.Size(54, 21);
            this.custTitleSuffixList.TabIndex = 0;
            // 
            // TitleSuffix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.custTitleSuffixList);
            this.Name = "TitleSuffix";
            this.Size = new System.Drawing.Size(55, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox custTitleSuffixList;
    }
}
