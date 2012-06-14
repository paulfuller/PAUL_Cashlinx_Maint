namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    partial class FindMerchandise
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
            this.manufacturerLabel = new System.Windows.Forms.Label();
            this.modelLabel = new System.Windows.Forms.Label();
            this.manufacturerTextBox = new System.Windows.Forms.TextBox();
            this.modelTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.textEntryErrorLabel = new System.Windows.Forms.Label();
            this.suggestedManufacturerListBox = new System.Windows.Forms.ListBox();
            this.suggestedManufacturerLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.manualEntryButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // manufacturerLabel
            // 
            this.manufacturerLabel.AutoSize = true;
            this.manufacturerLabel.BackColor = System.Drawing.Color.Transparent;
            this.manufacturerLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manufacturerLabel.ForeColor = System.Drawing.Color.Black;
            this.manufacturerLabel.Location = new System.Drawing.Point(42, 65);
            this.manufacturerLabel.Name = "manufacturerLabel";
            this.manufacturerLabel.Size = new System.Drawing.Size(72, 13);
            this.manufacturerLabel.TabIndex = 0;
            this.manufacturerLabel.Text = "Manufacturer";
            // 
            // modelLabel
            // 
            this.modelLabel.AutoSize = true;
            this.modelLabel.BackColor = System.Drawing.Color.Transparent;
            this.modelLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelLabel.ForeColor = System.Drawing.Color.Black;
            this.modelLabel.Location = new System.Drawing.Point(79, 102);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(35, 13);
            this.modelLabel.TabIndex = 1;
            this.modelLabel.Text = "Model";
            // 
            // manufacturerTextBox
            // 
            this.manufacturerTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.manufacturerTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.manufacturerTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.manufacturerTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manufacturerTextBox.Location = new System.Drawing.Point(120, 62);
            this.manufacturerTextBox.Name = "manufacturerTextBox";
            this.manufacturerTextBox.Size = new System.Drawing.Size(202, 21);
            this.manufacturerTextBox.TabIndex = 1;
            this.manufacturerTextBox.TextChanged += new System.EventHandler(this.manufacturerTextBox_TextChanged);
            // 
            // modelTextBox
            // 
            this.modelTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.modelTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelTextBox.Location = new System.Drawing.Point(120, 99);
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.Size = new System.Drawing.Size(202, 21);
            this.modelTextBox.TabIndex = 3;
            this.modelTextBox.TextChanged += new System.EventHandler(this.modelTextBox_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.searchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.searchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.searchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.searchButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(232, 149);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(90, 30);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // textEntryErrorLabel
            // 
            this.textEntryErrorLabel.AutoSize = true;
            this.textEntryErrorLabel.BackColor = System.Drawing.Color.Transparent;
            this.textEntryErrorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEntryErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.textEntryErrorLabel.Location = new System.Drawing.Point(55, 211);
            this.textEntryErrorLabel.Name = "textEntryErrorLabel";
            this.textEntryErrorLabel.Size = new System.Drawing.Size(267, 13);
            this.textEntryErrorLabel.TabIndex = 5;
            this.textEntryErrorLabel.Text = "Please enter data for Manufacturer and Model";
            this.textEntryErrorLabel.Visible = false;
            // 
            // suggestedManufacturerListBox
            // 
            this.suggestedManufacturerListBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.suggestedManufacturerListBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.suggestedManufacturerListBox.FormattingEnabled = true;
            this.suggestedManufacturerListBox.Location = new System.Drawing.Point(355, 63);
            this.suggestedManufacturerListBox.Name = "suggestedManufacturerListBox";
            this.suggestedManufacturerListBox.Size = new System.Drawing.Size(201, 225);
            this.suggestedManufacturerListBox.TabIndex = 2;
            this.suggestedManufacturerListBox.TabStop = false;
            this.suggestedManufacturerListBox.Visible = false;
            this.suggestedManufacturerListBox.SelectedIndexChanged += new System.EventHandler(this.suggestedManufacturerListBox_SelectedIndexChanged);
            // 
            // suggestedManufacturerLabel
            // 
            this.suggestedManufacturerLabel.AutoSize = true;
            this.suggestedManufacturerLabel.BackColor = System.Drawing.Color.Transparent;
            this.suggestedManufacturerLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.suggestedManufacturerLabel.ForeColor = System.Drawing.Color.Black;
            this.suggestedManufacturerLabel.Location = new System.Drawing.Point(352, 47);
            this.suggestedManufacturerLabel.Name = "suggestedManufacturerLabel";
            this.suggestedManufacturerLabel.Size = new System.Drawing.Size(131, 13);
            this.suggestedManufacturerLabel.TabIndex = 7;
            this.suggestedManufacturerLabel.Text = "Suggested Manufacturers";
            this.suggestedManufacturerLabel.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(120, 149);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 30);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // manualEntryButton
            // 
            this.manualEntryButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.manualEntryButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.manualEntryButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.manualEntryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.manualEntryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.manualEntryButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualEntryButton.ForeColor = System.Drawing.Color.White;
            this.manualEntryButton.Location = new System.Drawing.Point(232, 258);
            this.manualEntryButton.Name = "manualEntryButton";
            this.manualEntryButton.Size = new System.Drawing.Size(90, 30);
            this.manualEntryButton.TabIndex = 8;
            this.manualEntryButton.Text = "Manual Entry";
            this.manualEntryButton.UseVisualStyleBackColor = false;
            this.manualEntryButton.Visible = false;
            this.manualEntryButton.Click += new System.EventHandler(this.manualEntryButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(222, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Find Merchandise";
            // 
            // FindMerchandise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(574, 300);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.manualEntryButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.suggestedManufacturerLabel);
            this.Controls.Add(this.suggestedManufacturerListBox);
            this.Controls.Add(this.textEntryErrorLabel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.modelTextBox);
            this.Controls.Add(this.manufacturerTextBox);
            this.Controls.Add(this.modelLabel);
            this.Controls.Add(this.manufacturerLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindMerchandise";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Merchandise";
            this.Load += new System.EventHandler(this.FindMerchandise_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label manufacturerLabel;
        private System.Windows.Forms.Label modelLabel;
        protected System.Windows.Forms.TextBox manufacturerTextBox;
        private System.Windows.Forms.TextBox modelTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label textEntryErrorLabel;
        protected System.Windows.Forms.ListBox suggestedManufacturerListBox;
        private System.Windows.Forms.Label suggestedManufacturerLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button manualEntryButton;
        private System.Windows.Forms.Label label1;
    }
}

