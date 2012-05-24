namespace Support.Forms
{
    partial class IDType
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
            this.IdTypeList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // IdTypeList
            // 
            this.IdTypeList.BackColor = System.Drawing.Color.White;
            this.IdTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IdTypeList.ForeColor = System.Drawing.Color.Black;
            this.IdTypeList.FormattingEnabled = true;
            this.IdTypeList.IntegralHeight = false;
            this.IdTypeList.Location = new System.Drawing.Point(0, 0);
            this.IdTypeList.Name = "IdTypeList";
            this.IdTypeList.Size = new System.Drawing.Size(165, 21);
            this.IdTypeList.TabIndex = 0;
            this.IdTypeList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.idTypeList_DrawItem);
            this.IdTypeList.SelectedIndexChanged += new System.EventHandler(this.IdTypeList_SelectedIndexChanged);
            // 
            // IDType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.IdTypeList);
            this.DoubleBuffered = true;
            this.Name = "IDType";
            this.Size = new System.Drawing.Size(165, 22);
            this.EnabledChanged += new System.EventHandler(this.IDType_EnabledChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox IdTypeList;
    }
}
