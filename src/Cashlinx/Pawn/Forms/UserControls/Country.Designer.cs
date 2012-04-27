namespace Pawn.Forms.UserControls
{
    partial class Country
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
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // countryComboBox
            // 
            this.countryComboBox.BackColor = System.Drawing.Color.White;
            this.countryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.countryComboBox.ForeColor = System.Drawing.Color.Black;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.IntegralHeight = false;
            this.countryComboBox.Location = new System.Drawing.Point(0, 0);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(136, 21);
            this.countryComboBox.TabIndex = 0;
            this.countryComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.countryComboBox_DrawItem);
            this.countryComboBox.SelectedIndexChanged += new System.EventHandler(this.countryComboBox_SelectedIndexChanged);
            // 
            // Country
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.countryComboBox);
            this.DoubleBuffered = true;
            this.Name = "Country";
            this.Size = new System.Drawing.Size(136, 22);
            this.EnabledChanged += new System.EventHandler(this.Country_EnabledChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox countryComboBox;
    }
}
