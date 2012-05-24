namespace Pawn.Forms.UserControls
{
    partial class PickSlipPrintControl
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
            this.itemNumberLabel = new System.Windows.Forms.Label();
            this.itemDescriptionLabel = new System.Windows.Forms.Label();
            this.itemLocationAisleLabel = new System.Windows.Forms.Label();
            this.itemLocationShelfLabel = new System.Windows.Forms.Label();
            this.itemLocationOtherLabel = new System.Windows.Forms.Label();
            this.gunNumberLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // itemNumberLabel
            // 
            this.itemNumberLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemNumberLabel.Location = new System.Drawing.Point(0, 0);
            this.itemNumberLabel.Name = "itemNumberLabel";
            this.itemNumberLabel.Size = new System.Drawing.Size(39, 25);
            this.itemNumberLabel.TabIndex = 47;
            this.itemNumberLabel.Text = "0";
            // 
            // itemDescriptionLabel
            // 
            this.itemDescriptionLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemDescriptionLabel.Location = new System.Drawing.Point(41, 0);
            this.itemDescriptionLabel.Name = "itemDescriptionLabel";
            this.itemDescriptionLabel.Size = new System.Drawing.Size(430, 40);
            this.itemDescriptionLabel.TabIndex = 48;
            this.itemDescriptionLabel.Text = "0";
            // 
            // itemLocationAisleLabel
            // 
            this.itemLocationAisleLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemLocationAisleLabel.Location = new System.Drawing.Point(549, 0);
            this.itemLocationAisleLabel.Name = "itemLocationAisleLabel";
            this.itemLocationAisleLabel.Size = new System.Drawing.Size(65, 40);
            this.itemLocationAisleLabel.TabIndex = 49;
            this.itemLocationAisleLabel.Text = "0";
            // 
            // itemLocationShelfLabel
            // 
            this.itemLocationShelfLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemLocationShelfLabel.Location = new System.Drawing.Point(617, 0);
            this.itemLocationShelfLabel.Name = "itemLocationShelfLabel";
            this.itemLocationShelfLabel.Size = new System.Drawing.Size(80, 40);
            this.itemLocationShelfLabel.TabIndex = 50;
            this.itemLocationShelfLabel.Tag = "0";
            this.itemLocationShelfLabel.Text = "0";
            // 
            // itemLocationOtherLabel
            // 
            this.itemLocationOtherLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemLocationOtherLabel.Location = new System.Drawing.Point(696, 0);
            this.itemLocationOtherLabel.Name = "itemLocationOtherLabel";
            this.itemLocationOtherLabel.Size = new System.Drawing.Size(75, 40);
            this.itemLocationOtherLabel.TabIndex = 51;
            this.itemLocationOtherLabel.Tag = "0";
            this.itemLocationOtherLabel.Text = "0";
            // 
            // gunNumberLabel
            // 
            this.gunNumberLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunNumberLabel.Location = new System.Drawing.Point(471, 0);
            this.gunNumberLabel.Name = "gunNumberLabel";
            this.gunNumberLabel.Size = new System.Drawing.Size(71, 40);
            this.gunNumberLabel.TabIndex = 52;
            this.gunNumberLabel.Text = "0";
            // 
            // PickSlipPrintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gunNumberLabel);
            this.Controls.Add(this.itemLocationOtherLabel);
            this.Controls.Add(this.itemLocationShelfLabel);
            this.Controls.Add(this.itemLocationAisleLabel);
            this.Controls.Add(this.itemDescriptionLabel);
            this.Controls.Add(this.itemNumberLabel);
            this.Name = "PickSlipPrintControl";
            this.Size = new System.Drawing.Size(796, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label itemNumberLabel;
        private System.Windows.Forms.Label itemDescriptionLabel;
        private System.Windows.Forms.Label itemLocationAisleLabel;
        private System.Windows.Forms.Label itemLocationShelfLabel;
        private System.Windows.Forms.Label itemLocationOtherLabel;
        private System.Windows.Forms.Label gunNumberLabel;
    }
}
