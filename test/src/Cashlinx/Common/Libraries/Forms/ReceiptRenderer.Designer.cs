namespace Common.Libraries.Forms
{
    partial class ReceiptRenderer
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
            this.rendererControl = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rendererControl
            // 
            this.rendererControl.AcceptsTab = true;
            this.rendererControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rendererControl.Location = new System.Drawing.Point(0, 0);
            this.rendererControl.Name = "rendererControl";
            this.rendererControl.ReadOnly = true;
            this.rendererControl.Size = new System.Drawing.Size(460, 574);
            this.rendererControl.TabIndex = 2;
            this.rendererControl.Text = "test";
            // 
            // ReceiptRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 574);
            this.Controls.Add(this.rendererControl);
            this.Name = "ReceiptRenderer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt View";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rendererControl;


    }
}