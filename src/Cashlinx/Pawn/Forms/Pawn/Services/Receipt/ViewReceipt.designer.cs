using System.Windows.Forms;

namespace Pawn.Forms.Pawn.Services.Receipt
{
    partial class ViewReceipt
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
            this.receiptDataTextBox = new System.Windows.Forms.RichTextBox();
            this.printButton = new System.Windows.Forms.Button();
            this.receiptDateTimeLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // receiptDataTextBox
            // 
            this.receiptDataTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.receiptDataTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.receiptDataTextBox.DetectUrls = false;
            this.receiptDataTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptDataTextBox.ForeColor = System.Drawing.Color.Black;
            this.receiptDataTextBox.Location = new System.Drawing.Point(25, 100);
            this.receiptDataTextBox.Name = "receiptDataTextBox";
            this.receiptDataTextBox.ReadOnly = true;
            this.receiptDataTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.receiptDataTextBox.Size = new System.Drawing.Size(351, 432);
            this.receiptDataTextBox.TabIndex = 2;
            this.receiptDataTextBox.Text = "";
            // 
            // printButton
            // 
            this.printButton.BackColor = System.Drawing.Color.Transparent;
            this.printButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.printButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.printButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.printButton.FlatAppearance.BorderSize = 0;
            this.printButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printButton.ForeColor = System.Drawing.Color.White;
            this.printButton.Location = new System.Drawing.Point(273, 535);
            this.printButton.Margin = new System.Windows.Forms.Padding(0);
            this.printButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.printButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(100, 50);
            this.printButton.TabIndex = 3;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // receiptDateTimeLabel
            // 
            this.receiptDateTimeLabel.AutoSize = true;
            this.receiptDateTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.receiptDateTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.receiptDateTimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.receiptDateTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.receiptDateTimeLabel.Location = new System.Drawing.Point(25, 85);
            this.receiptDateTimeLabel.Name = "receiptDateTimeLabel";
            this.receiptDateTimeLabel.Size = new System.Drawing.Size(64, 15);
            this.receiptDateTimeLabel.TabIndex = 5;
            this.receiptDateTimeLabel.Text = "Date / Time";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(151, 29);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(99, 19);
            this.titleLabel.TabIndex = 6;
            this.titleLabel.Text = "View Receipt";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.backButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(22, 535);
            this.backButton.Margin = new System.Windows.Forms.Padding(0);
            this.backButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.backButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 50);
            this.backButton.TabIndex = 7;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // ViewReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(400, 600);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.receiptDateTimeLabel);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.receiptDataTextBox);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewReceipt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Receipt";
            this.Load += new System.EventHandler(this.ViewReceipt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox receiptDataTextBox;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Label receiptDateTimeLabel;
        private System.Windows.Forms.Label titleLabel;
        private Button backButton;

    }
}

