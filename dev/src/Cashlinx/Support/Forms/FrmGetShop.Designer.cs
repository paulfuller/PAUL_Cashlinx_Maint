namespace Support.Forms
{
    partial class FrmGetShop
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
            this.changePasswordHeaderLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.Findbutton1 = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShop = new Common.Libraries.Forms.Components.CustomTextBox();
            this.SuspendLayout();
            // 
            // changePasswordHeaderLabel
            // 
            this.changePasswordHeaderLabel.AutoSize = true;
            this.changePasswordHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.changePasswordHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePasswordHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.changePasswordHeaderLabel.Location = new System.Drawing.Point(133, 27);
            this.changePasswordHeaderLabel.Name = "changePasswordHeaderLabel";
            this.changePasswordHeaderLabel.Size = new System.Drawing.Size(80, 19);
            this.changePasswordHeaderLabel.TabIndex = 2;
            this.changePasswordHeaderLabel.Text = "Find Shop";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(28, 180);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 38);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Findbutton1
            // 
            this.Findbutton1.BackColor = System.Drawing.Color.Transparent;
            this.Findbutton1.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.Findbutton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Findbutton1.FlatAppearance.BorderSize = 0;
            this.Findbutton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Findbutton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Findbutton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Findbutton1.ForeColor = System.Drawing.Color.White;
            this.Findbutton1.Location = new System.Drawing.Point(219, 180);
            this.Findbutton1.Margin = new System.Windows.Forms.Padding(0);
            this.Findbutton1.Name = "Findbutton1";
            this.Findbutton1.Size = new System.Drawing.Size(100, 38);
            this.Findbutton1.TabIndex = 3;
            this.Findbutton1.Text = "Find";
            this.Findbutton1.UseVisualStyleBackColor = false;
            this.Findbutton1.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Location = new System.Drawing.Point(80, 118);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSearch.Size = new System.Drawing.Size(75, 13);
            this.lblSearch.TabIndex = 16;
            this.lblSearch.Text = "Shop Number:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(156, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShop
            // 
            this.txtShop.AllowOnlyNumbers = true;
            this.txtShop.CausesValidation = false;
            this.txtShop.ErrorMessage = "Invalid Data Entry.";
            this.txtShop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShop.Location = new System.Drawing.Point(188, 115);
            this.txtShop.MaxLength = 5;
            this.txtShop.Name = "txtShop";
            this.txtShop.RegularExpression = true;
            this.txtShop.Required = true;
            this.txtShop.Size = new System.Drawing.Size(69, 21);
            this.txtShop.TabIndex = 18;
            this.txtShop.ValidationExpression = "[0-9]";
            this.txtShop.WordWrap = false;
            // 
            // FrmGetShop
            // 
            this.AcceptButton = this.Findbutton1;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Support.Properties.Resources.form_350_240;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(350, 240);
            this.Controls.Add(this.txtShop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.Findbutton1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changePasswordHeaderLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(350, 240);
            this.MinimumSize = new System.Drawing.Size(350, 240);
            this.Name = "FrmGetShop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGetShop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changePasswordHeaderLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button Findbutton1;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label1;
        private Common.Libraries.Forms.Components.CustomTextBox txtShop;  
    }
}