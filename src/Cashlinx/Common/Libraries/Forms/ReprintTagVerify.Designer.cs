using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms
{
    partial class ReprintTagVerify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReprintTagVerify));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRetailPrice = new System.Windows.Forms.Label();
            this.lblDocType = new System.Windows.Forms.Label();
            this.lblTicketNumber = new System.Windows.Forms.Label();
            this.lblIcn = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.btnComplete = new CustomButton();
            this.btnCancel = new CustomButton();
            this.chkPrintFeaturesTag = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTagsToReprint = new CustomTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailPrice, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDocType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTicketNumber, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblIcn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtItemDescription, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(512, 191);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Location = new System.Drawing.Point(131, 106);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 13);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "label12";
            // 
            // lblRetailPrice
            // 
            this.lblRetailPrice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRetailPrice.AutoSize = true;
            this.lblRetailPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblRetailPrice.Location = new System.Drawing.Point(131, 81);
            this.lblRetailPrice.Name = "lblRetailPrice";
            this.lblRetailPrice.Size = new System.Drawing.Size(41, 13);
            this.lblRetailPrice.TabIndex = 7;
            this.lblRetailPrice.Text = "label11";
            // 
            // lblDocType
            // 
            this.lblDocType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDocType.AutoSize = true;
            this.lblDocType.BackColor = System.Drawing.Color.Transparent;
            this.lblDocType.Location = new System.Drawing.Point(131, 56);
            this.lblDocType.Name = "lblDocType";
            this.lblDocType.Size = new System.Drawing.Size(41, 13);
            this.lblDocType.TabIndex = 5;
            this.lblDocType.Text = "label10";
            // 
            // lblTicketNumber
            // 
            this.lblTicketNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTicketNumber.AutoSize = true;
            this.lblTicketNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblTicketNumber.Location = new System.Drawing.Point(131, 31);
            this.lblTicketNumber.Name = "lblTicketNumber";
            this.lblTicketNumber.Size = new System.Drawing.Size(35, 13);
            this.lblTicketNumber.TabIndex = 3;
            this.lblTicketNumber.Text = "label9";
            // 
            // lblIcn
            // 
            this.lblIcn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIcn.AutoSize = true;
            this.lblIcn.BackColor = System.Drawing.Color.Transparent;
            this.lblIcn.Location = new System.Drawing.Point(131, 6);
            this.lblIcn.Name = "lblIcn";
            this.lblIcn.Size = new System.Drawing.Size(35, 13);
            this.lblIcn.TabIndex = 1;
            this.lblIcn.Text = "label8";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Barcode Number (ICN)";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(50, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ticket Number";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(73, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Doc Type";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(65, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Retail Price";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(87, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Status";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(40, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Item Description";
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemDescription.Location = new System.Drawing.Point(131, 128);
            this.txtItemDescription.Multiline = true;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ReadOnly = true;
            this.txtItemDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtItemDescription.Size = new System.Drawing.Size(378, 60);
            this.txtItemDescription.TabIndex = 11;
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComplete.BackColor = System.Drawing.Color.Transparent;
            this.btnComplete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnComplete.BackgroundImage")));
            this.btnComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComplete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnComplete.FlatAppearance.BorderSize = 0;
            this.btnComplete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(424, 261);
            this.btnComplete.Margin = new System.Windows.Forms.Padding(0);
            this.btnComplete.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnComplete.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(100, 50);
            this.btnComplete.TabIndex = 6;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(324, 261);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkPrintFeaturesTag
            // 
            this.chkPrintFeaturesTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPrintFeaturesTag.AutoSize = true;
            this.chkPrintFeaturesTag.BackColor = System.Drawing.Color.Transparent;
            this.chkPrintFeaturesTag.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrintFeaturesTag.Location = new System.Drawing.Point(44, 265);
            this.chkPrintFeaturesTag.Name = "chkPrintFeaturesTag";
            this.chkPrintFeaturesTag.Size = new System.Drawing.Size(172, 17);
            this.chkPrintFeaturesTag.TabIndex = 2;
            this.chkPrintFeaturesTag.Text = "Print features tag if available :";
            this.chkPrintFeaturesTag.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(15, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "How many barcode tags to reprint? :";
            // 
            // txtTagsToReprint
            // 
            this.txtTagsToReprint.AllowOnlyNumbers = true;
            this.txtTagsToReprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTagsToReprint.CausesValidation = false;
            this.txtTagsToReprint.ErrorMessage = "";
            this.txtTagsToReprint.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTagsToReprint.Location = new System.Drawing.Point(202, 284);
            this.txtTagsToReprint.Name = "txtTagsToReprint";
            this.txtTagsToReprint.Size = new System.Drawing.Size(66, 21);
            this.txtTagsToReprint.TabIndex = 4;
            this.txtTagsToReprint.ValidationExpression = "";
            this.txtTagsToReprint.Leave += new System.EventHandler(this.txtTagsToReprint_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(188, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "Barcode Tag Reprint";
            // 
            // ReprintTagVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(536, 320);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTagsToReprint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkPrintFeaturesTag);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(536, 780);
            this.Name = "ReprintTagVerify";
            this.Text = "ReprintTag";
            this.Load += new System.EventHandler(this.ReprintTag_Load);
            this.Shown += new System.EventHandler(this.ReprintTagVerify_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton btnComplete;
        private CustomButton btnCancel;
        private System.Windows.Forms.CheckBox chkPrintFeaturesTag;
        private System.Windows.Forms.Label label1;
        private CustomTextBox txtTagsToReprint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblIcn;
        private System.Windows.Forms.Label lblTicketNumber;
        private System.Windows.Forms.Label lblDocType;
        private System.Windows.Forms.Label lblRetailPrice;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtItemDescription;
    }
}