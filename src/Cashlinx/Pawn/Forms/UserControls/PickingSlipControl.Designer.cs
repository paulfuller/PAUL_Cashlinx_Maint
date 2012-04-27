namespace Pawn.Forms.UserControls
{
    partial class PickingSlipControl
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
            this.itemLocationShelfLabel = new System.Windows.Forms.Label();
            this.itemLocationAisleLabel = new System.Windows.Forms.Label();
            this.itemAmountLabel = new System.Windows.Forms.Label();
            this.itemLocationLabel = new System.Windows.Forms.Label();
            this.itemDescriptionLabel = new System.Windows.Forms.Label();
            this.itemNumberLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // itemLocationShelfLabel
            // 
            this.itemLocationShelfLabel.AutoSize = true;
            this.itemLocationShelfLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemLocationShelfLabel.Location = new System.Drawing.Point(481, 35);
            this.itemLocationShelfLabel.Name = "itemLocationShelfLabel";
            this.itemLocationShelfLabel.Size = new System.Drawing.Size(16, 16);
            this.itemLocationShelfLabel.TabIndex = 45;
            this.itemLocationShelfLabel.Tag = "0";
            this.itemLocationShelfLabel.Text = "0";
            this.itemLocationShelfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // itemLocationAisleLabel
            // 
            this.itemLocationAisleLabel.AutoSize = true;
            this.itemLocationAisleLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemLocationAisleLabel.Location = new System.Drawing.Point(481, 19);
            this.itemLocationAisleLabel.Name = "itemLocationAisleLabel";
            this.itemLocationAisleLabel.Size = new System.Drawing.Size(16, 16);
            this.itemLocationAisleLabel.TabIndex = 44;
            this.itemLocationAisleLabel.Text = "0";
            this.itemLocationAisleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // itemAmountLabel
            // 
            this.itemAmountLabel.AutoSize = true;
            this.itemAmountLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemAmountLabel.Location = new System.Drawing.Point(603, 3);
            this.itemAmountLabel.Name = "itemAmountLabel";
            this.itemAmountLabel.Size = new System.Drawing.Size(16, 16);
            this.itemAmountLabel.TabIndex = 43;
            this.itemAmountLabel.Text = "0";
            this.itemAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // itemLocationLabel
            // 
            this.itemLocationLabel.AutoSize = true;
            this.itemLocationLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemLocationLabel.Location = new System.Drawing.Point(481, 3);
            this.itemLocationLabel.Name = "itemLocationLabel";
            this.itemLocationLabel.Size = new System.Drawing.Size(16, 16);
            this.itemLocationLabel.TabIndex = 42;
            this.itemLocationLabel.Text = "0";
            this.itemLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // itemDescriptionLabel
            // 
            this.itemDescriptionLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemDescriptionLabel.Location = new System.Drawing.Point(30, 0);
            this.itemDescriptionLabel.Name = "itemDescriptionLabel";
            this.itemDescriptionLabel.Size = new System.Drawing.Size(448, 70);
            this.itemDescriptionLabel.TabIndex = 41;
            this.itemDescriptionLabel.Text = "0";
            // 
            // itemNumberLabel
            // 
            this.itemNumberLabel.AutoSize = true;
            this.itemNumberLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemNumberLabel.Location = new System.Drawing.Point(0, 0);
            this.itemNumberLabel.Name = "itemNumberLabel";
            this.itemNumberLabel.Size = new System.Drawing.Size(16, 16);
            this.itemNumberLabel.TabIndex = 46;
            this.itemNumberLabel.Text = "0";
            this.itemNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PickingSlipControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.itemNumberLabel);
            this.Controls.Add(this.itemLocationShelfLabel);
            this.Controls.Add(this.itemLocationAisleLabel);
            this.Controls.Add(this.itemAmountLabel);
            this.Controls.Add(this.itemLocationLabel);
            this.Controls.Add(this.itemDescriptionLabel);
            this.Name = "PickingSlipControl";
            this.Size = new System.Drawing.Size(680, 70);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label itemLocationShelfLabel;
        private System.Windows.Forms.Label itemLocationAisleLabel;
        private System.Windows.Forms.Label itemAmountLabel;
        private System.Windows.Forms.Label itemLocationLabel;
        private System.Windows.Forms.Label itemDescriptionLabel;
        private System.Windows.Forms.Label itemNumberLabel;
    }
}
