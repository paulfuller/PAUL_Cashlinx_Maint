namespace Support.Forms
{
    partial class EyeColor
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
            this.eyecolorList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // eyecolorList
            // 
            this.eyecolorList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.eyecolorList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eyecolorList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.eyecolorList.FormattingEnabled = true;
            this.eyecolorList.IntegralHeight = false;
            this.eyecolorList.Location = new System.Drawing.Point(0, 0);
            this.eyecolorList.Name = "eyecolorList";
            this.eyecolorList.Size = new System.Drawing.Size(128, 21);
            this.eyecolorList.TabIndex = 0;
            this.eyecolorList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.eyecolorList_DrawItem);
            // 
            // EyeColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eyecolorList);
            this.DoubleBuffered = true;
            this.Name = "EyeColor";
            this.Size = new System.Drawing.Size(128, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox eyecolorList;
    }
}
