using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Tender
{
    partial class ManualTicketNumber
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
            this.manualTicketNumberLabel = new System.Windows.Forms.Label();
            this.continueButton = new CustomButton();
            this.cancelButton = new CustomButton();
            this.SuspendLayout();
            // 
            // manualTicketNumberLabel
            // 
            this.manualTicketNumberLabel.AutoSize = true;
            this.manualTicketNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.manualTicketNumberLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualTicketNumberLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.manualTicketNumberLabel.Location = new System.Drawing.Point(172, 24);
            this.manualTicketNumberLabel.Name = "manualTicketNumberLabel";
            this.manualTicketNumberLabel.Size = new System.Drawing.Size(168, 19);
            this.manualTicketNumberLabel.TabIndex = 2;
            this.manualTicketNumberLabel.Text = "Manual Ticket Number";
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.continueButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(403, 197);
            this.continueButton.Margin = new System.Windows.Forms.Padding(0);
            this.continueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 50);
            this.continueButton.TabIndex = 11;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(9, 197);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // ManualTicketNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(512, 256);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.manualTicketNumberLabel);
            this.Name = "ManualTicketNumber";
            this.Text = "ManualTicketNumber";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label manualTicketNumberLabel;
        private CustomButton continueButton;
        private CustomButton cancelButton;
    }
}