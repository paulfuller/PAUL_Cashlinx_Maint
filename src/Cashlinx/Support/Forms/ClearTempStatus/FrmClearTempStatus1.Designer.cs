using Common.Libraries.Forms.Components;

namespace Support.Forms.ClearTempStatus
{
    partial class FrmClearTempStatus1
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
            this.lblRecordType = new System.Windows.Forms.Label();
            this.LblRecTypeAst = new System.Windows.Forms.Label();
            this.cbRecordType = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblAst2 = new System.Windows.Forms.Label();
            this.txtShop = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtDetail = new Common.Libraries.Forms.Components.CustomTextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.FindButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRecordType
            // 
            this.lblRecordType.AutoSize = true;
            this.lblRecordType.BackColor = System.Drawing.Color.Transparent;
            this.lblRecordType.Location = new System.Drawing.Point(70, 116);
            this.lblRecordType.Name = "lblRecordType";
            this.lblRecordType.Size = new System.Drawing.Size(72, 13);
            this.lblRecordType.TabIndex = 0;
            this.lblRecordType.Text = "Record Type:";
            this.lblRecordType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblRecTypeAst
            // 
            this.LblRecTypeAst.AutoSize = true;
            this.LblRecTypeAst.BackColor = System.Drawing.Color.Transparent;
            this.LblRecTypeAst.Enabled = false;
            this.LblRecTypeAst.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRecTypeAst.ForeColor = System.Drawing.Color.Maroon;
            this.LblRecTypeAst.Location = new System.Drawing.Point(183, 119);
            this.LblRecTypeAst.Name = "LblRecTypeAst";
            this.LblRecTypeAst.Size = new System.Drawing.Size(13, 16);
            this.LblRecTypeAst.TabIndex = 1;
            this.LblRecTypeAst.Text = "*";
            this.LblRecTypeAst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbRecordType
            // 
            this.cbRecordType.BackColor = System.Drawing.Color.White;
            this.cbRecordType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecordType.ForeColor = System.Drawing.Color.Black;
            this.cbRecordType.FormattingEnabled = true;
            this.cbRecordType.Location = new System.Drawing.Point(242, 113);
            this.cbRecordType.Name = "cbRecordType";
            this.cbRecordType.Size = new System.Drawing.Size(189, 21);
            this.cbRecordType.TabIndex = 2;
            this.cbRecordType.SelectedIndexChanged += new System.EventHandler(this.cbRecordType_SelectedIndexChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Location = new System.Drawing.Point(16, 162);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSearch.Size = new System.Drawing.Size(126, 13);
            this.lblSearch.TabIndex = 3;
            this.lblSearch.Text = "Shop / Transfer Number:";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAst2
            // 
            this.lblAst2.AutoSize = true;
            this.lblAst2.BackColor = System.Drawing.Color.Transparent;
            this.lblAst2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAst2.ForeColor = System.Drawing.Color.Maroon;
            this.lblAst2.Location = new System.Drawing.Point(183, 163);
            this.lblAst2.Name = "lblAst2";
            this.lblAst2.Size = new System.Drawing.Size(13, 16);
            this.lblAst2.TabIndex = 4;
            this.lblAst2.Text = "*";
            this.lblAst2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShop
            // 
            this.txtShop.AllowOnlyNumbers = true;
            this.txtShop.CausesValidation = false;
            this.txtShop.ErrorMessage = "Invalid Data Entry.";
            this.txtShop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShop.Location = new System.Drawing.Point(205, 154);
            this.txtShop.MaxLength = 5;
            this.txtShop.Name = "txtShop";
            this.txtShop.RegularExpression = true;
            this.txtShop.Required = true;
            this.txtShop.Size = new System.Drawing.Size(61, 21);
            this.txtShop.TabIndex = 5;
            this.txtShop.ValidationExpression = "[0-9]";
            this.txtShop.WordWrap = false;
            // 
            // txtDetail
            // 
            this.txtDetail.CausesValidation = false;
            this.txtDetail.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtDetail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetail.Location = new System.Drawing.Point(278, 154);
            this.txtDetail.MaxLength = 18;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Required = true;
            this.txtDetail.Size = new System.Drawing.Size(151, 21);
            this.txtDetail.TabIndex = 6;
            this.txtDetail.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            this.txtDetail.WordWrap = false;
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
            this.cancelButton.Location = new System.Drawing.Point(75, 267);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 38);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // FindButton
            // 
            this.FindButton.BackColor = System.Drawing.Color.Transparent;
            this.FindButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.FindButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FindButton.FlatAppearance.BorderSize = 0;
            this.FindButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FindButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FindButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindButton.ForeColor = System.Drawing.Color.White;
            this.FindButton.Location = new System.Drawing.Point(305, 267);
            this.FindButton.Margin = new System.Windows.Forms.Padding(0);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(100, 38);
            this.FindButton.TabIndex = 8;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = false;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(144, 29);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Clear Temp Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FrmClearTempStatus1
            // 
            this.AcceptButton = this.FindButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Support.Properties.Resources.form_480_340;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(480, 340);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRecordType);
            this.Controls.Add(this.LblRecTypeAst);
            this.Controls.Add(this.cbRecordType);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblAst2);
            this.Controls.Add(this.txtShop);
            this.Controls.Add(this.txtDetail);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.FindButton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(480, 340);
            this.MinimumSize = new System.Drawing.Size(480, 340);
            this.Name = "FrmClearTempStatus1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmClearTempStatus1";
            this.Load += new System.EventHandler(this.FrmClearTempStatus1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changePasswordHeaderLabel;
        private System.Windows.Forms.ComboBox cbRecordType;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label lblRecordType;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.Label lblSearch;
        //private System.Windows.Forms.TextBox txtShop;
        //private System.Windows.Forms.TextBox txtDetail;
        private Common.Libraries.Forms.Components.CustomTextBox txtShop;
        private Common.Libraries.Forms.Components.CustomTextBox txtDetail;
        private System.Windows.Forms.Label LblRecTypeAst;
        private System.Windows.Forms.Label lblAst2;
        private System.Windows.Forms.Label label1;

    }
}
