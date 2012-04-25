namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    partial class JewelryDisplay
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
            this.jewelryPictureBox = new System.Windows.Forms.PictureBox();
            this.jewelryNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.jewelryPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // jewelryPictureBox
            // 
            this.jewelryPictureBox.ErrorImage = null;
            this.jewelryPictureBox.InitialImage = null;
            this.jewelryPictureBox.Location = new System.Drawing.Point(15, 53);
            this.jewelryPictureBox.MaximumSize = new System.Drawing.Size(250, 250);
            this.jewelryPictureBox.MinimumSize = new System.Drawing.Size(250, 250);
            this.jewelryPictureBox.Name = "jewelryPictureBox";
            this.jewelryPictureBox.Size = new System.Drawing.Size(250, 250);
            this.jewelryPictureBox.TabIndex = 1;
            this.jewelryPictureBox.TabStop = false;
            // 
            // jewelryNameLabel
            // 
            this.jewelryNameLabel.AutoSize = true;
            this.jewelryNameLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jewelryNameLabel.ForeColor = System.Drawing.Color.White;
            this.jewelryNameLabel.Location = new System.Drawing.Point(15, 12);
            this.jewelryNameLabel.MaximumSize = new System.Drawing.Size(250, 25);
            this.jewelryNameLabel.MinimumSize = new System.Drawing.Size(250, 25);
            this.jewelryNameLabel.Name = "jewelryNameLabel";
            this.jewelryNameLabel.Size = new System.Drawing.Size(250, 25);
            this.jewelryNameLabel.TabIndex = 2;
            this.jewelryNameLabel.Text = "Jewelry Display";
            this.jewelryNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JewelryDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(280, 320);
            this.ControlBox = false;
            this.Controls.Add(this.jewelryNameLabel);
            this.Controls.Add(this.jewelryPictureBox);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JewelryDisplay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Jewelry Name";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.jewelryPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox jewelryPictureBox;
        private System.Windows.Forms.Label jewelryNameLabel;
    }
}