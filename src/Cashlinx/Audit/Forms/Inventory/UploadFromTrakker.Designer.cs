namespace Audit.Forms.Inventory
{
    partial class UploadFromTrakker
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
            this.SuspendLayout();
            // 
            // btnContinue
            // 
            this.btnContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnAdditionalTracker
            // 
            this.btnAdditionalTracker.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAdditionalTracker.FlatAppearance.BorderSize = 0;
            this.btnAdditionalTracker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdditionalTracker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdditionalTracker.Click += new System.EventHandler(this.btnAdditionalTracker_Click);
            // 
            // UploadFromTrakker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(585, 360);
            this.Mode = Audit.Logic.TransferMode.UploadFromTrakker;
            this.Name = "UploadFromTrakker";
            this.Text = "UploadFromTrakker";
            this.Load += new System.EventHandler(this.UploadFromTrakker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}