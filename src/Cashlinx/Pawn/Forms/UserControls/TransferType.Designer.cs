namespace Pawn.Forms.UserControls
{
    partial class TransferType
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
            this.transferList = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.labelAsterisk6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // transferList
            // 
            this.transferList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.transferList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transferList.FormattingEnabled = true;
            this.transferList.Location = new System.Drawing.Point(0, 21);
            this.transferList.Name = "transferList";
            this.transferList.Size = new System.Drawing.Size(166, 21);
            this.transferList.TabIndex = 0;
            this.transferList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.list_DrawItem);
            this.transferList.SelectedIndexChanged += new System.EventHandler(this.transferList_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(-3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(173, 17);
            this.label10.TabIndex = 163;
            this.label10.Text = "CATCO Transfer Type:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAsterisk6
            // 
            this.labelAsterisk6.AutoSize = true;
            this.labelAsterisk6.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk6.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk6.Location = new System.Drawing.Point(169, 5);
            this.labelAsterisk6.Name = "labelAsterisk6";
            this.labelAsterisk6.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk6.TabIndex = 164;
            this.labelAsterisk6.Text = "*";
            // 
            // TransferType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelAsterisk6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.transferList);
            this.Name = "TransferType";
            this.Size = new System.Drawing.Size(191, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox transferList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelAsterisk6;
    }
}
