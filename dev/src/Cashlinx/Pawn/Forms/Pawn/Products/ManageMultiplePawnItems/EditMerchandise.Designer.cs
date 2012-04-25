namespace Pawn.Forms.Pawn.Products.ManageMultiplePawnItems
{
    partial class EditMerchandise
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
            this.doneButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.modelLabel = new System.Windows.Forms.Label();
            this.manufacturerTextBox = new System.Windows.Forms.TextBox();
            this.modelTextBox = new System.Windows.Forms.TextBox();
            this.loanRangeTextBox = new System.Windows.Forms.TextBox();
            this.retailAmountTextBox = new System.Windows.Forms.TextBox();
            this.loanAmountTextBox = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.loanRangeLabel = new System.Windows.Forms.Label();
            this.retailAmountLabel = new System.Windows.Forms.Label();
            this.loanAmountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // manufacturerLabel
            // 
            this.manufacturerLabel.AutoSize = true;
            this.manufacturerLabel.BackColor = System.Drawing.Color.Transparent;
            this.manufacturerLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manufacturerLabel.ForeColor = System.Drawing.Color.Black;
            this.manufacturerLabel.Location = new System.Drawing.Point(29, 69);
            this.manufacturerLabel.Name = "manufacturerLabel";
            this.manufacturerLabel.Size = new System.Drawing.Size(72, 13);
            this.manufacturerLabel.TabIndex = 0;
            this.manufacturerLabel.Text = "Manufacturer";
            // 
            // doneButton
            // 
            this.doneButton.BackColor = System.Drawing.Color.Transparent;
            this.doneButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.doneButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.doneButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.doneButton.FlatAppearance.BorderSize = 0;
            this.doneButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.doneButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.doneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doneButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doneButton.ForeColor = System.Drawing.Color.White;
            this.doneButton.Location = new System.Drawing.Point(164, 273);
            this.doneButton.Margin = new System.Windows.Forms.Padding(4);
            this.doneButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.doneButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(100, 50);
            this.doneButton.TabIndex = 2;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = false;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(28, 273);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // modelLabel
            // 
            this.modelLabel.AutoSize = true;
            this.modelLabel.BackColor = System.Drawing.Color.Transparent;
            this.modelLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelLabel.ForeColor = System.Drawing.Color.Black;
            this.modelLabel.Location = new System.Drawing.Point(66, 105);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(35, 13);
            this.modelLabel.TabIndex = 3;
            this.modelLabel.Text = "Model";
            // 
            // manufacturerTextBox
            // 
            this.manufacturerTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.manufacturerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.manufacturerTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manufacturerTextBox.ForeColor = System.Drawing.Color.Black;
            this.manufacturerTextBox.Location = new System.Drawing.Point(104, 68);
            this.manufacturerTextBox.Name = "manufacturerTextBox";
            this.manufacturerTextBox.ReadOnly = true;
            this.manufacturerTextBox.Size = new System.Drawing.Size(162, 14);
            this.manufacturerTextBox.TabIndex = 7;
            this.manufacturerTextBox.TabStop = false;
            // 
            // modelTextBox
            // 
            this.modelTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.modelTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modelTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelTextBox.ForeColor = System.Drawing.Color.Black;
            this.modelTextBox.Location = new System.Drawing.Point(104, 105);
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.ReadOnly = true;
            this.modelTextBox.Size = new System.Drawing.Size(162, 14);
            this.modelTextBox.TabIndex = 8;
            this.modelTextBox.TabStop = false;
            // 
            // loanRangeTextBox
            // 
            this.loanRangeTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.loanRangeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.loanRangeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanRangeTextBox.ForeColor = System.Drawing.Color.Black;
            this.loanRangeTextBox.Location = new System.Drawing.Point(104, 142);
            this.loanRangeTextBox.Name = "loanRangeTextBox";
            this.loanRangeTextBox.ReadOnly = true;
            this.loanRangeTextBox.Size = new System.Drawing.Size(162, 14);
            this.loanRangeTextBox.TabIndex = 9;
            this.loanRangeTextBox.TabStop = false;
            // 
            // retailAmountTextBox
            // 
            this.retailAmountTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.retailAmountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.retailAmountTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retailAmountTextBox.ForeColor = System.Drawing.Color.Black;
            this.retailAmountTextBox.Location = new System.Drawing.Point(104, 178);
            this.retailAmountTextBox.Name = "retailAmountTextBox";
            this.retailAmountTextBox.ReadOnly = true;
            this.retailAmountTextBox.Size = new System.Drawing.Size(162, 14);
            this.retailAmountTextBox.TabIndex = 10;
            this.retailAmountTextBox.TabStop = false;
            // 
            // loanAmountTextBox
            // 
            this.loanAmountTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loanAmountTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanAmountTextBox.ForeColor = System.Drawing.Color.Black;
            this.loanAmountTextBox.Location = new System.Drawing.Point(104, 215);
            this.loanAmountTextBox.Name = "loanAmountTextBox";
            this.loanAmountTextBox.Size = new System.Drawing.Size(162, 21);
            this.loanAmountTextBox.TabIndex = 1;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(101, 239);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(165, 13);
            this.errorLabel.TabIndex = 12;
            this.errorLabel.Text = "Please enter a valid number";
            this.errorLabel.Visible = false;
            // 
            // loanRangeLabel
            // 
            this.loanRangeLabel.AutoSize = true;
            this.loanRangeLabel.BackColor = System.Drawing.Color.Transparent;
            this.loanRangeLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanRangeLabel.ForeColor = System.Drawing.Color.Black;
            this.loanRangeLabel.Location = new System.Drawing.Point(37, 142);
            this.loanRangeLabel.Name = "loanRangeLabel";
            this.loanRangeLabel.Size = new System.Drawing.Size(64, 13);
            this.loanRangeLabel.TabIndex = 13;
            this.loanRangeLabel.Text = "Loan Range";
            // 
            // retailAmountLabel
            // 
            this.retailAmountLabel.AutoSize = true;
            this.retailAmountLabel.BackColor = System.Drawing.Color.Transparent;
            this.retailAmountLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retailAmountLabel.ForeColor = System.Drawing.Color.Black;
            this.retailAmountLabel.Location = new System.Drawing.Point(27, 179);
            this.retailAmountLabel.Name = "retailAmountLabel";
            this.retailAmountLabel.Size = new System.Drawing.Size(74, 13);
            this.retailAmountLabel.TabIndex = 14;
            this.retailAmountLabel.Text = "Retail Amount";
            // 
            // loanAmountLabel
            // 
            this.loanAmountLabel.AutoSize = true;
            this.loanAmountLabel.BackColor = System.Drawing.Color.Transparent;
            this.loanAmountLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanAmountLabel.ForeColor = System.Drawing.Color.Black;
            this.loanAmountLabel.Location = new System.Drawing.Point(31, 218);
            this.loanAmountLabel.Name = "loanAmountLabel";
            this.loanAmountLabel.Size = new System.Drawing.Size(70, 13);
            this.loanAmountLabel.TabIndex = 15;
            this.loanAmountLabel.Text = "Loan Amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(82, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 19);
            this.label1.TabIndex = 16;
            this.label1.Text = "Edit Merchandise";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EditMerchandise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(292, 331);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loanAmountLabel);
            this.Controls.Add(this.retailAmountLabel);
            this.Controls.Add(this.loanRangeLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.loanAmountTextBox);
            this.Controls.Add(this.retailAmountTextBox);
            this.Controls.Add(this.loanRangeTextBox);
            this.Controls.Add(this.modelTextBox);
            this.Controls.Add(this.manufacturerTextBox);
            this.Controls.Add(this.modelLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.manufacturerLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditMerchandise";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Merchandise";
            this.Load += new System.EventHandler(this.EditMerchandise_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label manufacturerLabel;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label modelLabel;
        private System.Windows.Forms.TextBox manufacturerTextBox;
        private System.Windows.Forms.TextBox modelTextBox;
        private System.Windows.Forms.TextBox loanRangeTextBox;
        private System.Windows.Forms.TextBox retailAmountTextBox;
        private System.Windows.Forms.TextBox loanAmountTextBox;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label loanRangeLabel;
        private System.Windows.Forms.Label retailAmountLabel;
        private System.Windows.Forms.Label loanAmountLabel;
        private System.Windows.Forms.Label label1;
    }
}