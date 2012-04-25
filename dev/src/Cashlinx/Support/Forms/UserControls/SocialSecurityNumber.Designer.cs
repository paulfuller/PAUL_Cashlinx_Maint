using Common.Libraries.Forms.Components;

namespace Support.Forms.UserControls
{
    partial class SocialSecurityNumber
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
            this.ssnTextBox = new CustomTextBox();
            this.SuspendLayout();
            // 
            // ssnTextBox
            // 
            this.ssnTextBox.AllowOnlyNumbers = true;
            this.ssnTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ssnTextBox.CausesValidation = false;
            this.ssnTextBox.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.ssnTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssnTextBox.Location = new System.Drawing.Point(0, 0);
            this.ssnTextBox.MaxLength = 9;
            this.ssnTextBox.Name = "ssnTextBox";
            this.ssnTextBox.Size = new System.Drawing.Size(123, 21);
            this.ssnTextBox.TabIndex = 0;
            this.ssnTextBox.ValidationExpression = "";
            // 
            // SocialSecurityNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.ssnTextBox);
            this.Name = "SocialSecurityNumber";
            this.Size = new System.Drawing.Size(123, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox ssnTextBox;
    }
}
