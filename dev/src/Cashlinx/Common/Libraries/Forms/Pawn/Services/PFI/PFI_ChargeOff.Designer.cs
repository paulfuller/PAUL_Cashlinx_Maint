using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms.Pawn.Services.PFI
{
    partial class PFI_ChargeOff
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
            this.cancelButton = new CustomButton();
            this.chargeOffButton = new CustomButton();
            this.label3 = new System.Windows.Forms.Label();
            this.chargeCodeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(67, 95);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 35);
            this.cancelButton.TabIndex = 49;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // chargeOffButton
            // 
            this.chargeOffButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chargeOffButton.AutoSize = true;
            this.chargeOffButton.BackColor = System.Drawing.Color.Transparent;
            this.chargeOffButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chargeOffButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.chargeOffButton.FlatAppearance.BorderSize = 0;
            this.chargeOffButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.chargeOffButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.chargeOffButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chargeOffButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chargeOffButton.ForeColor = System.Drawing.Color.White;
            this.chargeOffButton.Location = new System.Drawing.Point(221, 95);
            this.chargeOffButton.Name = "chargeOffButton";
            this.chargeOffButton.Size = new System.Drawing.Size(100, 35);
            this.chargeOffButton.TabIndex = 51;
            this.chargeOffButton.Text = "Charge Off";
            this.chargeOffButton.UseVisualStyleBackColor = false;
            this.chargeOffButton.Click += new System.EventHandler(this.chargeOffButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(151, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 136;
            this.label3.Text = "Charge Off";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chargeCodeComboBox
            // 
            this.chargeCodeComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chargeCodeComboBox.FormattingEnabled = true;
            this.chargeCodeComboBox.Location = new System.Drawing.Point(136, 60);
            this.chargeCodeComboBox.Name = "chargeCodeComboBox";
            this.chargeCodeComboBox.Size = new System.Drawing.Size(185, 21);
            this.chargeCodeComboBox.TabIndex = 137;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(64, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 140;
            this.label4.Text = "Reason: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PFI_ChargeOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(373, 159);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chargeCodeComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chargeOffButton);
            this.Controls.Add(this.cancelButton);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PFI_ChargeOff";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton cancelButton;
        private CustomButton chargeOffButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox chargeCodeComboBox;
        private System.Windows.Forms.Label label4;
    }
}