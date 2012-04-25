namespace PawnStoreSetupTool
{
    partial class InProgressForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.processingMessageLabel = new System.Windows.Forms.Label();
            this.borderBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.borderBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(251, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Processing";
            // 
            // processingMessageLabel
            // 
            this.processingMessageLabel.AutoSize = true;
            this.processingMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.processingMessageLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processingMessageLabel.ForeColor = System.Drawing.Color.Black;
            this.processingMessageLabel.Location = new System.Drawing.Point(240, 63);
            this.processingMessageLabel.Name = "processingMessageLabel";
            this.processingMessageLabel.Size = new System.Drawing.Size(114, 14);
            this.processingMessageLabel.TabIndex = 1;
            this.processingMessageLabel.Text = "Processing Message";
            this.processingMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // borderBox
            // 
            this.borderBox.BackColor = System.Drawing.Color.MediumBlue;
            this.borderBox.Location = new System.Drawing.Point(9, 35);
            this.borderBox.MaximumSize = new System.Drawing.Size(582, 2);
            this.borderBox.MinimumSize = new System.Drawing.Size(582, 2);
            this.borderBox.Name = "borderBox";
            this.borderBox.Size = new System.Drawing.Size(582, 2);
            this.borderBox.TabIndex = 2;
            this.borderBox.TabStop = false;
            // 
            // InProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::PawnStoreSetupTool.Properties.Resources.dark_grey_panel;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(594, 114);
            this.ControlBox = false;
            this.Controls.Add(this.borderBox);
            this.Controls.Add(this.processingMessageLabel);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(600, 120);
            this.MinimumSize = new System.Drawing.Size(600, 120);
            this.Name = "InProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.InProgressForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.borderBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label processingMessageLabel;
        private System.Windows.Forms.PictureBox borderBox;
    }
}