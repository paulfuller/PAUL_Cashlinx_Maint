using Common.Libraries.Forms.Components;
//Odd file lock

namespace Pawn.Forms.Pawn.Customer
{
    partial class LookupCustomerResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupCustomerResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lookupCustomerResultsGrid = new System.Windows.Forms.DataGridView();
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
            this.customButtonAddCustomer = new CustomButton();
            this.customButtonView = new CustomButton();
            this.select = new System.Windows.Forms.DataGridViewImageColumn();
            this.cust_last_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_first_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Postal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_of_birth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ident_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issued_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issuer_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lookupCustomerResultsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(439, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Customer";
            // 
            // groupBox1
            // 
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(10, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(979, 2);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lookupCustomerResultsGrid
            // 
            this.lookupCustomerResultsGrid.AllowUserToAddRows = false;
            this.lookupCustomerResultsGrid.AllowUserToDeleteRows = false;
            this.lookupCustomerResultsGrid.AllowUserToResizeColumns = false;
            this.lookupCustomerResultsGrid.AllowUserToResizeRows = false;
            this.lookupCustomerResultsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lookupCustomerResultsGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.lookupCustomerResultsGrid.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lookupCustomerResultsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.lookupCustomerResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lookupCustomerResultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.cust_last_name,
            this.cust_first_name,
            this.Address1,
            this.Address2,
            this.City,
            this.State,
            this.Postal,
            this.date_of_birth,
            this.Ident_desc,
            this.issued_number,
            this.issuer_name,
            this.phone});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.lookupCustomerResultsGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.lookupCustomerResultsGrid.GridColor = System.Drawing.Color.Black;
            this.lookupCustomerResultsGrid.Location = new System.Drawing.Point(13, 105);
            this.lookupCustomerResultsGrid.MultiSelect = false;
            this.lookupCustomerResultsGrid.Name = "lookupCustomerResultsGrid";
            this.lookupCustomerResultsGrid.ReadOnly = true;
            this.lookupCustomerResultsGrid.RowHeadersVisible = false;
            this.lookupCustomerResultsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lookupCustomerResultsGrid.Size = new System.Drawing.Size(976, 375);
            this.lookupCustomerResultsGrid.TabIndex = 2;
            this.lookupCustomerResultsGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lookupCustomerResultsGrid_CellDoubleClick);
            this.lookupCustomerResultsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lookupCustomerResultsGrid_CellClick);
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
            this.customButtonBack.Click += new System.EventHandler(this.lookupCustomerBackButton_Click);
            // 
            // customButtonAddCustomer
            // 
            this.customButtonAddCustomer.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAddCustomer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonAddCustomer.BackgroundImage")));
            this.customButtonAddCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAddCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAddCustomer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAddCustomer.FlatAppearance.BorderSize = 0;
            this.customButtonAddCustomer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAddCustomer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAddCustomer.ForeColor = System.Drawing.Color.White;
            this.customButtonAddCustomer.Location = new System.Drawing.Point(125, 481);
            this.customButtonAddCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAddCustomer.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.Name = "customButtonAddCustomer";
            this.customButtonAddCustomer.Size = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.TabIndex = 12;
            this.customButtonAddCustomer.Text = "&Add Customer";
            this.customButtonAddCustomer.UseVisualStyleBackColor = false;
            this.customButtonAddCustomer.Click += new System.EventHandler(this.addCustomerButton_Click);
            // 
            // customButtonView
            // 
            this.customButtonView.BackColor = System.Drawing.Color.Transparent;
            this.customButtonView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonView.BackgroundImage")));
            this.customButtonView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonView.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonView.FlatAppearance.BorderSize = 0;
            this.customButtonView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonView.ForeColor = System.Drawing.Color.White;
            this.customButtonView.Location = new System.Drawing.Point(825, 483);
            this.customButtonView.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonView.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonView.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonView.Name = "customButtonView";
            this.customButtonView.Size = new System.Drawing.Size(100, 50);
            this.customButtonView.TabIndex = 13;
            this.customButtonView.Text = "&View";
            this.customButtonView.UseVisualStyleBackColor = false;
            this.customButtonView.Click += new System.EventHandler(this.viewButton_Click);
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
            this.select.Frozen = true;
            this.select.HeaderText = "Select";
            this.select.Image = global::Pawn.Properties.Resources.blueglossy_select2;
            this.select.Name = "select";
            this.select.ReadOnly = true;
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.select.Width = 48;
            // 
            // cust_last_name
            // 
            this.cust_last_name.HeaderText = "Customer Last Name";
            this.cust_last_name.Name = "cust_last_name";
            this.cust_last_name.ReadOnly = true;
            this.cust_last_name.Width = 130;
            // 
            // cust_first_name
            // 
            this.cust_first_name.HeaderText = "Customer First Name";
            this.cust_first_name.Name = "cust_first_name";
            this.cust_first_name.ReadOnly = true;
            this.cust_first_name.Width = 129;
            // 
            // Address1
            // 
            this.Address1.HeaderText = "Address1";
            this.Address1.Name = "Address1";
            this.Address1.ReadOnly = true;
            this.Address1.Width = 76;
            // 
            // Address2
            // 
            this.Address2.HeaderText = "Address2";
            this.Address2.Name = "Address2";
            this.Address2.ReadOnly = true;
            this.Address2.Visible = false;
            this.Address2.Width = 85;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.Name = "City";
            this.City.ReadOnly = true;
            this.City.Width = 60;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Width = 57;
            // 
            // Postal
            // 
            this.Postal.HeaderText = "Postal";
            this.Postal.Name = "Postal";
            this.Postal.ReadOnly = true;
            this.Postal.Width = 65;
            // 
            // date_of_birth
            // 
            this.date_of_birth.HeaderText = "Date Of Birth";
            this.date_of_birth.Name = "date_of_birth";
            this.date_of_birth.ReadOnly = true;
            this.date_of_birth.Width = 93;
            // 
            // Ident_desc
            // 
            this.Ident_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Ident_desc.HeaderText = "ID Type";
            this.Ident_desc.Name = "Ident_desc";
            this.Ident_desc.ReadOnly = true;
            this.Ident_desc.Width = 70;
            // 
            // issued_number
            // 
            this.issued_number.HeaderText = "ID Number";
            this.issued_number.Name = "issued_number";
            this.issued_number.ReadOnly = true;
            this.issued_number.Width = 83;
            // 
            // issuer_name
            // 
            this.issuer_name.HeaderText = "Issuer";
            this.issuer_name.Name = "issuer_name";
            this.issuer_name.ReadOnly = true;
            this.issuer_name.Width = 60;
            // 
            // phone
            // 
            this.phone.HeaderText = "Customer Phone Number";
            this.phone.Name = "phone";
            this.phone.ReadOnly = true;
            this.phone.Visible = false;
            this.phone.Width = 117;
            // 
            // LookupCustomerResults
            // 
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1000, 540);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonView);
            this.Controls.Add(this.customButtonAddCustomer);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.lookupCustomerResultsGrid);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupCustomerResults";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lookup Customer Results";
            this.Load += new System.EventHandler(this.LookupCustomerResults_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookupCustomerResultsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;   
        private System.Windows.Forms.DataGridView lookupCustomerResultsGrid;
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
        private CustomButton customButtonAddCustomer;
        private CustomButton customButtonView;
        private System.Windows.Forms.DataGridViewImageColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_last_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_first_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address2;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Postal;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_of_birth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn issued_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuer_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn phone;
    }
}
