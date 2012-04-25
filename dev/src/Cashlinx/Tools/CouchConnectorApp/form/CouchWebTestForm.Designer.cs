namespace CouchConsoleApp.form
{
    partial class CouchWebTestForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.authButton = new System.Windows.Forms.Button();
            this.GetDocButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.doctext = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // authButton
            // 
            this.authButton.Location = new System.Drawing.Point(237, 30);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(75, 23);
            this.authButton.TabIndex = 0;
            this.authButton.Text = "Authenticate";
            this.authButton.UseVisualStyleBackColor = true;
            // 
            // GetDocButton
            // 
            this.GetDocButton.Location = new System.Drawing.Point(237, 73);
            this.GetDocButton.Name = "GetDocButton";
            this.GetDocButton.Size = new System.Drawing.Size(75, 23);
            this.GetDocButton.TabIndex = 1;
            this.GetDocButton.Text = "Get Doc";
            this.GetDocButton.UseVisualStyleBackColor = true;
            this.GetDocButton.Click += new System.EventHandler(this.GetDocButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(237, 115);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add Doc";
            this.addButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(237, 161);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete Doc";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // doctext
            // 
            this.doctext.Location = new System.Drawing.Point(338, 73);
            this.doctext.Name = "doctext";
            this.doctext.Size = new System.Drawing.Size(261, 20);
            this.doctext.TabIndex = 4;
            // 
            // CouchWebTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 262);
            this.Controls.Add(this.doctext);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.GetDocButton);
            this.Controls.Add(this.authButton);
            this.Name = "CouchWebTestForm";
            this.Text = "CouchWebTestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button authButton;
        private System.Windows.Forms.Button GetDocButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox doctext;
    }
}