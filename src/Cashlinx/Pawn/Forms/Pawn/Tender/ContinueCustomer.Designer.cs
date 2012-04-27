using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Tender
{
    partial class ContinueCustomer
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
            this.customLabel1 = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.yesButton = new CustomButton();
            this.noButton = new CustomButton();
            this.SuspendLayout();
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.ForeColor = System.Drawing.Color.White;
            this.customLabel1.Location = new System.Drawing.Point(153, 24);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(145, 19);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "Continue Customer";
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(67, 124);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(317, 32);
            this.customLabel2.TabIndex = 1;
            this.customLabel2.Text = "Your transaction is complete.\r\nDo you have additional transactions for this custo" +
                "mer?";
            this.customLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yesButton
            // 
            this.yesButton.BackColor = System.Drawing.Color.Transparent;
            this.yesButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.yesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.yesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yesButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.yesButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.yesButton.FlatAppearance.BorderSize = 0;
            this.yesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.yesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.yesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yesButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yesButton.ForeColor = System.Drawing.Color.White;
            this.yesButton.Location = new System.Drawing.Point(101, 204);
            this.yesButton.Margin = new System.Windows.Forms.Padding(0);
            this.yesButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.yesButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(100, 50);
            this.yesButton.TabIndex = 1;
            this.yesButton.Text = "Yes";
            this.yesButton.UseVisualStyleBackColor = false;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // noButton
            // 
            this.noButton.BackColor = System.Drawing.Color.Transparent;
            this.noButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.noButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.noButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.noButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.noButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.noButton.FlatAppearance.BorderSize = 0;
            this.noButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.noButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.noButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noButton.ForeColor = System.Drawing.Color.White;
            this.noButton.Location = new System.Drawing.Point(250, 204);
            this.noButton.Margin = new System.Windows.Forms.Padding(0);
            this.noButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.noButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(100, 50);
            this.noButton.TabIndex = 2;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = false;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // ContinueCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(450, 275);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.customLabel1);
            this.Name = "ContinueCustomer";
            this.Text = "ContinueCustomer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel customLabel1;
        private CustomLabel customLabel2;
        private CustomButton yesButton;
        private CustomButton noButton;
    }
}