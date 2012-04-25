namespace Pawn.Forms.UserControls
{
    partial class DateCalendar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateCalendar));
            this.pictureBoxCalendar = new System.Windows.Forms.PictureBox();
            this.dateText = new Date();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCalendar
            // 
            this.pictureBoxCalendar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCalendar.Image")));
            this.pictureBoxCalendar.Location = new System.Drawing.Point(106, 2);
            this.pictureBoxCalendar.Name = "pictureBoxCalendar";
            this.pictureBoxCalendar.Size = new System.Drawing.Size(23, 20);
            this.pictureBoxCalendar.TabIndex = 57;
            this.pictureBoxCalendar.TabStop = false;
            this.pictureBoxCalendar.Click += new System.EventHandler(this.pictureBoxCalendar_Click);
            // 
            // dateText
            // 
            this.dateText.ErrorMessage = "";
            this.dateText.Location = new System.Drawing.Point(3, 3);
            this.dateText.Name = "dateText";
            this.dateText.Size = new System.Drawing.Size(100, 20);
            this.dateText.TabIndex = 56;
            this.dateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateText.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20|21|22)\\d\\d" +
                "$";
            this.dateText.TextBoxTextChanged += new System.EventHandler(this.dateText_TextBoxTextChanged);
            this.dateText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateText_KeyDown);
            // 
            // DateCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pictureBoxCalendar);
            this.Controls.Add(this.dateText);
            this.Name = "DateCalendar";
            this.Size = new System.Drawing.Size(267, 108);
            this.Load += new System.EventHandler(this.DateCalendar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCalendar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCalendar;
        private Date dateText;
    }
}
