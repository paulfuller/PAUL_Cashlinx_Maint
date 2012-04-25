namespace Pawn.Forms.UserControls
{
    partial class TransferOutCount
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
            this.txtBeginCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProcessedCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemainingCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBeginCount
            // 
            this.txtBeginCount.Location = new System.Drawing.Point(63, 32);
            this.txtBeginCount.Name = "txtBeginCount";
            this.txtBeginCount.Size = new System.Drawing.Size(48, 20);
            this.txtBeginCount.TabIndex = 164;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 165;
            this.label1.Text = "Begin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 167;
            this.label2.Text = "Processed";
            // 
            // txtProcessedCount
            // 
            this.txtProcessedCount.Location = new System.Drawing.Point(63, 58);
            this.txtProcessedCount.Name = "txtProcessedCount";
            this.txtProcessedCount.Size = new System.Drawing.Size(48, 20);
            this.txtProcessedCount.TabIndex = 166;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 169;
            this.label3.Text = "Remaining";
            // 
            // txtRemainingCount
            // 
            this.txtRemainingCount.Location = new System.Drawing.Point(63, 84);
            this.txtRemainingCount.Name = "txtRemainingCount";
            this.txtRemainingCount.Size = new System.Drawing.Size(48, 20);
            this.txtRemainingCount.TabIndex = 168;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 17);
            this.label10.TabIndex = 170;
            this.label10.Text = "Scrap:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TransferOutCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRemainingCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProcessedCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBeginCount);
            this.Name = "TransferOutCount";
            this.Size = new System.Drawing.Size(121, 110);
            this.Load += new System.EventHandler(this.TransferOutCount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBeginCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProcessedCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRemainingCount;
        private System.Windows.Forms.Label label10;
    }
}
