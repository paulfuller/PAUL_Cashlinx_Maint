namespace Common.Libraries.Forms.Components
{
    partial class CustomTextBox
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
            this.cashlinxTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cashlinxTextbox
            // 
            this.cashlinxTextbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cashlinxTextbox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashlinxTextbox.ForeColor = System.Drawing.Color.Black;
            this.cashlinxTextbox.Location = new System.Drawing.Point(5, 3);
            this.cashlinxTextbox.Name = "cashlinxTextbox";
            this.cashlinxTextbox.Size = new System.Drawing.Size(160, 21);
            this.cashlinxTextbox.TabIndex = 0;
            // 
            // CustomTextBox
            // 
            this.CausesValidation = false;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReadOnlyChanged += new System.EventHandler(this.CustomTextBox_ReadOnlyChanged);
            this.EnabledChanged += new System.EventHandler(this.CustomTextBox_EnabledChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox cashlinxTextbox;
    }
}
