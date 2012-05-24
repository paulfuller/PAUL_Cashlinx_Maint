namespace Pawn.Forms.Admin
{
    partial class AdminSection
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
            this.userInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.buttonReloadPrintFormats = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.buttonTerminalEmulator = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelBarcode = new System.Windows.Forms.Label();
            this.btnEmployeeBarcodeTag = new System.Windows.Forms.Button();
            this.userInfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(29, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 16);
            this.label3.TabIndex = 138;
            this.label3.Text = "Administration:  Main Menu";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userInfoGroupBox
            // 
            this.userInfoGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.userInfoGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.userInfoGroupBox.Controls.Add(this.LabelBarcode);
            this.userInfoGroupBox.Controls.Add(this.label1);
            this.userInfoGroupBox.Controls.Add(this.buttonTerminalEmulator);
            this.userInfoGroupBox.Controls.Add(this.btnEmployeeBarcodeTag);
            this.userInfoGroupBox.Controls.Add(this.userNameLabel);
            this.userInfoGroupBox.Controls.Add(this.buttonReloadPrintFormats);
            this.userInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userInfoGroupBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userInfoGroupBox.ForeColor = System.Drawing.Color.Black;
            this.userInfoGroupBox.Location = new System.Drawing.Point(57, 111);
            this.userInfoGroupBox.Name = "userInfoGroupBox";
            this.userInfoGroupBox.Size = new System.Drawing.Size(500, 232);
            this.userInfoGroupBox.TabIndex = 148;
            this.userInfoGroupBox.TabStop = false;
            this.userInfoGroupBox.Text = "Admin Online Guider";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(247, 65);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(216, 13);
            this.userNameLabel.TabIndex = 0;
            this.userNameLabel.Text = "Refreshes Thermal Printer Barcode Formats";
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
            this.buttonReloadPrintFormats.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReloadPrintFormats.ForeColor = System.Drawing.Color.White;
            this.buttonReloadPrintFormats.Location = new System.Drawing.Point(17, 47);
            this.buttonReloadPrintFormats.Name = "buttonReloadPrintFormats";
            this.buttonReloadPrintFormats.Size = new System.Drawing.Size(197, 49);
            this.buttonReloadPrintFormats.TabIndex = 146;
            this.buttonReloadPrintFormats.Text = "Reload Print Formats";
            this.buttonReloadPrintFormats.UseVisualStyleBackColor = false;
            this.buttonReloadPrintFormats.Click += new System.EventHandler(this.buttonReloadPrintFormats_Click);
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
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(258, 348);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 40);
            this.closeButton.TabIndex = 149;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // buttonTerminalEmulator
            // 
            this.buttonTerminalEmulator.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonTerminalEmulator.AutoSize = true;
            this.buttonTerminalEmulator.BackColor = System.Drawing.Color.Transparent;
            this.buttonTerminalEmulator.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.buttonTerminalEmulator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonTerminalEmulator.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonTerminalEmulator.FlatAppearance.BorderSize = 0;
            this.buttonTerminalEmulator.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonTerminalEmulator.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonTerminalEmulator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTerminalEmulator.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTerminalEmulator.ForeColor = System.Drawing.Color.White;
            this.buttonTerminalEmulator.Location = new System.Drawing.Point(17, 161);
            this.buttonTerminalEmulator.Name = "buttonTerminalEmulator";
            this.buttonTerminalEmulator.Size = new System.Drawing.Size(197, 49);
            this.buttonTerminalEmulator.TabIndex = 147;
            this.buttonTerminalEmulator.Text = "Terminal Emulator";
            this.buttonTerminalEmulator.UseVisualStyleBackColor = false;
            this.buttonTerminalEmulator.Click += new System.EventHandler(this.buttonTerminalEmulator_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 13);
            this.label1.TabIndex = 148;
            this.label1.Text = "Opens up a Terminal Emulation Screen.";
            // 
            // LabelBarcode
            // 
            this.LabelBarcode.AutoSize = true;
            this.LabelBarcode.Location = new System.Drawing.Point(247, 120);
            this.LabelBarcode.Name = "LabelBarcode";
            this.LabelBarcode.Size = new System.Drawing.Size(148, 13);
            this.LabelBarcode.TabIndex = 150;
            this.LabelBarcode.Text = "Prints out Employee Barcode.";
            // 
            // btnEmployeeBarcodeTag
            // 
            this.btnEmployeeBarcodeTag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEmployeeBarcodeTag.AutoSize = true;
            this.btnEmployeeBarcodeTag.BackColor = System.Drawing.Color.Transparent;
            this.btnEmployeeBarcodeTag.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnEmployeeBarcodeTag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEmployeeBarcodeTag.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEmployeeBarcodeTag.FlatAppearance.BorderSize = 0;
            this.btnEmployeeBarcodeTag.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEmployeeBarcodeTag.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEmployeeBarcodeTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmployeeBarcodeTag.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployeeBarcodeTag.ForeColor = System.Drawing.Color.White;
            this.btnEmployeeBarcodeTag.Location = new System.Drawing.Point(17, 102);
            this.btnEmployeeBarcodeTag.Name = "btnEmployeeBarcodeTag";
            this.btnEmployeeBarcodeTag.Size = new System.Drawing.Size(197, 49);
            this.btnEmployeeBarcodeTag.TabIndex = 149;
            this.btnEmployeeBarcodeTag.Text = "Employee Barcode Tag";
            this.btnEmployeeBarcodeTag.UseVisualStyleBackColor = false;
            this.btnEmployeeBarcodeTag.Click += new System.EventHandler(this.btnEmployeeBarcodeTag_Click);
            // 
            // AdminSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(623, 400);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.userInfoGroupBox);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminSection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminSection";
            this.userInfoGroupBox.ResumeLayout(false);
            this.userInfoGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox userInfoGroupBox;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Button buttonReloadPrintFormats;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button buttonTerminalEmulator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelBarcode;
        private System.Windows.Forms.Button btnEmployeeBarcodeTag;
    }
}