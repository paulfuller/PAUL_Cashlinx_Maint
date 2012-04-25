namespace Support.Forms
{
    partial class Gender
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
            this.genderList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // genderList
            // 
            this.genderList.BackColor = System.Drawing.Color.White;
            this.genderList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.genderList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderList.ForeColor = System.Drawing.Color.Black;
            this.genderList.FormattingEnabled = true;
            this.genderList.IntegralHeight = false;
            this.genderList.Location = new System.Drawing.Point(0, 0);
            this.genderList.Name = "genderList";
            this.genderList.Size = new System.Drawing.Size(82, 21);
            this.genderList.TabIndex = 0;
            this.genderList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.genderList_DrawItem);
            // 
            // Gender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.genderList);
            this.DoubleBuffered = true;
            this.Name = "Gender";
            this.Size = new System.Drawing.Size(82, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox genderList;
    }
}
