namespace Pawn.Forms.UserControls
{
    partial class ShopsTreenode
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
            this.NodePictureBox = new System.Windows.Forms.PictureBox();
            this.NodeLabel = new System.Windows.Forms.Label();
            this.NodeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NodePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NodePictureBox
            // 
            this.NodePictureBox.Location = new System.Drawing.Point(7, 2);
            this.NodePictureBox.Name = "NodePictureBox";
            this.NodePictureBox.Size = new System.Drawing.Size(25, 17);
            this.NodePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.NodePictureBox.TabIndex = 3;
            this.NodePictureBox.TabStop = false;
            this.NodePictureBox.Click += new System.EventHandler(this.NodePictureBox_Click);
            // 
            // NodeLabel
            // 
            this.NodeLabel.AutoSize = true;
            this.NodeLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.NodeLabel.Location = new System.Drawing.Point(76, 4);
            this.NodeLabel.Name = "NodeLabel";
            this.NodeLabel.Size = new System.Drawing.Size(70, 14);
            this.NodeLabel.TabIndex = 8;
            this.NodeLabel.Text = "NodeLabel";
            // 
            // NodeCheckBox
            // 
            this.NodeCheckBox.AutoSize = true;
            this.NodeCheckBox.Font = new System.Drawing.Font("Tahoma", 9F);
            this.NodeCheckBox.Location = new System.Drawing.Point(55, 5);
            this.NodeCheckBox.Name = "NodeCheckBox";
            this.NodeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.NodeCheckBox.TabIndex = 9;
            this.NodeCheckBox.UseVisualStyleBackColor = true;
            this.NodeCheckBox.Click += new System.EventHandler(this.NodeCheckBox_Click);
            // 
            // LocationsTreenode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NodeCheckBox);
            this.Controls.Add(this.NodeLabel);
            this.Controls.Add(this.NodePictureBox);
            this.Name = "LocationsTreenode";
            this.Size = new System.Drawing.Size(243, 23);
            ((System.ComponentModel.ISupportInitialize)(this.NodePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox NodePictureBox;
        private System.Windows.Forms.Label NodeLabel;
        public System.Windows.Forms.CheckBox NodeCheckBox;
    }
}
