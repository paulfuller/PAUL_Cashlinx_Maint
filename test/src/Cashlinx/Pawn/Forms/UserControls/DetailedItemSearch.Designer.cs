using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls
{
    partial class DetailedItemSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedItemSearch));
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModelNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdCategoryID = new CustomButton();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.BackColor = System.Drawing.Color.Transparent;
            this.lblManufacturer.Location = new System.Drawing.Point(245, 69);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(70, 13);
            this.lblManufacturer.TabIndex = 3;
            this.lblManufacturer.Text = "Manufacturer";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(419, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Model #";
            // 
            // txtModelNumber
            // 
            this.txtModelNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtModelNumber.Location = new System.Drawing.Point(471, 66);
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.Size = new System.Drawing.Size(92, 20);
            this.txtModelNumber.TabIndex = 6;
            this.txtModelNumber.TextChanged += new System.EventHandler(this.txtModelNumber_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(571, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Serial #";
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSerialNumber.Location = new System.Drawing.Point(620, 66);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(82, 20);
            this.txtSerialNumber.TabIndex = 8;
            this.txtSerialNumber.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.02837F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.04965F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.90071F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.375886F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.90071F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.234043F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmdCategoryID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSerialNumber, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtModelNumber, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtManufacturer, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblManufacturer, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCategory, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDescription, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(705, 122);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(50, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Description";
            // 
            // cmdCategoryID
            // 
            this.cmdCategoryID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdCategoryID.BackColor = System.Drawing.Color.Transparent;
            this.cmdCategoryID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdCategoryID.BackgroundImage")));
            this.cmdCategoryID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdCategoryID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdCategoryID.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cmdCategoryID.FlatAppearance.BorderSize = 0;
            this.cmdCategoryID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmdCategoryID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdCategoryID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCategoryID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCategoryID.ForeColor = System.Drawing.Color.White;
            this.cmdCategoryID.Location = new System.Drawing.Point(0, 5);
            this.cmdCategoryID.Margin = new System.Windows.Forms.Padding(0);
            this.cmdCategoryID.MaximumSize = new System.Drawing.Size(100, 50);
            this.cmdCategoryID.MinimumSize = new System.Drawing.Size(100, 50);
            this.cmdCategoryID.Name = "cmdCategoryID";
            this.cmdCategoryID.Size = new System.Drawing.Size(100, 50);
            this.cmdCategoryID.TabIndex = 0;
            this.cmdCategoryID.Text = "Desc Item";
            this.cmdCategoryID.UseVisualStyleBackColor = false;
            this.cmdCategoryID.Click += new System.EventHandler(this.cmdCategoryID_Click);
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtManufacturer.Location = new System.Drawing.Point(321, 66);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(92, 20);
            this.txtManufacturer.TabIndex = 4;
            this.txtManufacturer.TextChanged += new System.EventHandler(this.txtManufacturer_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(61, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Category";
            // 
            // txtCategory
            // 
            this.txtCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCategory.Enabled = false;
            this.txtCategory.Location = new System.Drawing.Point(116, 66);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(107, 20);
            this.txtCategory.TabIndex = 2;
            this.txtCategory.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDescription.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.txtDescription, 2);
            this.txtDescription.Location = new System.Drawing.Point(116, 96);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(184, 20);
            this.txtDescription.TabIndex = 10;
            this.txtDescription.TextChanged += new System.EventHandler(this.txt_TextChanged);

            // 
            // DetailedItemSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DetailedItemSearch";
            this.Size = new System.Drawing.Size(711, 122);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DetailedItemSearch_Paint);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomButton cmdCategoryID;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModelNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;


    }
}
