using Support.Libraries.Forms.Components;

namespace Support.Forms.Customer.ProductTabs
{
    partial class AddViewSupportCustomerComment
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
            this.CmbBoxCategory = new System.Windows.Forms.ComboBox();
            this.TxbEmployeeNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.LblEmployeeNumber = new System.Windows.Forms.Label();
            this.LblCategory = new System.Windows.Forms.Label();
            this.TxbComment = new Common.Libraries.Forms.Components.CustomTextBox();
            this.LblHeader = new System.Windows.Forms.Label();
            this.TxbCategory = new Common.Libraries.Forms.Components.CustomTextBox();
            this.BtnAppend = new Support.Libraries.Forms.Components.SupportButton();
            this.BtnCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.SuspendLayout();
            // 
            // CmbBoxCategory
            // 
            this.CmbBoxCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CmbBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBoxCategory.FormattingEnabled = true;
            this.CmbBoxCategory.Location = new System.Drawing.Point(45, 298);
            this.CmbBoxCategory.Name = "CmbBoxCategory";
            this.CmbBoxCategory.Size = new System.Drawing.Size(221, 21);
            this.CmbBoxCategory.TabIndex = 2;
            // 
            // TxbEmployeeNumber
            // 
            this.TxbEmployeeNumber.CausesValidation = false;
            this.TxbEmployeeNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.TxbEmployeeNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbEmployeeNumber.Location = new System.Drawing.Point(45, 352);
            this.TxbEmployeeNumber.Name = "TxbEmployeeNumber";
            this.TxbEmployeeNumber.Size = new System.Drawing.Size(221, 21);
            this.TxbEmployeeNumber.TabIndex = 3;
            this.TxbEmployeeNumber.MaxLength = 20;
            this.TxbEmployeeNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            //this.TxbEmployeeNumber.Leave += new System.EventHandler(this.TxbEmployeeNumber_Leave);
            // 
            // LblEmployeeNumber
            // 
            this.LblEmployeeNumber.AutoSize = true;
            this.LblEmployeeNumber.BackColor = System.Drawing.Color.Transparent;
            this.LblEmployeeNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEmployeeNumber.ForeColor = System.Drawing.SystemColors.Control;
            this.LblEmployeeNumber.Location = new System.Drawing.Point(34, 334);
            this.LblEmployeeNumber.Name = "LblEmployeeNumber";
            this.LblEmployeeNumber.Size = new System.Drawing.Size(109, 13);
            this.LblEmployeeNumber.TabIndex = 6;
            this.LblEmployeeNumber.Text = "Employee Number";
            // 
            // LblCategory
            // 
            this.LblCategory.AutoSize = true;
            this.LblCategory.BackColor = System.Drawing.Color.Transparent;
            this.LblCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCategory.ForeColor = System.Drawing.SystemColors.Control;
            this.LblCategory.Location = new System.Drawing.Point(34, 282);
            this.LblCategory.Name = "LblCategory";
            this.LblCategory.Size = new System.Drawing.Size(63, 14);
            this.LblCategory.TabIndex = 4;
            this.LblCategory.Text = "Category";
            // 
            // TxbComment
            // 
            this.TxbComment.CausesValidation = false;
            this.TxbComment.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.TxbComment.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbComment.Location = new System.Drawing.Point(45, 68);
            this.TxbComment.Multiline = true;
            this.TxbComment.Name = "TxbComment";
            this.TxbComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxbComment.Size = new System.Drawing.Size(593, 195);
            this.TxbComment.TabIndex = 1;
            this.TxbComment.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // LblHeader
            // 
            this.LblHeader.AutoSize = true;
            this.LblHeader.BackColor = System.Drawing.Color.Transparent;
            this.LblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.White;
            this.LblHeader.Location = new System.Drawing.Point(251, 20);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(131, 25);
            this.LblHeader.TabIndex = 0;
            this.LblHeader.Text = "Add Comments";
            this.LblHeader.UseCompatibleTextRendering = true;
            // 
            // TxbCategory
            // 
            this.TxbCategory.CausesValidation = false;
            this.TxbCategory.Enabled = false;
            this.TxbCategory.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.TxbCategory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbCategory.Location = new System.Drawing.Point(45, 298);
            this.TxbCategory.Name = "TxbCategory";
            this.TxbCategory.Size = new System.Drawing.Size(221, 21);
            this.TxbCategory.TabIndex = 2;
            this.TxbCategory.Text = "temp";
            this.TxbCategory.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // BtnAppend
            // 
            this.BtnAppend.BackColor = System.Drawing.Color.Transparent;
            this.BtnAppend.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.BtnAppend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAppend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAppend.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnAppend.FlatAppearance.BorderSize = 0;
            this.BtnAppend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnAppend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnAppend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAppend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAppend.ForeColor = System.Drawing.Color.White;
            this.BtnAppend.Location = new System.Drawing.Point(539, 393);
            this.BtnAppend.Margin = new System.Windows.Forms.Padding(0);
            this.BtnAppend.MaximumSize = new System.Drawing.Size(90, 40);
            this.BtnAppend.MinimumSize = new System.Drawing.Size(90, 40);
            this.BtnAppend.Name = "BtnAppend";
            this.BtnAppend.Size = new System.Drawing.Size(90, 40);
            this.BtnAppend.TabIndex = 5;
            this.BtnAppend.Text = "Save";
            this.BtnAppend.UseVisualStyleBackColor = false;
            this.BtnAppend.Click += new System.EventHandler(this.BtnAppend_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.Transparent;
            this.BtnCancel.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.BtnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnCancel.FlatAppearance.BorderSize = 0;
            this.BtnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(45, 393);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.BtnCancel.MaximumSize = new System.Drawing.Size(90, 40);
            this.BtnCancel.MinimumSize = new System.Drawing.Size(90, 40);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(90, 40);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AddViewSupportCustomerComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_600;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(676, 455);
            this.ControlBox = false;
            this.Controls.Add(this.TxbEmployeeNumber);
            this.Controls.Add(this.LblEmployeeNumber);
            this.Controls.Add(this.CmbBoxCategory);
            this.Controls.Add(this.LblCategory);
            this.Controls.Add(this.TxbComment);
            this.Controls.Add(this.BtnAppend);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblHeader);
            this.Controls.Add(this.TxbCategory);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddViewSupportCustomerComment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Comment";
            this.Load += new System.EventHandler(this.AddViewSupportCustomerComment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblHeader;
        private SupportButton BtnCancel;
        private SupportButton BtnAppend;
        private Common.Libraries.Forms.Components.CustomTextBox TxbComment;
        private System.Windows.Forms.Label LblCategory;
        private System.Windows.Forms.ComboBox CmbBoxCategory;
        private System.Windows.Forms.Label LblEmployeeNumber;
        private Common.Libraries.Forms.Components.CustomTextBox TxbEmployeeNumber;
        private Common.Libraries.Forms.Components.CustomTextBox TxbCategory;
        private System.Windows.Forms.Label label1;
    }
}