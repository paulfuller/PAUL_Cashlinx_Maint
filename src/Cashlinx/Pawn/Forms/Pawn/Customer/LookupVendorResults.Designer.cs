using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer
{
    partial class LookupVendorResults
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupVendorResults));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lookupVendorResultsGrid = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewImageColumn();
            this.vendor_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vend_tax_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vend_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vend_contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vend_phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorLabel = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonBack = new CustomButton();
            this.customButtonAddVendor = new CustomButton();
            this.nextPage = new CustomButtonTiny();
            this.prevPage = new CustomButtonTiny();
            this.pageInd = new CustomLabel();
            this.lastPage = new CustomButtonTiny();
            this.firstPage = new CustomButtonTiny();
            ((System.ComponentModel.ISupportInitialize)(this.lookupVendorResultsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(349, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Vendor";
            // 
            // groupBox1
            // 
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(10, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 2);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lookupVendorResultsGrid
            // 
            this.lookupVendorResultsGrid.AllowUserToAddRows = false;
            this.lookupVendorResultsGrid.AllowUserToDeleteRows = false;
            this.lookupVendorResultsGrid.AllowUserToResizeColumns = false;
            this.lookupVendorResultsGrid.AllowUserToResizeRows = false;
            this.lookupVendorResultsGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.lookupVendorResultsGrid.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lookupVendorResultsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.lookupVendorResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lookupVendorResultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.vendor_ID,
            this.vend_tax_id,
            this.vend_name,
            this.Address,
            this.vend_contact,
            this.vend_phone});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.lookupVendorResultsGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.lookupVendorResultsGrid.GridColor = System.Drawing.Color.Black;
            this.lookupVendorResultsGrid.Location = new System.Drawing.Point(13, 89);
            this.lookupVendorResultsGrid.MultiSelect = false;
            this.lookupVendorResultsGrid.Name = "lookupVendorResultsGrid";
            this.lookupVendorResultsGrid.ReadOnly = true;
            this.lookupVendorResultsGrid.RowHeadersVisible = false;
            this.lookupVendorResultsGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.lookupVendorResultsGrid.Size = new System.Drawing.Size(796, 365);
            this.lookupVendorResultsGrid.TabIndex = 2;
            this.lookupVendorResultsGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lookupVendorResultsGrid_CellDoubleClick);
            this.lookupVendorResultsGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataSort);
            this.lookupVendorResultsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lookupCustomerResultsGrid_CellClick);
            // 
            // select
            // 
            this.select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            this.select.DefaultCellStyle = dataGridViewCellStyle2;
            this.select.FillWeight = 50F;
            this.select.HeaderText = "Select";
            this.select.Image = global::Pawn.Properties.Resources.blueglossy_select2;
            this.select.Name = "select";
            this.select.ReadOnly = true;
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.select.Width = 48;
            // 
            // vendor_ID
            // 
            this.vendor_ID.HeaderText = "ID";
            this.vendor_ID.Name = "vendor_ID";
            this.vendor_ID.ReadOnly = true;
            this.vendor_ID.Visible = false;
            // 
            // vend_tax_id
            // 
            this.vend_tax_id.HeaderText = "Tax ID Number";
            this.vend_tax_id.MinimumWidth = 15;
            this.vend_tax_id.Name = "vend_tax_id";
            this.vend_tax_id.ReadOnly = true;
            this.vend_tax_id.Width = 90;
            // 
            // vend_name
            // 
            this.vend_name.HeaderText = "Vendor Name";
            this.vend_name.MinimumWidth = 40;
            this.vend_name.Name = "vend_name";
            this.vend_name.ReadOnly = true;
            this.vend_name.Width = 200;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.MinimumWidth = 50;
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 258;
            // 
            // vend_contact
            // 
            this.vend_contact.HeaderText = "Contact";
            this.vend_contact.MinimumWidth = 40;
            this.vend_contact.Name = "vend_contact";
            this.vend_contact.ReadOnly = true;
            // 
            // vend_phone
            // 
            this.vend_phone.HeaderText = "Phone";
            this.vend_phone.MinimumWidth = 20;
            this.vend_phone.Name = "vend_phone";
            this.vend_phone.ReadOnly = true;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.CausesValidation = false;
            this.errorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(13, 73);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(31, 13);
            this.errorLabel.TabIndex = 10;
            this.errorLabel.Text = "Error";
            this.errorLabel.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Customer Last Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Customer First Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Address";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Date Of Birth";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "ID Type";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "ID Number";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Issuer";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Customer Phone Number";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // customButtonBack
            // 
            this.customButtonBack.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBack.BackgroundImage")));
            this.customButtonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBack.FlatAppearance.BorderSize = 0;
            this.customButtonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBack.ForeColor = System.Drawing.Color.White;
            this.customButtonBack.Location = new System.Drawing.Point(14, 481);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 11;
            this.customButtonBack.Text = "&Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Click += new System.EventHandler(this.lookupVendorBackButton_Click);
            // 
            // customButtonAddVendor
            // 
            this.customButtonAddVendor.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAddVendor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonAddVendor.BackgroundImage")));
            this.customButtonAddVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAddVendor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAddVendor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAddVendor.FlatAppearance.BorderSize = 0;
            this.customButtonAddVendor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddVendor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAddVendor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAddVendor.ForeColor = System.Drawing.Color.White;
            this.customButtonAddVendor.Location = new System.Drawing.Point(125, 481);
            this.customButtonAddVendor.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAddVendor.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddVendor.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddVendor.Name = "customButtonAddVendor";
            this.customButtonAddVendor.Size = new System.Drawing.Size(100, 50);
            this.customButtonAddVendor.TabIndex = 12;
            this.customButtonAddVendor.Text = "&Add Vendor";
            this.customButtonAddVendor.UseVisualStyleBackColor = false;
            this.customButtonAddVendor.Click += new System.EventHandler(this.addVendorButton_Click);
            // 
            // nextPage
            // 
            this.nextPage.BackColor = System.Drawing.Color.Transparent;
            this.nextPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextPage.BackgroundImage")));
            this.nextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nextPage.FlatAppearance.BorderSize = 0;
            this.nextPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.nextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.nextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextPage.ForeColor = System.Drawing.Color.White;
            this.nextPage.Location = new System.Drawing.Point(503, 457);
            this.nextPage.Margin = new System.Windows.Forms.Padding(0);
            this.nextPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.nextPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.nextPage.Name = "nextPage";
            this.nextPage.Size = new System.Drawing.Size(75, 35);
            this.nextPage.TabIndex = 13;
            this.nextPage.Text = ">   ";
            this.nextPage.UseVisualStyleBackColor = false;
            this.nextPage.Visible = false;
            this.nextPage.Click += new System.EventHandler(this.nextPage_Click);
            // 
            // prevPage
            // 
            this.prevPage.BackColor = System.Drawing.Color.Transparent;
            this.prevPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("prevPage.BackgroundImage")));
            this.prevPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.prevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.prevPage.FlatAppearance.BorderSize = 0;
            this.prevPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.prevPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.prevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevPage.ForeColor = System.Drawing.Color.White;
            this.prevPage.Location = new System.Drawing.Point(353, 457);
            this.prevPage.Margin = new System.Windows.Forms.Padding(0);
            this.prevPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.prevPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.prevPage.Name = "prevPage";
            this.prevPage.Size = new System.Drawing.Size(75, 35);
            this.prevPage.TabIndex = 14;
            this.prevPage.Text = "<   ";
            this.prevPage.UseVisualStyleBackColor = false;
            this.prevPage.Visible = false;
            this.prevPage.Click += new System.EventHandler(this.prevPage_Click);
            // 
            // pageInd
            // 
            this.pageInd.AutoSize = true;
            this.pageInd.BackColor = System.Drawing.Color.Transparent;
            this.pageInd.Location = new System.Drawing.Point(419, 466);
            this.pageInd.Name = "pageInd";
            this.pageInd.Size = new System.Drawing.Size(78, 13);
            this.pageInd.TabIndex = 15;
            this.pageInd.Text = "Page {0} of {1}";
            this.pageInd.Visible = false;
            // 
            // lastPage
            // 
            this.lastPage.BackColor = System.Drawing.Color.Transparent;
            this.lastPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lastPage.BackgroundImage")));
            this.lastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lastPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.lastPage.FlatAppearance.BorderSize = 0;
            this.lastPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lastPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.lastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lastPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastPage.ForeColor = System.Drawing.Color.White;
            this.lastPage.Location = new System.Drawing.Point(566, 457);
            this.lastPage.Margin = new System.Windows.Forms.Padding(0);
            this.lastPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.lastPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.lastPage.Name = "lastPage";
            this.lastPage.Size = new System.Drawing.Size(75, 35);
            this.lastPage.TabIndex = 16;
            this.lastPage.Text = ">>   ";
            this.lastPage.UseVisualStyleBackColor = false;
            this.lastPage.Visible = false;
            this.lastPage.Click += new System.EventHandler(this.lastPage_Click);
            // 
            // firstPage
            // 
            this.firstPage.BackColor = System.Drawing.Color.Transparent;
            this.firstPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("firstPage.BackgroundImage")));
            this.firstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.firstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.firstPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.firstPage.FlatAppearance.BorderSize = 0;
            this.firstPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.firstPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.firstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.firstPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstPage.ForeColor = System.Drawing.Color.White;
            this.firstPage.Location = new System.Drawing.Point(295, 457);
            this.firstPage.Margin = new System.Windows.Forms.Padding(0);
            this.firstPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.firstPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.firstPage.Name = "firstPage";
            this.firstPage.Size = new System.Drawing.Size(75, 35);
            this.firstPage.TabIndex = 17;
            this.firstPage.Text = "<<   ";
            this.firstPage.UseVisualStyleBackColor = false;
            this.firstPage.Visible = false;
            this.firstPage.Click += new System.EventHandler(this.firstPage_Click);
            // 
            // LookupVendorResults
            // 
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(820, 540);
            this.ControlBox = false;
            this.Controls.Add(this.pageInd);
            this.Controls.Add(this.prevPage);
            this.Controls.Add(this.firstPage);
            this.Controls.Add(this.lastPage);
            this.Controls.Add(this.nextPage);
            this.Controls.Add(this.customButtonAddVendor);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.lookupVendorResultsGrid);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupVendorResults";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lookup Vendor Results";
            this.Load += new System.EventHandler(this.LookupVendorResults_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookupVendorResultsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;   
        private System.Windows.Forms.DataGridView lookupVendorResultsGrid;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private CustomButton customButtonBack;
        private CustomButton customButtonAddVendor;
        private System.Windows.Forms.DataGridViewImageColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendor_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn vend_tax_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn vend_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn vend_contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn vend_phone;
        private CustomButtonTiny nextPage;
        private CustomButtonTiny prevPage;
        private CustomLabel pageInd;
        private CustomButtonTiny lastPage;
        private CustomButtonTiny firstPage;
    }
}
