namespace Pawn.Forms.UserControls
{
    partial class State
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
            this.stateList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // stateList
            // 
            this.stateList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.stateList.BackColor = System.Drawing.Color.White;
            this.stateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateList.ForeColor = System.Drawing.Color.Black;
            this.stateList.FormattingEnabled = true;
            this.stateList.Location = new System.Drawing.Point(0, -1);
            this.stateList.Name = "stateList";
            this.stateList.Size = new System.Drawing.Size(50, 21);
            this.stateList.TabIndex = 0;
            this.stateList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.stateList_DrawItem);
            this.stateList.SelectedIndexChanged += new System.EventHandler(this.stateList_SelectedIndexChanged);
            this.stateList.Layout += new System.Windows.Forms.LayoutEventHandler(this.stateList_Layout);
            // 
            // State
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.stateList);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "State";
            this.Size = new System.Drawing.Size(50, 21);
            this.EnabledChanged += new System.EventHandler(this.State_EnabledChanged);
            this.ResumeLayout(false);

        }

        #endregion

       
   
        private System.Windows.Forms.ComboBox stateList;
    }
}
