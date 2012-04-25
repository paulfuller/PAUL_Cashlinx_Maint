using Common.Libraries.Forms.Components;

namespace Pawn.Forms
{
    partial class InfoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoDialog));
            this.customLabelMessage = new CustomLabel();
            this.customButtonClose = new CustomButton();
            this.customHeaderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // customLabelMessage
            // 
            this.customLabelMessage.BackColor = System.Drawing.Color.Transparent;
            this.customLabelMessage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMessage.Location = new System.Drawing.Point(70, 83);
            this.customLabelMessage.Name = "customLabelMessage";
            this.customLabelMessage.Size = new System.Drawing.Size(369, 121);
            this.customLabelMessage.TabIndex = 0;
            this.customLabelMessage.Text = "Message";
            // 
            // customButtonClose
            // 
            this.customButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonClose.BackgroundImage")));
            this.customButtonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClose.FlatAppearance.BorderSize = 0;
            this.customButtonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClose.ForeColor = System.Drawing.Color.White;
            this.customButtonClose.Location = new System.Drawing.Point(218, 244);
            this.customButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClose.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.Name = "customButtonClose";
            this.customButtonClose.Size = new System.Drawing.Size(100, 50);
            this.customButtonClose.TabIndex = 1;
            this.customButtonClose.Text = "&Close";
            this.customButtonClose.UseVisualStyleBackColor = false;
            this.customButtonClose.Click += new System.EventHandler(this.customButtonClose_Click);
            // 
            // customHeaderLabel
            // 
            this.customHeaderLabel.AutoSize = true;
            this.customHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.customHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.customHeaderLabel.Location = new System.Drawing.Point(190, 29);
            this.customHeaderLabel.Name = "customHeaderLabel";
            this.customHeaderLabel.Size = new System.Drawing.Size(157, 19);
            this.customHeaderLabel.TabIndex = 2;
            this.customHeaderLabel.Text = "Information Message";
            // 
            // InfoDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(536, 320);
            this.Controls.Add(this.customHeaderLabel);
            this.Controls.Add(this.customButtonClose);
            this.Controls.Add(this.customLabelMessage);
            this.Name = "InfoDialog";
            this.Text = "InfoDialog";
            this.Load += new System.EventHandler(this.InfoDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel customLabelMessage;
        private CustomButton customButtonClose;
        private System.Windows.Forms.Label customHeaderLabel;
    }
}
