namespace Pawn.Forms.UserControls
{
    partial class HearAboutUs
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
            this.comboBoxHearAbtUs = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxHearAbtUs
            // 
            this.comboBoxHearAbtUs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxHearAbtUs.CausesValidation = false;
            this.comboBoxHearAbtUs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHearAbtUs.Location = new System.Drawing.Point(0, 1);
            this.comboBoxHearAbtUs.Name = "comboBoxHearAbtUs";
            this.comboBoxHearAbtUs.Size = new System.Drawing.Size(121, 21);
            this.comboBoxHearAbtUs.TabIndex = 0;
            // 
            // HearAboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxHearAbtUs);
            this.Name = "HearAboutUs";
            this.Size = new System.Drawing.Size(121, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxHearAbtUs;
    }
}
