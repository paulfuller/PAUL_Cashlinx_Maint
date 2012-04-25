namespace Pawn.Forms.UserControls
{
    partial class UserItemCommentsControl
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtComments = new Common.Libraries.Forms.Components.CustomTextBox();
            this.lblICN = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Courier New", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(152, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(161, 34);
            this.lblDescription.TabIndex = 50;
            this.lblDescription.Text = "0";
            // 
            // txtComments
            // 
            this.txtComments.CausesValidation = false;
            this.txtComments.ErrorMessage = "";
            this.txtComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComments.Location = new System.Drawing.Point(319, 5);
            this.txtComments.MaxLength = 100;
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(186, 34);
            this.txtComments.TabIndex = 51;
            this.txtComments.ValidationExpression = "";
            this.txtComments.Leave += new System.EventHandler(this.txtComments_Leave);
            // 
            // lblICN
            // 
            this.lblICN.Font = new System.Drawing.Font("Courier New", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblICN.Location = new System.Drawing.Point(3, 5);
            this.lblICN.Name = "lblICN";
            this.lblICN.Size = new System.Drawing.Size(143, 34);
            this.lblICN.TabIndex = 52;
            this.lblICN.Text = "0";
            // 
            // UserItemCommentsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblICN);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.lblDescription);
            this.Name = "UserItemCommentsControl";
            this.Size = new System.Drawing.Size(508, 43);
            this.Load += new System.EventHandler(this.UserItemCommentsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private Common.Libraries.Forms.Components.CustomTextBox txtComments;
        private System.Windows.Forms.Label lblICN;
    }
}
