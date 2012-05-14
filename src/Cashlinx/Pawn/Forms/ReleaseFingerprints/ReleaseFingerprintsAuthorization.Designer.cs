namespace Pawn.Forms.ReleaseFingerprints
{
    partial class ReleaseFingerprintsAuthorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseFingerprintsAuthorization));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.AuthorizationToReleaseFingerprintsFormLabel = new System.Windows.Forms.Label();
            this.LoanBuyNumberLabel = new System.Windows.Forms.Label();
            this.LoanBuyNumberLabelText = new System.Windows.Forms.Label();
            this.OfficerFirstNameLabel = new System.Windows.Forms.Label();
            this.OfficerLastNameLabel = new System.Windows.Forms.Label();
            this.BadgeNumberText = new System.Windows.Forms.TextBox();
            this.BadgeNumberLabel = new System.Windows.Forms.Label();
            this.AgencyText = new System.Windows.Forms.TextBox();
            this.AgencyLabel = new System.Windows.Forms.Label();
            this.CaseNumberText = new System.Windows.Forms.TextBox();
            this.CaseNumberLabel = new System.Windows.Forms.Label();
            this.MerchandiseDataGridView = new System.Windows.Forms.DataGridView();
            this.RowNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ICN = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Merchandise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelOfficerFirstName = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabel1 = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabel2 = new Common.Libraries.Forms.Components.CustomLabel();
            this.OfficerFirstNameText = new Common.Libraries.Forms.Components.CustomTextBox();
            this.OfficerLastNameText = new Common.Libraries.Forms.Components.CustomTextBox();
            this.SubpoenaNumberText = new Common.Libraries.Forms.Components.CustomTextBox();
            this.CommentText = new Common.Libraries.Forms.Components.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MerchandiseDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCancel.BackgroundImage")));
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(35, 482);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 142;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Transparent;
            this.BackButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BackButton.BackgroundImage")));
            this.BackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BackButton.CausesValidation = false;
            this.BackButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BackButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BackButton.FlatAppearance.BorderSize = 0;
            this.BackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButton.ForeColor = System.Drawing.Color.White;
            this.BackButton.Location = new System.Drawing.Point(160, 482);
            this.BackButton.Margin = new System.Windows.Forms.Padding(4);
            this.BackButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.BackButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(100, 50);
            this.BackButton.TabIndex = 143;
            this.BackButton.Text = "&Back";
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // SubmitButton
            // 
            this.SubmitButton.BackColor = System.Drawing.Color.Transparent;
            this.SubmitButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SubmitButton.BackgroundImage")));
            this.SubmitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SubmitButton.CausesValidation = false;
            this.SubmitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SubmitButton.Enabled = false;
            this.SubmitButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.SubmitButton.FlatAppearance.BorderSize = 0;
            this.SubmitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SubmitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitButton.ForeColor = System.Drawing.Color.White;
            this.SubmitButton.Location = new System.Drawing.Point(565, 482);
            this.SubmitButton.Margin = new System.Windows.Forms.Padding(4);
            this.SubmitButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.SubmitButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(100, 50);
            this.SubmitButton.TabIndex = 7;
            this.SubmitButton.Text = "&Submit";
            this.SubmitButton.UseVisualStyleBackColor = false;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // AuthorizationToReleaseFingerprintsFormLabel
            // 
            this.AuthorizationToReleaseFingerprintsFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.AuthorizationToReleaseFingerprintsFormLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AuthorizationToReleaseFingerprintsFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorizationToReleaseFingerprintsFormLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.AuthorizationToReleaseFingerprintsFormLabel.Location = new System.Drawing.Point(32, 24);
            this.AuthorizationToReleaseFingerprintsFormLabel.Name = "AuthorizationToReleaseFingerprintsFormLabel";
            this.AuthorizationToReleaseFingerprintsFormLabel.Size = new System.Drawing.Size(633, 19);
            this.AuthorizationToReleaseFingerprintsFormLabel.TabIndex = 145;
            this.AuthorizationToReleaseFingerprintsFormLabel.Text = "Authorization to Release Customer Fingerprints";
            this.AuthorizationToReleaseFingerprintsFormLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LoanBuyNumberLabel
            // 
            this.LoanBuyNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.LoanBuyNumberLabel.Location = new System.Drawing.Point(35, 89);
            this.LoanBuyNumberLabel.Name = "LoanBuyNumberLabel";
            this.LoanBuyNumberLabel.Size = new System.Drawing.Size(83, 13);
            this.LoanBuyNumberLabel.TabIndex = 146;
            this.LoanBuyNumberLabel.Text = "Loan <Buy> #:";
            this.LoanBuyNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LoanBuyNumberLabelText
            // 
            this.LoanBuyNumberLabelText.AutoSize = true;
            this.LoanBuyNumberLabelText.Location = new System.Drawing.Point(118, 89);
            this.LoanBuyNumberLabelText.Name = "LoanBuyNumberLabelText";
            this.LoanBuyNumberLabelText.Size = new System.Drawing.Size(0, 13);
            this.LoanBuyNumberLabelText.TabIndex = 147;
            // 
            // OfficerFirstNameLabel
            // 
            this.OfficerFirstNameLabel.AutoSize = true;
            this.OfficerFirstNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.OfficerFirstNameLabel.Location = new System.Drawing.Point(136, 117);
            this.OfficerFirstNameLabel.Name = "OfficerFirstNameLabel";
            this.OfficerFirstNameLabel.Size = new System.Drawing.Size(60, 13);
            this.OfficerFirstNameLabel.TabIndex = 149;
            this.OfficerFirstNameLabel.Text = "First Name:";
            // 
            // OfficerLastNameLabel
            // 
            this.OfficerLastNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.OfficerLastNameLabel.Location = new System.Drawing.Point(386, 117);
            this.OfficerLastNameLabel.Name = "OfficerLastNameLabel";
            this.OfficerLastNameLabel.Size = new System.Drawing.Size(61, 13);
            this.OfficerLastNameLabel.TabIndex = 151;
            this.OfficerLastNameLabel.Text = "Last Name:";
            this.OfficerLastNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BadgeNumberText
            // 
            this.BadgeNumberText.Location = new System.Drawing.Point(453, 135);
            this.BadgeNumberText.MaxLength = 15;
            this.BadgeNumberText.Name = "BadgeNumberText";
            this.BadgeNumberText.Size = new System.Drawing.Size(158, 20);
            this.BadgeNumberText.TabIndex = 3;
            // 
            // BadgeNumberLabel
            // 
            this.BadgeNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.BadgeNumberLabel.Location = new System.Drawing.Point(386, 142);
            this.BadgeNumberLabel.Name = "BadgeNumberLabel";
            this.BadgeNumberLabel.Size = new System.Drawing.Size(61, 13);
            this.BadgeNumberLabel.TabIndex = 155;
            this.BadgeNumberLabel.Text = "Badge #:";
            this.BadgeNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AgencyText
            // 
            this.AgencyText.Location = new System.Drawing.Point(139, 135);
            this.AgencyText.MaxLength = 15;
            this.AgencyText.Name = "AgencyText";
            this.AgencyText.Size = new System.Drawing.Size(222, 20);
            this.AgencyText.TabIndex = 2;
            // 
            // AgencyLabel
            // 
            this.AgencyLabel.BackColor = System.Drawing.Color.Transparent;
            this.AgencyLabel.Location = new System.Drawing.Point(35, 142);
            this.AgencyLabel.Name = "AgencyLabel";
            this.AgencyLabel.Size = new System.Drawing.Size(83, 13);
            this.AgencyLabel.TabIndex = 153;
            this.AgencyLabel.Text = "Agency:";
            this.AgencyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CaseNumberText
            // 
            this.CaseNumberText.Location = new System.Drawing.Point(453, 161);
            this.CaseNumberText.MaxLength = 15;
            this.CaseNumberText.Name = "CaseNumberText";
            this.CaseNumberText.Size = new System.Drawing.Size(158, 20);
            this.CaseNumberText.TabIndex = 5;
            // 
            // CaseNumberLabel
            // 
            this.CaseNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.CaseNumberLabel.Location = new System.Drawing.Point(386, 164);
            this.CaseNumberLabel.Name = "CaseNumberLabel";
            this.CaseNumberLabel.Size = new System.Drawing.Size(61, 13);
            this.CaseNumberLabel.TabIndex = 159;
            this.CaseNumberLabel.Text = "Case #:";
            this.CaseNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MerchandiseDataGridView
            // 
            this.MerchandiseDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MerchandiseDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.MerchandiseDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.MerchandiseDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MerchandiseDataGridView.CausesValidation = false;
            this.MerchandiseDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.MerchandiseDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MerchandiseDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MerchandiseDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MerchandiseDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNumber,
            this.ICN,
            this.Merchandise});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MerchandiseDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.MerchandiseDataGridView.Location = new System.Drawing.Point(38, 257);
            this.MerchandiseDataGridView.Name = "MerchandiseDataGridView";
            this.MerchandiseDataGridView.ReadOnly = true;
            this.MerchandiseDataGridView.RowHeadersVisible = false;
            this.MerchandiseDataGridView.RowTemplate.Height = 25;
            this.MerchandiseDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.MerchandiseDataGridView.Size = new System.Drawing.Size(627, 198);
            this.MerchandiseDataGridView.TabIndex = 163;
            this.MerchandiseDataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MerchandiseDataGridView_CellMouseUp);
            // 
            // RowNumber
            // 
            this.RowNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RowNumber.DataPropertyName = "mItemOrder";
            this.RowNumber.FillWeight = 76.14214F;
            this.RowNumber.HeaderText = "#";
            this.RowNumber.Name = "RowNumber";
            this.RowNumber.ReadOnly = true;
            this.RowNumber.Width = 50;
            // 
            // ICN
            // 
            this.ICN.DataPropertyName = "ICN";
            this.ICN.FillWeight = 57.66773F;
            this.ICN.HeaderText = "ICN";
            this.ICN.Name = "ICN";
            this.ICN.ReadOnly = true;
            // 
            // Merchandise
            // 
            this.Merchandise.DataPropertyName = "TicketDescription";
            this.Merchandise.FillWeight = 154.2612F;
            this.Merchandise.HeaderText = "Merchandise";
            this.Merchandise.Name = "Merchandise";
            this.Merchandise.ReadOnly = true;
            // 
            // labelOfficerFirstName
            // 
            this.labelOfficerFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOfficerFirstName.AutoSize = true;
            this.labelOfficerFirstName.BackColor = System.Drawing.Color.Transparent;
            this.labelOfficerFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOfficerFirstName.Location = new System.Drawing.Point(74, 117);
            this.labelOfficerFirstName.Name = "labelOfficerFirstName";
            this.labelOfficerFirstName.Required = true;
            this.labelOfficerFirstName.Size = new System.Drawing.Size(44, 13);
            this.labelOfficerFirstName.TabIndex = 164;
            this.labelOfficerFirstName.Text = "Officer:";
            // 
            // customLabel1
            // 
            this.customLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(48, 168);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Required = true;
            this.customLabel1.Size = new System.Drawing.Size(70, 13);
            this.customLabel1.TabIndex = 165;
            this.customLabel1.Text = "Subpoena #:";
            // 
            // customLabel2
            // 
            this.customLabel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(62, 194);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Required = true;
            this.customLabel2.Size = new System.Drawing.Size(56, 13);
            this.customLabel2.TabIndex = 166;
            this.customLabel2.Text = "Comment:";
            // 
            // OfficerFirstNameText
            // 
            this.OfficerFirstNameText.CausesValidation = false;
            this.OfficerFirstNameText.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.OfficerFirstNameText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OfficerFirstNameText.Location = new System.Drawing.Point(203, 108);
            this.OfficerFirstNameText.MaxLength = 20;
            this.OfficerFirstNameText.Name = "OfficerFirstNameText";
            this.OfficerFirstNameText.Required = true;
            this.OfficerFirstNameText.Size = new System.Drawing.Size(158, 21);
            this.OfficerFirstNameText.TabIndex = 0;
            this.OfficerFirstNameText.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.OfficerFirstNameText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnFormPreviewKeyDown);
            // 
            // OfficerLastNameText
            // 
            this.OfficerLastNameText.CausesValidation = false;
            this.OfficerLastNameText.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.OfficerLastNameText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OfficerLastNameText.Location = new System.Drawing.Point(453, 108);
            this.OfficerLastNameText.MaxLength = 20;
            this.OfficerLastNameText.Name = "OfficerLastNameText";
            this.OfficerLastNameText.Required = true;
            this.OfficerLastNameText.Size = new System.Drawing.Size(158, 21);
            this.OfficerLastNameText.TabIndex = 1;
            this.OfficerLastNameText.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.OfficerLastNameText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnFormPreviewKeyDown);
            // 
            // SubpoenaNumberText
            // 
            this.SubpoenaNumberText.CausesValidation = false;
            this.SubpoenaNumberText.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.SubpoenaNumberText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubpoenaNumberText.Location = new System.Drawing.Point(139, 160);
            this.SubpoenaNumberText.MaxLength = 15;
            this.SubpoenaNumberText.Name = "SubpoenaNumberText";
            this.SubpoenaNumberText.Required = true;
            this.SubpoenaNumberText.Size = new System.Drawing.Size(222, 21);
            this.SubpoenaNumberText.TabIndex = 4;
            this.SubpoenaNumberText.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.SubpoenaNumberText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnFormPreviewKeyDown);
            // 
            // CommentText
            // 
            this.CommentText.CausesValidation = false;
            this.CommentText.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.CommentText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentText.Location = new System.Drawing.Point(139, 186);
            this.CommentText.MaxLength = 100;
            this.CommentText.Name = "CommentText";
            this.CommentText.Required = true;
            this.CommentText.Size = new System.Drawing.Size(472, 21);
            this.CommentText.TabIndex = 6;
            this.CommentText.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.CommentText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnFormPreviewKeyDown);
            // 
            // ReleaseFingerprintsAuthorization
            // 
            this.AcceptButton = this.SubmitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(704, 566);
            this.Controls.Add(this.CommentText);
            this.Controls.Add(this.SubpoenaNumberText);
            this.Controls.Add(this.OfficerLastNameText);
            this.Controls.Add(this.OfficerFirstNameText);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.labelOfficerFirstName);
            this.Controls.Add(this.MerchandiseDataGridView);
            this.Controls.Add(this.CaseNumberText);
            this.Controls.Add(this.CaseNumberLabel);
            this.Controls.Add(this.BadgeNumberText);
            this.Controls.Add(this.BadgeNumberLabel);
            this.Controls.Add(this.AgencyText);
            this.Controls.Add(this.AgencyLabel);
            this.Controls.Add(this.OfficerLastNameLabel);
            this.Controls.Add(this.OfficerFirstNameLabel);
            this.Controls.Add(this.LoanBuyNumberLabelText);
            this.Controls.Add(this.LoanBuyNumberLabel);
            this.Controls.Add(this.AuthorizationToReleaseFingerprintsFormLabel);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.buttonCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ReleaseFingerprintsAuthorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReleaseFingerprintsAuthorization";
            this.Load += new System.EventHandler(this.ReleaseFingerprintsAuthorization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MerchandiseDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Label AuthorizationToReleaseFingerprintsFormLabel;
        private System.Windows.Forms.Label LoanBuyNumberLabel;
        private System.Windows.Forms.Label LoanBuyNumberLabelText;
        private System.Windows.Forms.Label OfficerFirstNameLabel;
        private System.Windows.Forms.Label OfficerLastNameLabel;
        private System.Windows.Forms.TextBox BadgeNumberText;
        private System.Windows.Forms.Label BadgeNumberLabel;
        private System.Windows.Forms.TextBox AgencyText;
        private System.Windows.Forms.Label AgencyLabel;
        private System.Windows.Forms.TextBox CaseNumberText;
        private System.Windows.Forms.Label CaseNumberLabel;
        private System.Windows.Forms.DataGridView MerchandiseDataGridView;
        private Common.Libraries.Forms.Components.CustomLabel labelOfficerFirstName;
        private Common.Libraries.Forms.Components.CustomLabel customLabel1;
        private Common.Libraries.Forms.Components.CustomLabel customLabel2;
        private Common.Libraries.Forms.Components.CustomTextBox OfficerFirstNameText;
        private Common.Libraries.Forms.Components.CustomTextBox OfficerLastNameText;
        private Common.Libraries.Forms.Components.CustomTextBox SubpoenaNumberText;
        private Common.Libraries.Forms.Components.CustomTextBox CommentText;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNumber;
        private System.Windows.Forms.DataGridViewLinkColumn ICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Merchandise;
    }
}
