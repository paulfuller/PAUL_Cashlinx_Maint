using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration.Assignments
{
    partial class SecurityProfile
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityProfile));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.shopIDComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.homeShopIDLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.employeeRoleLabel = new System.Windows.Forms.Label();
            this.employeeNumberLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.limitsPDLLink = new System.Windows.Forms.LinkLabel();
            this.gvLimits = new System.Windows.Forms.DataGridView();
            this.colLimitID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimitsProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimitsLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResLimitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.limitsErrorLabel = new System.Windows.Forms.Label();
            this.limitsLastUpdated = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.resourcesErrorLabel = new System.Windows.Forms.Label();
            this.resourcesUnAssignButton = new System.Windows.Forms.Button();
            this.resourcesAssignButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.resourcesAvailableListBox = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBoxAssigned = new System.Windows.Forms.ListBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonSubmit = new CustomButton();
            this.customButtonReset = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLimits)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(235, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 19);
            this.label3.TabIndex = 138;
            this.label3.Text = "Security Profile";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(30, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 142;
            this.label4.Text = "Shop:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shopIDComboBox
            // 
            this.shopIDComboBox.BackColor = System.Drawing.Color.White;
            this.shopIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shopIDComboBox.ForeColor = System.Drawing.Color.Black;
            this.shopIDComboBox.FormattingEnabled = true;
            this.shopIDComboBox.Location = new System.Drawing.Point(71, 151);
            this.shopIDComboBox.Name = "shopIDComboBox";
            this.shopIDComboBox.Size = new System.Drawing.Size(137, 21);
            this.shopIDComboBox.TabIndex = 145;
            this.shopIDComboBox.SelectedIndexChanged += new System.EventHandler(this.shopIDComboBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.homeShopIDLabel);
            this.panel1.Location = new System.Drawing.Point(329, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 34);
            this.panel1.TabIndex = 149;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 14);
            this.label6.TabIndex = 141;
            this.label6.Text = "Home Shop:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // homeShopIDLabel
            // 
            this.homeShopIDLabel.AutoSize = true;
            this.homeShopIDLabel.BackColor = System.Drawing.Color.Transparent;
            this.homeShopIDLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeShopIDLabel.Location = new System.Drawing.Point(138, 3);
            this.homeShopIDLabel.Name = "homeShopIDLabel";
            this.homeShopIDLabel.Size = new System.Drawing.Size(52, 14);
            this.homeShopIDLabel.TabIndex = 142;
            this.homeShopIDLabel.Text = "[00024]";
            this.homeShopIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.employeeRoleLabel);
            this.panel2.Controls.Add(this.employeeNumberLabel);
            this.panel2.Location = new System.Drawing.Point(30, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(257, 34);
            this.panel2.TabIndex = 150;
            // 
            // employeeRoleLabel
            // 
            this.employeeRoleLabel.AutoSize = true;
            this.employeeRoleLabel.BackColor = System.Drawing.Color.Transparent;
            this.employeeRoleLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employeeRoleLabel.Location = new System.Drawing.Point(2, 5);
            this.employeeRoleLabel.Name = "employeeRoleLabel";
            this.employeeRoleLabel.Size = new System.Drawing.Size(132, 14);
            this.employeeRoleLabel.TabIndex = 141;
            this.employeeRoleLabel.Text = "[Marion Barber CSR III]";
            this.employeeRoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // employeeNumberLabel
            // 
            this.employeeNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.employeeNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employeeNumberLabel.Location = new System.Drawing.Point(155, 3);
            this.employeeNumberLabel.Name = "employeeNumberLabel";
            this.employeeNumberLabel.Size = new System.Drawing.Size(66, 18);
            this.employeeNumberLabel.TabIndex = 144;
            this.employeeNumberLabel.Text = "[12340]";
            this.employeeNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(20, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 18);
            this.label1.TabIndex = 159;
            this.label1.Text = "Pawn";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // limitsPDLLink
            // 
            this.limitsPDLLink.AutoSize = true;
            this.limitsPDLLink.Enabled = false;
            this.limitsPDLLink.Font = new System.Drawing.Font("Tahoma", 9F);
            this.limitsPDLLink.Location = new System.Drawing.Point(85, 53);
            this.limitsPDLLink.Name = "limitsPDLLink";
            this.limitsPDLLink.Size = new System.Drawing.Size(28, 14);
            this.limitsPDLLink.TabIndex = 158;
            this.limitsPDLLink.TabStop = true;
            this.limitsPDLLink.Text = "PDL";
            // 
            // gvLimits
            // 
            this.gvLimits.AllowUserToAddRows = false;
            this.gvLimits.AllowUserToDeleteRows = false;
            this.gvLimits.AllowUserToResizeColumns = false;
            this.gvLimits.AllowUserToResizeRows = false;
            this.gvLimits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.gvLimits.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvLimits.BackgroundColor = System.Drawing.Color.White;
            this.gvLimits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvLimits.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLimits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvLimits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLimits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLimitID,
            this.colLimitsProduct,
            this.colLimitsLimit,
            this.ResLimitId});
            this.gvLimits.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLimits.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvLimits.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvLimits.GridColor = System.Drawing.Color.LightGray;
            this.gvLimits.Location = new System.Drawing.Point(21, 75);
            this.gvLimits.MultiSelect = false;
            this.gvLimits.Name = "gvLimits";
            this.gvLimits.RowHeadersVisible = false;
            this.gvLimits.RowHeadersWidth = 20;
            this.gvLimits.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLimits.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvLimits.RowTemplate.Height = 25;
            this.gvLimits.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvLimits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvLimits.Size = new System.Drawing.Size(478, 98);
            this.gvLimits.TabIndex = 155;
            this.gvLimits.Tag = "Limits";
            this.gvLimits.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLimits_CellValueChanged);
            // 
            // colLimitID
            // 
            this.colLimitID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLimitID.DataPropertyName = "colLimitID";
            this.colLimitID.FillWeight = 47.31137F;
            this.colLimitID.HeaderText = "LimitID";
            this.colLimitID.Name = "colLimitID";
            this.colLimitID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLimitID.Visible = false;
            // 
            // colLimitsProduct
            // 
            this.colLimitsProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLimitsProduct.DataPropertyName = "colLimitsProduct";
            this.colLimitsProduct.HeaderText = "Product";
            this.colLimitsProduct.Name = "colLimitsProduct";
            this.colLimitsProduct.ReadOnly = true;
            // 
            // colLimitsLimit
            // 
            this.colLimitsLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLimitsLimit.DataPropertyName = "colLimitsLimit";
            this.colLimitsLimit.FillWeight = 47.31137F;
            this.colLimitsLimit.HeaderText = "Limit";
            this.colLimitsLimit.Name = "colLimitsLimit";
            this.colLimitsLimit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ResLimitId
            // 
            this.ResLimitId.HeaderText = "ResLimitID";
            this.ResLimitId.Name = "ResLimitId";
            this.ResLimitId.ReadOnly = true;
            this.ResLimitId.Visible = false;
            this.ResLimitId.Width = 82;
            // 
            // limitsErrorLabel
            // 
            this.limitsErrorLabel.BackColor = System.Drawing.Color.Transparent;
            this.limitsErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.limitsErrorLabel.Location = new System.Drawing.Point(21, -1);
            this.limitsErrorLabel.Name = "limitsErrorLabel";
            this.limitsErrorLabel.Size = new System.Drawing.Size(418, 18);
            this.limitsErrorLabel.TabIndex = 154;
            this.limitsErrorLabel.Text = "[error]";
            this.limitsErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // limitsLastUpdated
            // 
            this.limitsLastUpdated.BackColor = System.Drawing.Color.Transparent;
            this.limitsLastUpdated.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limitsLastUpdated.Location = new System.Drawing.Point(418, 51);
            this.limitsLastUpdated.Name = "limitsLastUpdated";
            this.limitsLastUpdated.Size = new System.Drawing.Size(79, 18);
            this.limitsLastUpdated.TabIndex = 146;
            this.limitsLastUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(331, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 18);
            this.label7.TabIndex = 145;
            this.label7.Text = "Last Updated:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // resourcesErrorLabel
            // 
            this.resourcesErrorLabel.BackColor = System.Drawing.Color.Transparent;
            this.resourcesErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.resourcesErrorLabel.Location = new System.Drawing.Point(102, 392);
            this.resourcesErrorLabel.Name = "resourcesErrorLabel";
            this.resourcesErrorLabel.Size = new System.Drawing.Size(381, 18);
            this.resourcesErrorLabel.TabIndex = 157;
            this.resourcesErrorLabel.Text = "[error]";
            this.resourcesErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resourcesUnAssignButton
            // 
            this.resourcesUnAssignButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resourcesUnAssignButton.AutoSize = true;
            this.resourcesUnAssignButton.BackColor = System.Drawing.Color.Transparent;
            this.resourcesUnAssignButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.resourcesUnAssignButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.resourcesUnAssignButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resourcesUnAssignButton.FlatAppearance.BorderSize = 0;
            this.resourcesUnAssignButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.resourcesUnAssignButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.resourcesUnAssignButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resourcesUnAssignButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resourcesUnAssignButton.ForeColor = System.Drawing.Color.White;
            this.resourcesUnAssignButton.Location = new System.Drawing.Point(273, 552);
            this.resourcesUnAssignButton.Name = "resourcesUnAssignButton";
            this.resourcesUnAssignButton.Size = new System.Drawing.Size(31, 29);
            this.resourcesUnAssignButton.TabIndex = 154;
            this.resourcesUnAssignButton.Text = "<";
            this.resourcesUnAssignButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.resourcesUnAssignButton.UseVisualStyleBackColor = false;
            this.resourcesUnAssignButton.Click += new System.EventHandler(this.resourcesUnAssignButton_Click);
            // 
            // resourcesAssignButton
            // 
            this.resourcesAssignButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.resourcesAssignButton.AutoSize = true;
            this.resourcesAssignButton.BackColor = System.Drawing.Color.Transparent;
            this.resourcesAssignButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.resourcesAssignButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.resourcesAssignButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resourcesAssignButton.FlatAppearance.BorderSize = 0;
            this.resourcesAssignButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.resourcesAssignButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.resourcesAssignButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resourcesAssignButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resourcesAssignButton.ForeColor = System.Drawing.Color.White;
            this.resourcesAssignButton.Location = new System.Drawing.Point(273, 505);
            this.resourcesAssignButton.Name = "resourcesAssignButton";
            this.resourcesAssignButton.Size = new System.Drawing.Size(31, 29);
            this.resourcesAssignButton.TabIndex = 153;
            this.resourcesAssignButton.Text = ">";
            this.resourcesAssignButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.resourcesAssignButton.UseVisualStyleBackColor = false;
            this.resourcesAssignButton.Click += new System.EventHandler(this.resourcesAssignButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(376, 435);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 14);
            this.label5.TabIndex = 143;
            this.label5.Text = "Assigned Resources";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(90, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 14);
            this.label2.TabIndex = 142;
            this.label2.Text = "Available Resources";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resourcesAvailableListBox
            // 
            this.resourcesAvailableListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resourcesAvailableListBox.FormattingEnabled = true;
            this.resourcesAvailableListBox.Location = new System.Drawing.Point(30, 455);
            this.resourcesAvailableListBox.Name = "resourcesAvailableListBox";
            this.resourcesAvailableListBox.Size = new System.Drawing.Size(235, 171);
            this.resourcesAvailableListBox.Sorted = true;
            this.resourcesAvailableListBox.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.limitsErrorLabel);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.limitsPDLLink);
            this.panel3.Controls.Add(this.gvLimits);
            this.panel3.Controls.Add(this.limitsLastUpdated);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(30, 198);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(522, 183);
            this.panel3.TabIndex = 153;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(3, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 18);
            this.label9.TabIndex = 160;
            this.label9.Text = "Limits";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(30, 410);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 18);
            this.label8.TabIndex = 161;
            this.label8.Text = "Resources";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxAssigned
            // 
            this.listBoxAssigned.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxAssigned.FormattingEnabled = true;
            this.listBoxAssigned.Location = new System.Drawing.Point(345, 455);
            this.listBoxAssigned.Name = "listBoxAssigned";
            this.listBoxAssigned.Size = new System.Drawing.Size(197, 173);
            this.listBoxAssigned.TabIndex = 165;
            this.listBoxAssigned.Click += new System.EventHandler(this.assignedListBox1_Click);
            this.listBoxAssigned.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.assignedListBox1_DrawItem);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "colLimitID";
            this.dataGridViewTextBoxColumn1.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Allowed";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "colLimitsProduct";
            this.dataGridViewTextBoxColumn2.HeaderText = "Product";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "colLimitsLimit";
            this.dataGridViewTextBoxColumn3.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Limit";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "ResLimitID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 82;
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(425, 648);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 164;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // customButtonReset
            // 
            this.customButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonReset.BackgroundImage")));
            this.customButtonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReset.FlatAppearance.BorderSize = 0;
            this.customButtonReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReset.ForeColor = System.Drawing.Color.White;
            this.customButtonReset.Location = new System.Drawing.Point(317, 648);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 163;
            this.customButtonReset.Text = "Reset";
            this.customButtonReset.UseVisualStyleBackColor = false;
            this.customButtonReset.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(68, 648);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 162;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SecurityProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(584, 716);
            this.Controls.Add(this.listBoxAssigned);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.resourcesErrorLabel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.resourcesUnAssignButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.resourcesAssignButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.shopIDComboBox);
            this.Controls.Add(this.resourcesAvailableListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SecurityProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeSearch";
            this.Load += new System.EventHandler(this.SecurityProfile_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLimits)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox shopIDComboBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label homeShopIDLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label employeeRoleLabel;
        private System.Windows.Forms.Label employeeNumberLabel;
        private System.Windows.Forms.Label limitsLastUpdated;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label limitsErrorLabel;
        private System.Windows.Forms.DataGridView gvLimits;
        private System.Windows.Forms.LinkLabel limitsPDLLink;
        private System.Windows.Forms.ListBox resourcesAvailableListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button resourcesUnAssignButton;
        private System.Windows.Forms.Button resourcesAssignButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label resourcesErrorLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private CustomButton customButtonCancel;
        private CustomButton customButtonReset;
        private CustomButton customButtonSubmit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimitID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimitsProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimitsLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResLimitId;
        private System.Windows.Forms.ListBox listBoxAssigned;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}