using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Tender
{
    partial class TenderCoupon
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
            this.customTextBox1 = new CustomTextBox();
            this.customButton1 = new CustomButton();
            this.SuspendLayout();
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.ForeColor = System.Drawing.Color.White;
            this.customLabel1.Location = new System.Drawing.Point(180, 22);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(64, 19);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "Coupon";
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(51, 82);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(104, 16);
            this.customLabel2.TabIndex = 1;
            this.customLabel2.Text = "Scan Coupon #";
            // 
            // customTextBox1
            // 
            this.customTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.customTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox1.CausesValidation = false;
            this.customTextBox1.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBox1.ForeColor = System.Drawing.Color.Black;
            this.customTextBox1.Location = new System.Drawing.Point(161, 81);
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.Size = new System.Drawing.Size(213, 21);
            this.customTextBox1.TabIndex = 2;
            this.customTextBox1.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.Transparent;
            this.customButton1.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.customButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButton1.FlatAppearance.BorderSize = 0;
            this.customButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.White;
            this.customButton1.Location = new System.Drawing.Point(162, 191);
            this.customButton1.Margin = new System.Windows.Forms.Padding(0);
            this.customButton1.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButton1.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(100, 50);
            this.customButton1.TabIndex = 3;
            this.customButton1.Text = "Continue";
            this.customButton1.UseVisualStyleBackColor = false;
            // 
            // TenderCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(425, 250);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.customLabel1);
            this.Name = "TenderCoupon";
            this.Text = "TenderCoupon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel customLabel1;
        private CustomLabel customLabel2;
        private CustomTextBox customTextBox1;
        private CustomButton customButton1;
    }
}