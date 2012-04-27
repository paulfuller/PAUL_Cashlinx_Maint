namespace Pawn.Forms.Admin
{
    partial class Resets
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
            this.label3 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.buttonReloadPrintFormats = new System.Windows.Forms.Button();
            this.userInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.userInfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(193, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 19);
            this.label3.TabIndex = 137;
            this.label3.Text = "Thermal Barcode Reset";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.closeButton.AutoSize = true;
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(229, 231);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 50);
            this.closeButton.TabIndex = 145;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // buttonReloadPrintFormats
            // 
            this.buttonReloadPrintFormats.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReloadPrintFormats.AutoSize = true;
            this.buttonReloadPrintFormats.BackColor = System.Drawing.Color.Transparent;
            this.buttonReloadPrintFormats.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.buttonReloadPrintFormats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonReloadPrintFormats.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonReloadPrintFormats.FlatAppearance.BorderSize = 0;
            this.buttonReloadPrintFormats.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonReloadPrintFormats.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonReloadPrintFormats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReloadPrintFormats.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReloadPrintFormats.ForeColor = System.Drawing.Color.White;
            this.buttonReloadPrintFormats.Location = new System.Drawing.Point(16, 27);
            this.buttonReloadPrintFormats.Name = "buttonReloadPrintFormats";
            this.buttonReloadPrintFormats.Size = new System.Drawing.Size(100, 50);
            this.buttonReloadPrintFormats.TabIndex = 146;
            this.buttonReloadPrintFormats.Text = "Reload \r\nPrinter";
            this.buttonReloadPrintFormats.UseVisualStyleBackColor = false;
            this.buttonReloadPrintFormats.Click += new System.EventHandler(this.buttonReloadPrintFormats_Click);
            // 
            // userInfoGroupBox
            // 
            this.userInfoGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.userInfoGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.userInfoGroupBox.Controls.Add(this.userNameLabel);
            this.userInfoGroupBox.Controls.Add(this.buttonReloadPrintFormats);
            this.userInfoGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userInfoGroupBox.ForeColor = System.Drawing.Color.Black;
            this.userInfoGroupBox.Location = new System.Drawing.Point(15, 58);
            this.userInfoGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.userInfoGroupBox.Name = "userInfoGroupBox";
            this.userInfoGroupBox.Padding = new System.Windows.Forms.Padding(0);
            this.userInfoGroupBox.Size = new System.Drawing.Size(532, 164);
            this.userInfoGroupBox.TabIndex = 3;
            this.userInfoGroupBox.TabStop = false;
            this.userInfoGroupBox.Text = "Options";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLabel.Location = new System.Drawing.Point(122, 44);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(197, 16);
            this.userNameLabel.TabIndex = 0;
            this.userNameLabel.Text = "Refresh Thermal Printer Formats";
            // 
            // Resets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(559, 288);
            this.ControlBox = false;
            this.Controls.Add(this.userInfoGroupBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Resets";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.userInfoGroupBox.ResumeLayout(false);
            this.userInfoGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button buttonReloadPrintFormats;
        private System.Windows.Forms.GroupBox userInfoGroupBox;
        private System.Windows.Forms.Label userNameLabel;
    }
}