using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls
{
    partial class Zipcode
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.zipcodeTextBox = new CustomTextBox();
            this.SuspendLayout();
            // 
            // zipcodeTextBox
            // 
            this.zipcodeTextBox.AllowOnlyNumbers = true;
            this.zipcodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zipcodeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.zipcodeTextBox.CausesValidation = false;
            this.zipcodeTextBox.ErrorMessage = "Zipcode should be a 5 digit number";
            this.zipcodeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zipcodeTextBox.Location = new System.Drawing.Point(2, 0);
            this.zipcodeTextBox.MaxLength = 5;
            this.zipcodeTextBox.Name = "zipcodeTextBox";
            this.zipcodeTextBox.RegularExpression = true;
            this.zipcodeTextBox.Size = new System.Drawing.Size(74, 21);
            this.zipcodeTextBox.TabIndex = 0;
            this.zipcodeTextBox.ValidationExpression = "^[0-9]{5}";
            // 
            // Zipcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.CausesValidation = false;
            this.Controls.Add(this.zipcodeTextBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Zipcode";
            this.Size = new System.Drawing.Size(74, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox zipcodeTextBox;
    }
}
