namespace Pawn.Forms.UserControls
{
    partial class Haircolor
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
            this.haircolorList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // haircolorList
            // 
            this.haircolorList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.haircolorList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.haircolorList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.haircolorList.FormattingEnabled = true;
            this.haircolorList.IntegralHeight = false;
            this.haircolorList.Location = new System.Drawing.Point(0, 0);
            this.haircolorList.Name = "haircolorList";
            this.haircolorList.Size = new System.Drawing.Size(132, 21);
            this.haircolorList.TabIndex = 0;
            this.haircolorList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.haircolorList_DrawItem);
            // 
            // Haircolor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.haircolorList);
            this.Name = "Haircolor";
            this.Size = new System.Drawing.Size(130, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox haircolorList;
    }
}
