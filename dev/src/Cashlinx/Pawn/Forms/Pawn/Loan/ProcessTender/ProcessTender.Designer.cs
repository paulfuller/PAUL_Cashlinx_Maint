namespace Pawn.Forms.Pawn.Loan.ProcessTender
{
    partial class ProcessTender
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessTender));
            this.loanDocListView = new System.Windows.Forms.ListView();
            this.pdfIconList = new System.Windows.Forms.ImageList(this.components);
            this.processTenderLabel = new System.Windows.Forms.Label();
            this.initialProcessTenderLabel = new System.Windows.Forms.Label();
            this.disburseLabel = new System.Windows.Forms.Label();
            this.customerNameLabel = new System.Windows.Forms.Label();
            this.customerNameValueLabel = new System.Windows.Forms.Label();
            this.loanTicketGroup = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.viewButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loanDocListView
            // 
            this.loanDocListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.loanDocListView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loanDocListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanDocListView.ForeColor = System.Drawing.Color.Black;
            this.loanDocListView.LargeImageList = this.pdfIconList;
            this.loanDocListView.Location = new System.Drawing.Point(12, 187);
            this.loanDocListView.MultiSelect = false;
            this.loanDocListView.Name = "loanDocListView";
            this.loanDocListView.Size = new System.Drawing.Size(533, 93);
            this.loanDocListView.TabIndex = 7;
            this.loanDocListView.UseCompatibleStateImageBehavior = false;
            this.loanDocListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.loanDocListView_ItemSelectionChanged);
            this.loanDocListView.SelectedIndexChanged += new System.EventHandler(this.loanDocListView_SelectedIndexChanged);
            this.loanDocListView.Leave += new System.EventHandler(this.loanDocListView_Leave);
            // 
            // pdfIconList
            // 
            this.pdfIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pdfIconList.ImageStream")));
            this.pdfIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.pdfIconList.Images.SetKeyName(0, "pdf_icon.png");
            // 
            // processTenderLabel
            // 
            this.processTenderLabel.AutoSize = true;
            this.processTenderLabel.BackColor = System.Drawing.Color.Transparent;
            this.processTenderLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.processTenderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processTenderLabel.ForeColor = System.Drawing.Color.White;
            this.processTenderLabel.Location = new System.Drawing.Point(220, 25);
            this.processTenderLabel.Name = "processTenderLabel";
            this.processTenderLabel.Size = new System.Drawing.Size(117, 19);
            this.processTenderLabel.TabIndex = 1;
            this.processTenderLabel.Text = "Process Tender";
            this.processTenderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // initialProcessTenderLabel
            // 
            this.initialProcessTenderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.initialProcessTenderLabel.BackColor = System.Drawing.Color.Transparent;
            this.initialProcessTenderLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initialProcessTenderLabel.ForeColor = System.Drawing.Color.Black;
            this.initialProcessTenderLabel.Location = new System.Drawing.Point(41, 133);
            this.initialProcessTenderLabel.Name = "initialProcessTenderLabel";
            this.initialProcessTenderLabel.Size = new System.Drawing.Size(174, 16);
            this.initialProcessTenderLabel.TabIndex = 8;
            this.initialProcessTenderLabel.Text = "Disbursement Amount:";
            this.initialProcessTenderLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // disburseLabel
            // 
            this.disburseLabel.AutoSize = true;
            this.disburseLabel.BackColor = System.Drawing.Color.Transparent;
            this.disburseLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disburseLabel.ForeColor = System.Drawing.Color.Black;
            this.disburseLabel.Location = new System.Drawing.Point(224, 125);
            this.disburseLabel.Margin = new System.Windows.Forms.Padding(0);
            this.disburseLabel.MaximumSize = new System.Drawing.Size(200, 32);
            this.disburseLabel.MinimumSize = new System.Drawing.Size(100, 32);
            this.disburseLabel.Name = "disburseLabel";
            this.disburseLabel.Size = new System.Drawing.Size(100, 32);
            this.disburseLabel.TabIndex = 9;
            this.disburseLabel.Text = "Amount";
            this.disburseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customerNameLabel
            // 
            this.customerNameLabel.AutoSize = true;
            this.customerNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.customerNameLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerNameLabel.ForeColor = System.Drawing.Color.Black;
            this.customerNameLabel.Location = new System.Drawing.Point(109, 99);
            this.customerNameLabel.Name = "customerNameLabel";
            this.customerNameLabel.Size = new System.Drawing.Size(105, 16);
            this.customerNameLabel.TabIndex = 10;
            this.customerNameLabel.Text = "Customer Name:";
            this.customerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customerNameValueLabel
            // 
            this.customerNameValueLabel.AutoSize = true;
            this.customerNameValueLabel.BackColor = System.Drawing.Color.Transparent;
            this.customerNameValueLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerNameValueLabel.ForeColor = System.Drawing.Color.Black;
            this.customerNameValueLabel.Location = new System.Drawing.Point(224, 92);
            this.customerNameValueLabel.MaximumSize = new System.Drawing.Size(256, 32);
            this.customerNameValueLabel.MinimumSize = new System.Drawing.Size(256, 32);
            this.customerNameValueLabel.Name = "customerNameValueLabel";
            this.customerNameValueLabel.Size = new System.Drawing.Size(256, 32);
            this.customerNameValueLabel.TabIndex = 11;
            this.customerNameValueLabel.Text = "FirstName LastName";
            this.customerNameValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // loanTicketGroup
            // 
            this.loanTicketGroup.BackColor = System.Drawing.Color.Transparent;
            this.loanTicketGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanTicketGroup.ForeColor = System.Drawing.Color.Black;
            this.loanTicketGroup.Location = new System.Drawing.Point(5, 170);
            this.loanTicketGroup.Name = "loanTicketGroup";
            this.loanTicketGroup.Size = new System.Drawing.Size(547, 120);
            this.loanTicketGroup.TabIndex = 6;
            this.loanTicketGroup.TabStop = false;
            this.loanTicketGroup.Text = "Loan Ticket(s)";

            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(358, 294);
            this.okButton.Margin = new System.Windows.Forms.Padding(0);
            this.okButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.okButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 50);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // viewButton
            // 
            this.viewButton.BackColor = System.Drawing.Color.Transparent;
            this.viewButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.viewButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.viewButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.viewButton.FlatAppearance.BorderSize = 0;
            this.viewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.viewButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.viewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewButton.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.viewButton.ForeColor = System.Drawing.Color.White;
            this.viewButton.Location = new System.Drawing.Point(98, 294);
            this.viewButton.Margin = new System.Windows.Forms.Padding(0);
            this.viewButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.viewButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.viewButton.Name = "viewButton";
            this.viewButton.Size = new System.Drawing.Size(100, 50);
            this.viewButton.TabIndex = 1;
            this.viewButton.Text = "View";
            this.viewButton.UseVisualStyleBackColor = false;
            this.viewButton.Click += new System.EventHandler(this.viewButton_Click);
            // 
            // ProcessTender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(557, 357);
            this.Controls.Add(this.customerNameLabel);
            this.Controls.Add(this.viewButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.customerNameValueLabel);
            this.Controls.Add(this.disburseLabel);
            this.Controls.Add(this.initialProcessTenderLabel);
            this.Controls.Add(this.processTenderLabel);
            this.Controls.Add(this.loanDocListView);
            this.Controls.Add(this.loanTicketGroup);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProcessTender";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Tender";
            this.Load += new System.EventHandler(this.ProcessTender_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView loanDocListView;
        private System.Windows.Forms.ImageList pdfIconList;
        private System.Windows.Forms.Label processTenderLabel;
        private System.Windows.Forms.Label initialProcessTenderLabel;
        private System.Windows.Forms.Label disburseLabel;
        private System.Windows.Forms.Label customerNameLabel;
        private System.Windows.Forms.Label customerNameValueLabel;
        private System.Windows.Forms.GroupBox loanTicketGroup;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button viewButton;
    }
}
