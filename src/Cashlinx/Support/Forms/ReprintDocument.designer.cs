using Common.Libraries.Forms.Components;
using Support.Forms.UserControls;

namespace Support.Forms
{
    partial class ReprintDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReprintDocument));
            this.reprintFormHeaderLabel = new System.Windows.Forms.Label();
            this.customLabel1 = new Common.Libraries.Forms.Components.CustomLabel();
            this.documentNameLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.viewDocButton = new Common.Libraries.Forms.Components.CustomButton();
            this.printDocButton = new Common.Libraries.Forms.Components.CustomButton();
            this.cancelButton = new Common.Libraries.Forms.Components.CustomButton();
            this.lblProduct = new Common.Libraries.Forms.Components.CustomLabel();
            this.lblTicket = new Common.Libraries.Forms.Components.CustomLabel();
            this.txtTicketNumber = new System.Windows.Forms.TextBox();
            this.productTypeList1 = new ProductTypeList();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reprintFormHeaderLabel
            // 
            this.reprintFormHeaderLabel.AutoSize = true;
            this.reprintFormHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.reprintFormHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reprintFormHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.reprintFormHeaderLabel.Location = new System.Drawing.Point(134, 19);
            this.reprintFormHeaderLabel.Name = "reprintFormHeaderLabel";
            this.reprintFormHeaderLabel.Size = new System.Drawing.Size(137, 19);
            this.reprintFormHeaderLabel.TabIndex = 0;
            this.reprintFormHeaderLabel.Text = "Reprint Document";
            // 
            // customLabel1
            // 
            this.customLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.customLabel1.Location = new System.Drawing.Point(100, 10);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(89, 13);
            this.customLabel1.TabIndex = 1;
            this.customLabel1.Text = "Document Name:";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // documentNameLabel
            // 
            this.documentNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.documentNameLabel.AutoSize = true;
            this.documentNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.documentNameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentNameLabel.Location = new System.Drawing.Point(195, 10);
            this.documentNameLabel.Name = "documentNameLabel";
            this.documentNameLabel.Size = new System.Drawing.Size(89, 13);
            this.documentNameLabel.TabIndex = 2;
            this.documentNameLabel.Text = "DOC NAME HERE";
            // 
            // viewDocButton
            // 
            this.viewDocButton.BackColor = System.Drawing.Color.Transparent;
            this.viewDocButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("viewDocButton.BackgroundImage")));
            this.viewDocButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.viewDocButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.viewDocButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.viewDocButton.FlatAppearance.BorderSize = 0;
            this.viewDocButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.viewDocButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.viewDocButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewDocButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewDocButton.ForeColor = System.Drawing.Color.White;
            this.viewDocButton.Location = new System.Drawing.Point(200, 169);
            this.viewDocButton.Margin = new System.Windows.Forms.Padding(0);
            this.viewDocButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.viewDocButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.viewDocButton.Name = "viewDocButton";
            this.viewDocButton.Size = new System.Drawing.Size(100, 50);
            this.viewDocButton.TabIndex = 7;
            this.viewDocButton.Text = "View";
            this.viewDocButton.UseVisualStyleBackColor = false;
            this.viewDocButton.Click += new System.EventHandler(this.viewDocButton_Click);
            // 
            // printDocButton
            // 
            this.printDocButton.BackColor = System.Drawing.Color.Transparent;
            this.printDocButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("printDocButton.BackgroundImage")));
            this.printDocButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.printDocButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.printDocButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.printDocButton.FlatAppearance.BorderSize = 0;
            this.printDocButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.printDocButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.printDocButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printDocButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printDocButton.ForeColor = System.Drawing.Color.White;
            this.printDocButton.Location = new System.Drawing.Point(296, 169);
            this.printDocButton.Margin = new System.Windows.Forms.Padding(0);
            this.printDocButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.printDocButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.printDocButton.Name = "printDocButton";
            this.printDocButton.Size = new System.Drawing.Size(100, 50);
            this.printDocButton.TabIndex = 8;
            this.printDocButton.Text = "Print";
            this.printDocButton.UseVisualStyleBackColor = false;
            this.printDocButton.Click += new System.EventHandler(this.printDocButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(9, 169);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblProduct
            // 
            this.lblProduct.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProduct.AutoSize = true;
            this.lblProduct.BackColor = System.Drawing.Color.Transparent;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProduct.Location = new System.Drawing.Point(141, 43);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(48, 13);
            this.lblProduct.TabIndex = 3;
            this.lblProduct.Text = "Product:";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTicket
            // 
            this.lblTicket.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTicket.AutoSize = true;
            this.lblTicket.BackColor = System.Drawing.Color.Transparent;
            this.lblTicket.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTicket.Location = new System.Drawing.Point(110, 76);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(79, 13);
            this.lblTicket.TabIndex = 5;
            this.lblTicket.Text = "Ticket Number:";
            this.lblTicket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTicket.Click += new System.EventHandler(this.customLabel3_Click);
            // 
            // txtTicketNumber
            // 
            this.txtTicketNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTicketNumber.Location = new System.Drawing.Point(195, 72);
            this.txtTicketNumber.Name = "txtTicketNumber";
            this.txtTicketNumber.Size = new System.Drawing.Size(135, 21);
            this.txtTicketNumber.TabIndex = 6;
            // 
            // productTypeList1
            // 
            this.productTypeList1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.productTypeList1.Location = new System.Drawing.Point(195, 39);
            this.productTypeList1.Name = "productTypeList1";
            this.productTypeList1.Size = new System.Drawing.Size(136, 21);
            this.productTypeList1.TabIndex = 4;
            this.productTypeList1.SelectedIndexChanged += new System.EventHandler(this.productTypeList1_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.customLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtTicketNumber, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.productTypeList1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblProduct, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTicket, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.documentNameLabel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 58);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 100);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // ReprintDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(405, 228);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.printDocButton);
            this.Controls.Add(this.viewDocButton);
            this.Controls.Add(this.reprintFormHeaderLabel);
            this.Name = "ReprintDocument";
            this.Text = "Reprint Document";
            this.Load += new System.EventHandler(this.ReprintDocument_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reprintFormHeaderLabel;
        private CustomLabel customLabel1;
        private CustomLabel documentNameLabel;
        private CustomButton viewDocButton;
        private CustomButton printDocButton;
        private CustomButton cancelButton;
        private CustomLabel lblProduct;
        private CustomLabel lblTicket;
        private System.Windows.Forms.TextBox txtTicketNumber;
        private ProductTypeList productTypeList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}