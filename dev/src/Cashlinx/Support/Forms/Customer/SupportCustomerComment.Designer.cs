namespace Support.Forms.Customer
{
    partial class SupportCustomerComment
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
            this.LblHeader = new System.Windows.Forms.Label();
            this.TxbComment = new System.Windows.Forms.TextBox();
            this.LbLastEditedBy = new Common.Libraries.Forms.Components.CustomLabel();
            this.LbUpdatedDate = new Common.Libraries.Forms.Components.CustomLabel();
            this.TxbLastEditedBy = new Common.Libraries.Forms.Components.CustomLabel();
            this.TxbUpdatedDate = new Common.Libraries.Forms.Components.CustomLabel();
            this.BtnSubmit = new Support.Libraries.Forms.Components.SupportButton();
            this.BtnCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.SuspendLayout();
            // 
            // LblHeader
            // 
            this.LblHeader.AutoSize = true;
            this.LblHeader.BackColor = System.Drawing.Color.Transparent;
            this.LblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LblHeader.Location = new System.Drawing.Point(249, 20);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(178, 19);
            this.LblHeader.TabIndex = 0;
            this.LblHeader.Text = "Customer Comments";
            // 
            // TxbComment
            // 
            this.TxbComment.AcceptsReturn = true;
            this.TxbComment.AcceptsTab = true;
            this.TxbComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbComment.Location = new System.Drawing.Point(42, 60);
            this.TxbComment.Multiline = true;
            this.TxbComment.Name = "TxbComment";
            this.TxbComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxbComment.Size = new System.Drawing.Size(592, 316);
            this.TxbComment.TabIndex = 6;
            this.TxbComment.TabStop = false;
            this.TxbComment.TextChanged += new System.EventHandler(this.TxbComment_TextChanged);
            // 
            // LbLastEditedBy
            // 
            this.LbLastEditedBy.AutoSize = true;
            this.LbLastEditedBy.BackColor = System.Drawing.Color.Transparent;
            this.LbLastEditedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbLastEditedBy.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LbLastEditedBy.Location = new System.Drawing.Point(190, 392);
            this.LbLastEditedBy.Name = "LbLastEditedBy";
            this.LbLastEditedBy.Size = new System.Drawing.Size(98, 13);
            this.LbLastEditedBy.TabIndex = 9;
            this.LbLastEditedBy.Text = "Last Udated By:";
            // 
            // LbUpdatedDate
            // 
            this.LbUpdatedDate.AutoSize = true;
            this.LbUpdatedDate.BackColor = System.Drawing.Color.Transparent;
            this.LbUpdatedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbUpdatedDate.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LbUpdatedDate.Location = new System.Drawing.Point(193, 416);
            this.LbUpdatedDate.Name = "LbUpdatedDate";
            this.LbUpdatedDate.Size = new System.Drawing.Size(81, 13);
            this.LbUpdatedDate.TabIndex = 11;
            this.LbUpdatedDate.Text = "Last Updated:";
            // 
            // TxbLastEditedBy
            // 
            this.TxbLastEditedBy.AutoSize = true;
            this.TxbLastEditedBy.BackColor = System.Drawing.Color.Transparent;
            this.TxbLastEditedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbLastEditedBy.ForeColor = System.Drawing.SystemColors.Desktop;
            this.TxbLastEditedBy.Location = new System.Drawing.Point(312, 392);
            this.TxbLastEditedBy.Name = "TxbLastEditedBy";
            this.TxbLastEditedBy.Size = new System.Drawing.Size(0, 13);
            this.TxbLastEditedBy.TabIndex = 13;
            // 
            // TxbUpdatedDate
            // 
            this.TxbUpdatedDate.AutoSize = true;
            this.TxbUpdatedDate.BackColor = System.Drawing.Color.Transparent;
            this.TxbUpdatedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxbUpdatedDate.ForeColor = System.Drawing.SystemColors.Desktop;
            this.TxbUpdatedDate.Location = new System.Drawing.Point(312, 416);
            this.TxbUpdatedDate.Name = "TxbUpdatedDate";
            this.TxbUpdatedDate.Size = new System.Drawing.Size(0, 13);
            this.TxbUpdatedDate.TabIndex = 14;
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.BtnSubmit.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.BtnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnSubmit.FlatAppearance.BorderSize = 0;
            this.BtnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubmit.ForeColor = System.Drawing.Color.White;
            this.BtnSubmit.Location = new System.Drawing.Point(544, 392);
            this.BtnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSubmit.MaximumSize = new System.Drawing.Size(90, 40);
            this.BtnSubmit.MinimumSize = new System.Drawing.Size(90, 40);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(90, 40);
            this.BtnSubmit.TabIndex = 15;
            this.BtnSubmit.Text = "Submit";
            this.BtnSubmit.UseVisualStyleBackColor = false;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
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
            this.BtnCancel.Location = new System.Drawing.Point(42, 392);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.BtnCancel.MaximumSize = new System.Drawing.Size(90, 40);
            this.BtnCancel.MinimumSize = new System.Drawing.Size(90, 40);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(90, 40);
            this.BtnCancel.TabIndex = 16;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // SupportCustomerComment
            // 
            this.AcceptButton = this.BtnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(676, 455);
            this.Controls.Add(this.TxbUpdatedDate);
            this.Controls.Add(this.TxbLastEditedBy);
            this.Controls.Add(this.LbUpdatedDate);
            this.Controls.Add(this.LbLastEditedBy);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.TxbComment);
            this.Controls.Add(this.LblHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SupportCustomerComment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SupportCustomerComment";
            this.Load += new System.EventHandler(this.SupportCustomerComment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblHeader;
        private System.Windows.Forms.TextBox TxbComment;
        private Common.Libraries.Forms.Components.CustomLabel LbLastEditedBy;
        private Common.Libraries.Forms.Components.CustomLabel LbUpdatedDate;
        private Common.Libraries.Forms.Components.CustomLabel TxbLastEditedBy;
        private Common.Libraries.Forms.Components.CustomLabel TxbUpdatedDate;
        private Libraries.Forms.Components.SupportButton BtnSubmit;
        private Libraries.Forms.Components.SupportButton BtnCancel;
    }
}